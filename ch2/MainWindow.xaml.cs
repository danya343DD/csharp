using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FileExplorer
{
    public partial class MainWindow : Window
    {
        private string currentPath = "";
        private Stack<string> navigationHistory = new Stack<string>();

        public MainWindow()
        {
            InitializeComponent();
            LoadDrives();
        }

        // === Загрузка дисков ===
        private void LoadDrives()
        {
            FolderTree.Items.Clear();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    TreeViewItem item = new TreeViewItem
                    {
                        Header = $"{drive.Name} ({drive.VolumeLabel})",
                        Tag = drive.RootDirectory.FullName
                    };

                    // Добавляем заглушку для ленивой загрузки
                    item.Items.Add(new TreeViewItem { Header = "Загрузка...", Tag = "placeholder" });
                    item.Expanded += DriveFolder_Expanded;

                    FolderTree.Items.Add(item);
                }
            }
        }

        private void DriveFolder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;

            // Загружаем подпапки только при первом раскрытии
            if (item.Items.Count == 1 &&
                ((TreeViewItem)item.Items[0]).Tag?.ToString() == "placeholder")
            {
                item.Items.Clear();
                string path = item.Tag.ToString();

                try
                {
                    foreach (string dir in Directory.GetDirectories(path))
                    {
                        TreeViewItem subItem = new TreeViewItem
                        {
                            Header = Path.GetFileName(dir),
                            Tag = dir
                        };
                        subItem.Items.Add(new TreeViewItem { Header = "Загрузка...", Tag = "placeholder" });
                        subItem.Expanded += DriveFolder_Expanded;

                        item.Items.Add(subItem);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // Пропускаем папки без доступа
                }
            }
        }

        // === Навигация по дереву ===
        private void FolderTree_SelectedItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            if (FolderTree.SelectedItem is TreeViewItem item &&
                item.Tag is string path &&
                Directory.Exists(path))
            {
                NavigateTo(path);
            }
        }

        private void FolderTree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (FolderTree.SelectedItem is TreeViewItem item &&
                item.Tag is string path &&
                Directory.Exists(path))
            {
                NavigateTo(path);
            }
        }

        // === Навигация ===
        private void NavigateTo(string path)
        {
            try
            {
                navigationHistory.Push(currentPath);
                currentPath = path;
                AddressBox.Text = path;
                LoadDirectoryContents(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка доступа: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadDirectoryContents(string path)
        {
            FileListView.Items.Clear();

            try
            {
                // Загружаем папки
                foreach (string dir in Directory.GetDirectories(path))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);
                    FileListView.Items.Add(new
                    {
                        Name = dirInfo.Name,
                        Type = "Папка",
                        Size = "",
                        Modified = dirInfo.LastWriteTime.ToString("dd.MM.yyyy HH:mm"),
                        FullPath = dir
                    });
                }

                // Загружаем файлы
                foreach (string file in Directory.GetFiles(path))
                {
                    FileInfo fileInfo = new FileInfo(file);
                    FileListView.Items.Add(new
                    {
                        Name = fileInfo.Name,
                        Type = fileInfo.Extension.ToUpper() + " файл",
                        Size = FormatSize(fileInfo.Length),
                        Modified = fileInfo.LastWriteTime.ToString("dd.MM.yyyy HH:mm"),
                        FullPath = file
                    });
                }

                ItemCountText.Text = $"Элементов: {FileListView.Items.Count}";
            }
            catch (UnauthorizedAccessException)
            {
                ItemCountText.Text = "Нет доступа";
            }
        }

        private string FormatSize(long bytes)
        {
            string[] sizes = { "Б", "КБ", "МБ", "ГБ" };
            int order = 0;
            double size = bytes;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return $"{size:F1} {sizes[order]}";
        }

        // === Кнопки навигации ===
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (navigationHistory.Count > 0)
            {
                string previousPath = navigationHistory.Pop();
                if (Directory.Exists(previousPath))
                {
                    currentPath = previousPath;
                    AddressBox.Text = currentPath;
                    LoadDirectoryContents(currentPath);
                }
            }
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentPath))
            {
                DirectoryInfo parent = Directory.GetParent(currentPath);
                if (parent != null)
                {
                    NavigateTo(parent.FullName);
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentPath))
                LoadDirectoryContents(currentPath);
        }

        // === Адресная строка ===
        private void AddressBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string path = AddressBox.Text;
                if (Directory.Exists(path))
                {
                    NavigateTo(path);
                }
                else
                {
                    MessageBox.Show("Указанный путь не существует!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        // === Действия с файлами ===
        private void FileListView_MouseDoubleClick(object sender,
            MouseButtonEventArgs e)
        {
            OpenSelectedItem();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectedItem();
        }

        private void OpenSelectedItem()
        {
            if (FileListView.SelectedItem != null)
            {
                dynamic item = FileListView.SelectedItem;
                string fullPath = item.FullPath;

                if (Directory.Exists(fullPath))
                {
                    NavigateTo(fullPath);
                }
                else if (File.Exists(fullPath))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(fullPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось открыть файл: {ex.Message}",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void CreateFolder_Click(object sender, RoutedEventArgs e)
        {
            string folderName = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите имя новой папки:", "Создание папки", "Новая папка");

            if (!string.IsNullOrEmpty(folderName))
            {
                try
                {
                    string newFolderPath = Path.Combine(currentPath, folderName);
                    Directory.CreateDirectory(newFolderPath);
                    LoadDirectoryContents(currentPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось создать папку: {ex.Message}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CopyFile_Click(object sender, RoutedEventArgs e)
        {
            if (FileListView.SelectedItem != null)
            {
                dynamic item = FileListView.SelectedItem;
                Clipboard.SetText(item.FullPath);
                MessageBox.Show("Путь скопирован в буфер обмена",
                    "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteFile_Click(object sender, RoutedEventArgs e)
        {
            if (FileListView.SelectedItem != null)
            {
                var result = MessageBox.Show("Удалить выбранный элемент?",
                    "Подтверждение", MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        dynamic item = FileListView.SelectedItem;
                        string fullPath = item.FullPath;

                        if (Directory.Exists(fullPath))
                            Directory.Delete(fullPath, true);
                        else if (File.Exists(fullPath))
                            File.Delete(fullPath);

                        LoadDirectoryContents(currentPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось удалить: {ex.Message}",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void TreeOpen_Click(object sender, RoutedEventArgs e)
        {
            FolderTree_MouseDoubleClick(null, null);
        }

        private void ViewMode_Click(object sender, RoutedEventArgs e)
        {
            // Заглушка для разных режимов отображения
            MenuItem menuItem = (MenuItem)sender;
            MessageBox.Show($"Режим: {menuItem.Tag}",
                "Режим просмотра", MessageBoxButton.OK);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}