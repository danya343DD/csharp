using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TextEditor
{
    public partial class MainWindow : Window
    {
        private string currentFilePath = "";
        private bool isModified = false;

        public MainWindow()
        {
            InitializeComponent();
            UpdateTitle();

            // Отслеживание изменений текста
            EditorTextBox.TextChanged += (s, e) =>
            {
                isModified = true;
                UpdateTitle();
            };

            // Отслеживание позиции курсора
            EditorTextBox.SelectionChanged += (s, e) =>
            {
                int line = EditorTextBox.GetLineIndexFromCharacterIndex(
                    EditorTextBox.CaretIndex);
                int col = EditorTextBox.CaretIndex -
                    EditorTextBox.GetCharacterIndexFromLineIndex(line);
                PositionText.Text = $"Строка: {line + 1}, Столбец: {col + 1}";
            };
        }

        private void UpdateTitle()
        {
            string fileName = string.IsNullOrEmpty(currentFilePath) ?
                "Новый документ" : currentFilePath;
            string modified = isModified ? " *" : "";
            Title = $"{fileName}{modified} - Текстовый редактор";
        }

        // === Файловые операции ===

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            if (isModified)
            {
                var result = MessageBox.Show("Сохранить изменения?",
                    "Новый документ", MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Cancel) return;
                if (result == MessageBoxResult.Yes) SaveFile();
            }

            EditorTextBox.Clear();
            currentFilePath = "";
            isModified = false;
            UpdateTitle();
            StatusText.Text = "Создан новый документ";
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Открыть текстовый файл"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    EditorTextBox.Text = File.ReadAllText(dialog.FileName);
                    currentFilePath = dialog.FileName;
                    isModified = false;
                    UpdateTitle();
                    StatusText.Text = $"Открыт: {dialog.FileName}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка открытия файла: {ex.Message}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
                SaveAsFile();
            else
                SaveFile(currentFilePath);
        }

        private void SaveAsFile_Click(object sender, RoutedEventArgs e)
        {
            SaveAsFile();
        }

        private void SaveFile()
        {
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                SaveFile(currentFilePath);
            }
        }
        private void SaveFile(string filePath)
        {
            try
            {
                File.WriteAllText(filePath, EditorTextBox.Text);
                currentFilePath = filePath;
                isModified = false;
                UpdateTitle();
                StatusText.Text = $"Сохранён: {filePath}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveAsFile()
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Сохранить как"
            };

            if (dialog.ShowDialog() == true)
            {
                SaveFile(dialog.FileName);
            }
        }

        // === Операции редактирования ===

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (EditorTextBox.CanUndo)
                EditorTextBox.Undo();
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EditorTextBox.SelectedText))
            {
                Clipboard.SetText(EditorTextBox.SelectedText);
                EditorTextBox.SelectedText = "";
            }
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EditorTextBox.SelectedText))
                Clipboard.SetText(EditorTextBox.SelectedText);
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
                EditorTextBox.SelectedText = Clipboard.GetText();
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            EditorTextBox.SelectAll();
            EditorTextBox.Focus();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // === Настройки ===

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow(EditorTextBox);
            settings.Owner = this;
            settings.ShowDialog();
        }

        private void EditorTextBox_ContextMenuOpening(object sender,
            ContextMenuEventArgs e)
        {
            // Контекстное меню уже определено в XAML
        }

        // Закрытие окна с проверкой сохранения
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (isModified)
            {
                var result = MessageBox.Show("Сохранить изменения перед выходом?",
                    "Выход", MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (result == MessageBoxResult.Yes)
                    SaveFile();  
            }

            base.OnClosing(e);
        }
    }
}