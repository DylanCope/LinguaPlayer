using System.Windows;
using System.Windows.Controls;

namespace VideoPlayer
{
    public partial class PreferencesWindow : Window
    {
        public bool ShowSuccessPopups { get; private set; }
        public new double FontSize { get; private set; }  // Add 'new' keyword to explicitly hide base member

        public PreferencesWindow(bool currentShowSuccessPopups, double currentFontSize)
        {
            InitializeComponent();
            ShowSuccessPopups = currentShowSuccessPopups;
            FontSize = currentFontSize;
            
            ShowSuccessPopupsCheckBox.IsChecked = ShowSuccessPopups;
            
            // Set the initial font size selection
            foreach (ComboBoxItem item in FontSizeComboBox.Items)
            {
                if (item.Tag != null && double.TryParse(item.Tag.ToString(), out double itemFontSize) && itemFontSize == currentFontSize)
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
            
            var selectedItem = FontSizeComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem?.Tag != null && double.TryParse(selectedItem.Tag.ToString(), out double newFontSize))
            {
                FontSize = newFontSize;
            }
            
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