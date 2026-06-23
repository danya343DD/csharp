using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TextEditor
{
    public partial class SettingsWindow : Window
    {
        private TextBox editorTextBox;

        public SettingsWindow(TextBox textBox)
        {
            InitializeComponent();
            editorTextBox = textBox;

            // Загружаем текущие настройки
            LoadCurrentSettings();
        }

        private void LoadCurrentSettings()
        {
            // Цвет шрифта
            foreach (ComboBoxItem item in FontColorCombo.Items)
            {
                if (item.Tag.ToString() == editorTextBox.Foreground.ToString())
                {
                    FontColorCombo.SelectedItem = item;
                    break;
                }
            }

            // Цвет фона
            foreach (ComboBoxItem item in BackgroundColorCombo.Items)
            {
                if (item.Tag.ToString() == editorTextBox.Background.ToString())
                {
                    BackgroundColorCombo.SelectedItem = item;
                    break;
                }
            }

            // Шрифт
            foreach (ComboBoxItem item in FontFamilyCombo.Items)
            {
                if (item.Content.ToString() == editorTextBox.FontFamily.Source)
                {
                    FontFamilyCombo.SelectedItem = item;
                    break;
                }
            }

            // Размер шрифта
            FontSizeCombo.Text = editorTextBox.FontSize.ToString();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            // цвет шрифта
            if (FontColorCombo.SelectedItem is ComboBoxItem fontColorItem)
            {
                string colorName = fontColorItem.Tag.ToString();
                editorTextBox.Foreground = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString(colorName));
            }

            // цвет фона
            if (BackgroundColorCombo.SelectedItem is ComboBoxItem bgColorItem)
            {
                string colorName = bgColorItem.Tag.ToString();
                editorTextBox.Background = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString(colorName));
            }

            // шрифт
            if (FontFamilyCombo.SelectedItem is ComboBoxItem fontItem)
            {
                editorTextBox.FontFamily = new FontFamily(fontItem.Content.ToString());
            }

            // размер шрифта
            if (double.TryParse(FontSizeCombo.Text, out double fontSize))
            {
                editorTextBox.FontSize = fontSize;
            }

            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}