using System.Windows;

namespace VideoPlayer
{
    public partial class CustomMessageBox : Window
    {
        public bool Result { get; private set; }

        public CustomMessageBox(string message, string title, MessageBoxButton buttons = MessageBoxButton.OK)
        {
            InitializeComponent();
            
            MessageText.Text = message;
            TitleText.Text = title;

            // Configure buttons based on MessageBoxButton type
            if (buttons == MessageBoxButton.OKCancel)
            {
                CancelButton.Visibility = Visibility.Visible;
            }

            // Enable window dragging
            this.MouseLeftButtonDown += (s, e) =>
            {
                if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                    this.DragMove();
            };
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            Close();
        }

        public static MessageBoxResult Show(string message, string title = "Message", MessageBoxButton buttons = MessageBoxButton.OK)
        {
            var dialog = new CustomMessageBox(message, title, buttons)
            {
                Owner = Application.Current.MainWindow
            };

            dialog.ShowDialog();

            if (buttons == MessageBoxButton.OK)
                return MessageBoxResult.OK;

            return dialog.Result ? MessageBoxResult.OK : MessageBoxResult.Cancel;
        }
    }
} 