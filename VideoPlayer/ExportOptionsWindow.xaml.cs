using System.Collections.Generic;
using System.Windows;
using VideoPlayer.Models;

namespace VideoPlayer
{
    public partial class ExportOptionsWindow : Window
    {
        public List<SubtitleItem> SelectedSubtitles { get; private set; }
        public string VideoPath { get; private set; }
        public string OutputPath { get; private set; }

        public ExportOptionsWindow(List<SubtitleItem> selectedSubtitles, string videoPath)
        {
            InitializeComponent();
            SelectedSubtitles = selectedSubtitles;
            VideoPath = videoPath;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "MP3 files (*.mp3)|*.mp3",
                DefaultExt = ".mp3"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                OutputPath = saveFileDialog.FileName;
                DialogResult = true;
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 