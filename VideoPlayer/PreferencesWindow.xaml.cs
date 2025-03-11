using System.Windows;
using System.Windows.Controls;

namespace VideoPlayer
{
    public partial class PreferencesWindow : Window
    {
        public bool ShowSuccessPopups { get; private set; }
        public double FontSize { get; private set; }

        public PreferencesWindow(bool currentShowSuccessPopups, double currentFontSize)
        {
            InitializeComponent();
            ShowSuccessPopups = currentShowSuccessPopups;
            FontSize = currentFontSize;
            
            ShowSuccessPopupsCheckBox.IsChecked = ShowSuccessPopups;
            
            // Set the initial font size selection
            foreach (ComboBoxItem item in FontSizeComboBox.Items)
            {
                if (double.Parse(item.Tag.ToString()) == currentFontSize)
                {
                    FontSizeComboBox.SelectedItem = item;
                    break;
                }
            }
            
            // If no match found, default to Medium (14)
            if (FontSizeComboBox.SelectedItem == null)
            {
                FontSizeComboBox.SelectedItem = FontSizeComboBox.Items[1]; // Medium
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSuccessPopups = ShowSuccessPopupsCheckBox.IsChecked ?? true;
            FontSize = double.Parse(((ComboBoxItem)FontSizeComboBox.SelectedItem).Tag.ToString());
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 