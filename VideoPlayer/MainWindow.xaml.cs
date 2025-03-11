using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using VideoPlayer.Models;
using VideoPlayer.Services;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace VideoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SubtitleService _subtitleService;
        private readonly DispatcherTimer _timer;
        private bool _isPlaying;
        private List<SubtitleItem>? _subtitles;
        private SubtitleItem? _currentSubtitle;
        private bool _isDraggingSlider;
        private string? _currentVideoPath;
        private bool _showSuccessPopups = true; // Default value
        private double _currentFontSize = 14; // Default font size

        // Add logging
        private void Log(string message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[VideoPlayer] {DateTime.Now:HH:mm:ss.fff}: {message}");
            }
            catch { } // Ensure logging never crashes the app
        }

        public MainWindow()
        {
            try
            {
                // Add global exception handlers
                Application.Current.DispatcherUnhandledException += (s, e) =>
                {
                    Log($"Unhandled exception in dispatcher: {e.Exception}");
                    CustomMessageBox.Show($"An error occurred: {e.Exception.Message}", "Error");
                    e.Handled = true;
                };

                AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                {
                    Log($"Unhandled exception in AppDomain: {e.ExceptionObject}");
                };

                Log("Initializing MainWindow");
                InitializeComponent();
                
                // Initialize fields that were causing nullable warnings
                _subtitleService = new SubtitleService();
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromMilliseconds(50);
                _timer.Tick += Timer_Tick;

                // Enable window dragging
                this.MouseLeftButtonDown += (s, e) =>
                {
                    if (e.ChangedButton == MouseButton.Left)
                        this.DragMove();
                };

                InitializeVideoPlayer();
                InitializeSubtitleControls();
                
                // Add keyboard event handler
                this.KeyDown += MainWindow_KeyDown;
                
                Log("MainWindow initialization complete");
            }
            catch (Exception ex)
            {
                Log($"Fatal error during initialization: {ex}");
                CustomMessageBox.Show("Fatal error during application initialization. The application will now close.", 
                    "Fatal Error");
                Application.Current.Shutdown();
            }
        }

        private void InitializeVideoPlayer()
        {
            try
            {
                // Set up video player properties
                VideoPlayer.ScrubbingEnabled = true;
                VideoPlayer.LoadedBehavior = MediaState.Manual;
                VideoPlayer.UnloadedBehavior = MediaState.Stop;
                
                // Register media events
                VideoPlayer.MediaFailed += VideoPlayer_MediaFailed;
                VideoPlayer.MediaOpened += VideoPlayer_MediaOpened;
                VideoPlayer.MediaEnded += VideoPlayer_MediaEnded;
                
                // Set up timeline slider
                TimelineSlider.Minimum = 0;
                TimelineSlider.SmallChange = 1;
                TimelineSlider.LargeChange = 5;
                TimelineSlider.Value = 0;

                // Handle mouse wheel on timeline
                TimelineSlider.MouseWheel += (s, e) =>
                {
                    e.Handled = true; // Prevent scrolling
                };

                // Add mouse event handlers
                TimelineSlider.AddHandler(MouseLeftButtonDownEvent, 
                    new MouseButtonEventHandler(TimelineSlider_MouseLeftButtonDown), true);
                TimelineSlider.AddHandler(MouseLeftButtonUpEvent, 
                    new MouseButtonEventHandler(TimelineSlider_MouseLeftButtonUp), true);
                TimelineSlider.AddHandler(MouseMoveEvent, 
                    new MouseEventHandler(TimelineSlider_MouseMove), true);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Error initializing video player: {ex.Message}", "Initialization Error");
            }
        }

        private void InitializeSubtitleControls()
        {
            // Initialize subtitle list view
            SubtitleListView.ItemsSource = _subtitles;
            SubtitleListView.SelectionMode = SelectionMode.Extended;  // Enable multi-selection
            
            // Add context menu
            var contextMenu = new ContextMenu();
            var mergeMenuItem = new MenuItem { Header = "Merge Subtitles" };
            mergeMenuItem.Click += MergeSubtitles_Click;
            contextMenu.Items.Add(mergeMenuItem);
            SubtitleListView.ContextMenu = contextMenu;
            
            // Handle context menu opening to enable/disable merge option
            contextMenu.Opened += (s, e) =>
            {
                var selectedItems = SubtitleListView.SelectedItems.Cast<SubtitleItem>().ToList();
                mergeMenuItem.IsEnabled = CanMergeSubtitles(selectedItems);
            };
            
            // Clear subtitle overlay
            SubtitleOverlay.Text = "";
            
            // Disable subtitle editing controls until subtitles are loaded
            StartTimeTextBox.IsEnabled = false;
            EndTimeTextBox.IsEnabled = false;
            SubtitleTextBox.IsEnabled = false;
            ApplyChangesButton.IsEnabled = false;
            SyncStartButton.IsEnabled = false;
            SyncEndButton.IsEnabled = false;
            SaveSubtitlesMenuItem.IsEnabled = false;
        }

        private bool CanMergeSubtitles(List<SubtitleItem> selectedSubtitles)
        {
            if (selectedSubtitles.Count < 2) return false;

            // Get the indices of selected subtitles
            var indices = selectedSubtitles
                .Select(s => _subtitles.IndexOf(s))
                .OrderBy(i => i)
                .ToList();

            // Check if the indices are consecutive
            for (int i = 1; i < indices.Count; i++)
            {
                if (indices[i] != indices[i - 1] + 1)
                {
                    return false;
                }
            }

            return true;
        }

        private void MergeSubtitles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedSubtitles = SubtitleListView.SelectedItems.Cast<SubtitleItem>().ToList();
                if (!CanMergeSubtitles(selectedSubtitles))
                {
                    CustomMessageBox.Show("Cannot merge non-consecutive subtitles.", "Warning");
                    return;
                }

                // Sort selected subtitles by start time to ensure correct order
                selectedSubtitles = selectedSubtitles.OrderBy(s => s.StartTime).ToList();

                // Create merged subtitle
                var mergedSubtitle = new SubtitleItem
                {
                    StartTime = selectedSubtitles.First().StartTime,
                    EndTime = selectedSubtitles.Max(s => s.EndTime),
                    Text = string.Join("\n", selectedSubtitles.Select(s => s.Text))
                };

                // Remove selected subtitles
                foreach (var subtitle in selectedSubtitles)
                {
                    _subtitles.Remove(subtitle);
                }

                // Add merged subtitle
                _subtitles.Add(mergedSubtitle);

                // Sort all subtitles by start time
                SortSubtitles();

                // Refresh ListView
                SubtitleListView.ItemsSource = null;
                SubtitleListView.ItemsSource = _subtitles;

                // Select the merged subtitle
                SubtitleListView.SelectedItem = mergedSubtitle;
                SubtitleListView.ScrollIntoView(mergedSubtitle);

                ShowSuccessMessage("Subtitles merged successfully!");
            }
            catch (Exception ex)
            {
                Log($"Error merging subtitles: {ex}");
                CustomMessageBox.Show($"Error merging subtitles: {ex.Message}", "Error");
            }
        }

        private void SortSubtitles()
        {
            // Sort subtitles by start time
            var sortedList = _subtitles.OrderBy(s => s.StartTime).ToList();
            _subtitles.Clear();
            foreach (var subtitle in sortedList)
            {
                _subtitles.Add(subtitle);
            }
        }

        private void TimelineSlider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (VideoPlayer?.Source == null) return;

            var slider = sender as Slider;
            if (slider == null) return;

            _isDraggingSlider = true;
            if (_isPlaying)
            {
                VideoPlayer.Pause();
            }

            Point pos = e.GetPosition(slider);
            double ratio = pos.X / slider.ActualWidth;
            double newValue = ratio * slider.Maximum;

            // Ensure the value is within bounds
            newValue = Math.Max(slider.Minimum, Math.Min(slider.Maximum, newValue));
            
            // Update position
            VideoPlayer.Position = TimeSpan.FromSeconds(newValue);
            slider.Value = newValue;
            
            UpdateTimeDisplay(VideoPlayer.Position, VideoPlayer.NaturalDuration.TimeSpan);
            UpdateCurrentSubtitle();

            // Capture mouse
            Mouse.Capture(slider);
            e.Handled = true;
        }

        private void TimelineSlider_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDraggingSlider && e.LeftButton == MouseButtonState.Pressed)
            {
                var slider = sender as Slider;
                if (slider == null) return;

                var mousePosition = e.GetPosition(slider);
                var ratio = mousePosition.X / slider.ActualWidth;
                var newValue = ratio * slider.Maximum;
                slider.Value = Math.Max(slider.Minimum, Math.Min(slider.Maximum, newValue));
                
                // Update video position while dragging
                VideoPlayer.Position = TimeSpan.FromSeconds(slider.Value);
                UpdateTimeDisplay(VideoPlayer.Position, VideoPlayer.NaturalDuration.TimeSpan);
                UpdateCurrentSubtitle();
            }
        }

        private void TimelineSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var slider = sender as Slider;
            if (slider == null) return;

            if (_isDraggingSlider)
            {
                VideoPlayer.Position = TimeSpan.FromSeconds(slider.Value);
                UpdateTimeDisplay(VideoPlayer.Position, VideoPlayer.NaturalDuration.TimeSpan);
                UpdateCurrentSubtitle();

                if (_isPlaying)
                {
                    VideoPlayer.Play();
                }
            }

            // Release mouse capture and reset state
            _isDraggingSlider = false;
            Mouse.Capture(null);
            e.Handled = true;
        }

        private void LoadVideoButton_Click(object sender, RoutedEventArgs e)
        {
            Log("LoadVideoButton clicked");
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Video Files|*.mp4;*.avi;*.mkv;*.mov|All Files|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    Log($"Attempting to load video: {openFileDialog.FileName}");

                    // Stop any currently playing video
                    if (VideoPlayer.Source != null)
                    {
                        Log("Stopping current video");
                        VideoPlayer.Stop();
                        _timer.Stop();
                        _isPlaying = false;
                    }

                    _currentVideoPath = openFileDialog.FileName;
                    
                    // Create URI and verify it's valid
                    var uri = new Uri(_currentVideoPath);
                    if (!System.IO.File.Exists(_currentVideoPath))
                    {
                        Log("Selected file does not exist");
                        throw new System.IO.FileNotFoundException("The selected video file does not exist.");
                    }

                    Log("Setting up video player");
                    // Set up video player
                    VideoPlayer.Source = uri;
                    
                    // Reset state
                    TimelineSlider.Value = 0;
                    UpdateTimeDisplay(TimeSpan.Zero, TimeSpan.Zero);
                    
                    // Start playback
                    Log("Starting video playback");
                    VideoPlayer.Play();
                    _isPlaying = true;
                    PlayPauseButton.Content = "Pause";
                    _timer.Start();
                }
                catch (Exception ex)
                {
                    Log($"Error loading video: {ex}");
                    CustomMessageBox.Show($"Error loading video: {ex.Message}", "Error");
                    
                    // Reset state on error
                    Log("Resetting player state after error");
                    VideoPlayer.Source = null;
                    _currentVideoPath = null;
                    _isPlaying = false;
                    PlayPauseButton.Content = "Play";
                    _timer.Stop();
                    TimelineSlider.Value = 0;
                    UpdateTimeDisplay(TimeSpan.Zero, TimeSpan.Zero);
                }
            }
        }

        private void LoadSubtitlesButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "SRT files (*.srt)|*.srt|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                LoadSubtitlesAsync(openFileDialog.FileName);
            }
        }

        private async void LoadSubtitlesAsync(string filePath)
        {
            try
            {
                _subtitles = await _subtitleService.LoadSubtitlesAsync(filePath);
                
                // Sort subtitles when loading
                _subtitles = _subtitles.OrderBy(s => s.StartTime).ToList();
                
                SubtitleListView.ItemsSource = _subtitles;

                // Enable subtitle editing controls
                StartTimeTextBox.IsEnabled = true;
                EndTimeTextBox.IsEnabled = true;
                SubtitleTextBox.IsEnabled = true;
                ApplyChangesButton.IsEnabled = true;
                SyncStartButton.IsEnabled = true;
                SyncEndButton.IsEnabled = true;
                SaveSubtitlesMenuItem.IsEnabled = true;

                ShowSuccessMessage("Subtitles loaded successfully!");
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Error loading subtitles: {ex.Message}", "Error");
            }
        }

        private void SaveSubtitlesButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "SRT files (*.srt)|*.srt|All files (*.*)|*.*",
                DefaultExt = ".srt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                SaveSubtitlesAsync(saveFileDialog.FileName);
            }
        }

        private async void SaveSubtitlesAsync(string filePath)
        {
            try
            {
                await _subtitleService.SaveSubtitlesAsync(filePath, _subtitles);
                ShowSuccessMessage("Subtitles saved successfully!");
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Error saving subtitles: {ex.Message}", "Error");
            }
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isPlaying)
            {
                VideoPlayer.Pause();
                _timer.Stop();
                PlayPauseButton.Content = "Play";
            }
            else
            {
                VideoPlayer.Play();
                _timer.Start();
                PlayPauseButton.Content = "Pause";
            }
            _isPlaying = !_isPlaying;
        }

        private void VideoPlayer_MediaFailed(object? sender, ExceptionRoutedEventArgs e)
        {
            Log($"MediaFailed event: {e.ErrorException}");
            CustomMessageBox.Show($"Error loading video: {e.ErrorException.Message}", "Media Error");
        }

        private void VideoPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            Log("MediaOpened event triggered");
            try
            {
                // Wait a short moment for the duration to be available
                var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
                timer.Tick += (s, args) =>
                {
                    try
                    {
                        timer.Stop();
                        if (VideoPlayer.NaturalDuration.HasTimeSpan)
                        {
                            Log($"Video duration: {VideoPlayer.NaturalDuration.TimeSpan}");
                            TimelineSlider.Maximum = VideoPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                            UpdateTimeDisplay(VideoPlayer.Position, VideoPlayer.NaturalDuration.TimeSpan);
                        }
                        else
                        {
                            Log("Video duration still not available after delay");
                            // For videos without duration info, set a large maximum
                            TimelineSlider.Maximum = double.MaxValue;
                            UpdateTimeDisplay(VideoPlayer.Position, TimeSpan.Zero);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"Error in delayed duration check: {ex}");
                        // Set default values
                        TimelineSlider.Maximum = double.MaxValue;
                        UpdateTimeDisplay(TimeSpan.Zero, TimeSpan.Zero);
                    }
                };
                timer.Start();

                // Clear subtitle overlay
                SubtitleOverlay.Text = "";
            }
            catch (Exception ex)
            {
                Log($"Error in MediaOpened: {ex}");
                CustomMessageBox.Show($"Error initializing video: {ex.Message}", "Error");
                
                // Reset state on error
                VideoPlayer.Source = null;
                _currentVideoPath = null;
                _isPlaying = false;
                PlayPauseButton.Content = "Play";
                _timer.Stop();
                TimelineSlider.Value = 0;
                UpdateTimeDisplay(TimeSpan.Zero, TimeSpan.Zero);
            }
        }

        private void VideoPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            Log("MediaEnded event triggered");
            VideoPlayer.Stop();
            _isPlaying = false;
            PlayPauseButton.Content = "Play";
            _timer.Stop();
            SubtitleOverlay.Text = "";  // Clear subtitle text when video ends
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (!_isDraggingSlider && VideoPlayer?.Source != null)
            {
                try
                {
                    var position = VideoPlayer.Position;
                    TimelineSlider.Value = position.TotalSeconds;
                    
                    if (VideoPlayer.NaturalDuration.HasTimeSpan)
                    {
                        UpdateTimeDisplay(position, VideoPlayer.NaturalDuration.TimeSpan);
                    }
                    else
                    {
                        UpdateTimeDisplay(position, TimeSpan.Zero);
                    }
                    
                    UpdateCurrentSubtitle();
                }
                catch (Exception ex)
                {
                    Log($"Error in Timer_Tick: {ex}");
                }
            }
        }

        private void TimelineSlider_DragStarted(object sender, DragStartedEventArgs e)
        {
            _isDraggingSlider = true;
            if (_isPlaying)
            {
                VideoPlayer.Pause();
                _timer.Stop();
            }
        }

        private void TimelineSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (sender is Slider slider)
            {
                VideoPlayer.Position = TimeSpan.FromSeconds(slider.Value);
                UpdateTimeDisplay(VideoPlayer.Position, VideoPlayer.NaturalDuration.TimeSpan);
                UpdateCurrentSubtitle();

                if (_isPlaying)
                {
                    VideoPlayer.Play();
                    _timer.Start();
                }
            }

            // Release mouse capture and reset state
            _isDraggingSlider = false;
            Mouse.Capture(null);
        }

        private void TimelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender is Slider && VideoPlayer?.Source != null)
            {
                // Update time display
                UpdateTimeDisplay(TimeSpan.FromSeconds(e.NewValue), VideoPlayer.NaturalDuration.TimeSpan);

                // Update video position if not currently dragging
                if (!_isDraggingSlider)
                {
                    VideoPlayer.Position = TimeSpan.FromSeconds(e.NewValue);
                    UpdateCurrentSubtitle();
                }
            }
        }

        private void UpdateTimeDisplay(TimeSpan current, TimeSpan total)
        {
            try
            {
                if (total == TimeSpan.Zero)
                {
                    // If total duration is not available, only show current time
                    TimeDisplay.Text = $"{current:hh\\:mm\\:ss}";
                }
                else
                {
                    TimeDisplay.Text = $"{current:hh\\:mm\\:ss} / {total:hh\\:mm\\:ss}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating time display: {ex.Message}");
                TimeDisplay.Text = "00:00:00";
            }
        }

        private void UpdateCurrentSubtitle()
        {
            if (VideoPlayer?.Source == null || _subtitles == null || _subtitles.Count == 0) return;

            try
            {
                var currentTime = VideoPlayer.Position;
                var subtitle = _subtitles.Find(s => 
                    currentTime >= s.StartTime && currentTime <= s.EndTime);

                if (subtitle != _currentSubtitle)
                {
                    _currentSubtitle = subtitle;
                    if (subtitle != null)
                    {
                        // Only update selection if editor is not locked
                        if (LockEditorCheckBox == null || !LockEditorCheckBox.IsChecked == true)
                        {
                            SubtitleListView.SelectedItem = subtitle;
                            SubtitleListView.ScrollIntoView(subtitle);
                        }
                        SubtitleOverlay.Text = subtitle.Text;
                    }
                    else
                    {
                        SubtitleOverlay.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't crash
                System.Diagnostics.Debug.WriteLine($"Error updating subtitle: {ex.Message}");
            }
        }

        private void SubtitleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Only update the editing controls if a single item is selected and editor is not locked
            if (SubtitleListView.SelectedItems.Count == 1 && 
                (LockEditorCheckBox == null || !LockEditorCheckBox.IsChecked == true))
            {
                var selectedSubtitle = SubtitleListView.SelectedItem as SubtitleItem;
                if (selectedSubtitle != null)
                {
                    StartTimeTextBox.Text = selectedSubtitle.StartTime.ToString(@"hh\:mm\:ss\,fff");
                    EndTimeTextBox.Text = selectedSubtitle.EndTime.ToString(@"hh\:mm\:ss\,fff");
                    SubtitleTextBox.Text = selectedSubtitle.Text;
                }
            }
            else if (!LockEditorCheckBox?.IsChecked == true)
            {
                StartTimeTextBox.Text = "";
                EndTimeTextBox.Text = "";
                SubtitleTextBox.Text = "";
            }
        }

        private void SyncStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (VideoPlayer?.Source == null) return;

            var selectedSubtitle = SubtitleListView.SelectedItem as SubtitleItem;
            if (selectedSubtitle != null)
            {
                StartTimeTextBox.Text = VideoPlayer.Position.ToString(@"hh\:mm\:ss\,fff");
            }
        }

        private void SyncEndButton_Click(object sender, RoutedEventArgs e)
        {
            if (VideoPlayer?.Source == null) return;

            var selectedSubtitle = SubtitleListView.SelectedItem as SubtitleItem;
            if (selectedSubtitle != null)
            {
                EndTimeTextBox.Text = VideoPlayer.Position.ToString(@"hh\:mm\:ss\,fff");
            }
        }

        private void ApplyChangesButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSubtitle = SubtitleListView.SelectedItem as SubtitleItem;
            if (selectedSubtitle == null)
            {
                CustomMessageBox.Show("Please select a subtitle to edit.", "Warning");
                return;
            }

            try
            {
                // Parse and validate start time
                if (!TimeSpan.TryParse(StartTimeTextBox.Text.Replace(',', '.'), out TimeSpan startTime))
                {
                    throw new FormatException("Invalid start time format");
                }

                // Parse and validate end time
                if (!TimeSpan.TryParse(EndTimeTextBox.Text.Replace(',', '.'), out TimeSpan endTime))
                {
                    throw new FormatException("Invalid end time format");
                }

                // Validate time range
                if (endTime <= startTime)
                {
                    throw new Exception("End time must be greater than start time");
                }

                // Update subtitle
                selectedSubtitle.StartTime = startTime;
                selectedSubtitle.EndTime = endTime;
                selectedSubtitle.Text = SubtitleTextBox.Text.Trim();

                // Sort subtitles after editing times
                SortSubtitles();

                // Refresh the ListView
                SubtitleListView.Items.Refresh();

                // Uncheck the lock checkbox after applying changes
                if (LockEditorCheckBox != null)
                {
                    LockEditorCheckBox.IsChecked = false;
                }

                ShowSuccessMessage("Changes applied successfully!");
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Error applying changes: {ex.Message}", "Error");
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Ignore keyboard shortcuts if any TextBox has focus
            if (e.OriginalSource is TextBox)
            {
                return;
            }

            if (VideoPlayer?.Source == null || _subtitles == null || _subtitles.Count == 0) return;

            try
            {
                var currentTime = VideoPlayer.Position;
                int currentIndex = -1;

                // Find the current subtitle index
                if (_currentSubtitle != null)
                {
                    currentIndex = _subtitles.IndexOf(_currentSubtitle);
                }
                else
                {
                    // If no current subtitle, find the nearest one
                    for (int i = 0; i < _subtitles.Count; i++)
                    {
                        if (_subtitles[i].StartTime > currentTime)
                        {
                            currentIndex = i - 1;
                            break;
                        }
                    }
                }

                switch (e.Key)
                {
                    case Key.A: // Previous subtitle
                        if (currentIndex > 0)
                        {
                            NavigateToSubtitle(_subtitles[currentIndex - 1]);
                        }
                        e.Handled = true;
                        break;

                    case Key.S: // Current subtitle
                        if (currentIndex >= 0 && currentIndex < _subtitles.Count)
                        {
                            NavigateToSubtitle(_subtitles[currentIndex]);
                        }
                        e.Handled = true;
                        break;

                    case Key.D: // Next subtitle
                        if (currentIndex < _subtitles.Count - 1)
                        {
                            NavigateToSubtitle(_subtitles[currentIndex + 1]);
                        }
                        e.Handled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Log($"Error in keyboard navigation: {ex}");
            }
        }

        private void NavigateToSubtitle(SubtitleItem subtitle)
        {
            if (subtitle == null) return;

            try
            {
                // Navigate to the start of the subtitle
                VideoPlayer.Position = subtitle.StartTime;
                TimelineSlider.Value = subtitle.StartTime.TotalSeconds;
                
                // Update display
                UpdateTimeDisplay(VideoPlayer.Position, VideoPlayer.NaturalDuration.TimeSpan);
                
                // Select the subtitle in the list
                SubtitleListView.SelectedItem = subtitle;
                SubtitleListView.ScrollIntoView(subtitle);
                
                Log($"Navigated to subtitle at {subtitle.StartTime}");
            }
            catch (Exception ex)
            {
                Log($"Error navigating to subtitle: {ex}");
            }
        }

        private void FileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _timer.Stop();
            base.OnClosing(e);
        }

        private void ShowPreferences_Click(object sender, RoutedEventArgs e)
        {
            var preferencesWindow = new PreferencesWindow(_showSuccessPopups, _currentFontSize)
            {
                Owner = this
            };

            if (preferencesWindow.ShowDialog() == true)
            {
                _showSuccessPopups = preferencesWindow.ShowSuccessPopups;
                if (_currentFontSize != preferencesWindow.FontSize)
                {
                    _currentFontSize = preferencesWindow.FontSize;
                    UpdateFontSize(_currentFontSize);
                }
            }
        }

        private void UpdateFontSize(double fontSize)
        {
            // Create and apply new styles with updated font sizes
            var baseTextStyle = (Style)FindResource(typeof(TextBlock));
            var newTextStyle = new Style(typeof(TextBlock), baseTextStyle);
            newTextStyle.Setters.Add(new Setter(TextBlock.FontSizeProperty, fontSize));
            Resources[typeof(TextBlock)] = newTextStyle;

            var baseButtonStyle = (Style)FindResource(typeof(Button));
            var newButtonStyle = new Style(typeof(Button), baseButtonStyle);
            newButtonStyle.Setters.Add(new Setter(Button.FontSizeProperty, fontSize));
            Resources[typeof(Button)] = newButtonStyle;

            var baseTextBoxStyle = (Style)FindResource(typeof(TextBox));
            var newTextBoxStyle = new Style(typeof(TextBox), baseTextBoxStyle);
            newTextBoxStyle.Setters.Add(new Setter(TextBox.FontSizeProperty, fontSize));
            Resources[typeof(TextBox)] = newTextBoxStyle;

            var baseMenuStyle = (Style)FindResource(typeof(Menu));
            var newMenuStyle = new Style(typeof(Menu), baseMenuStyle);
            newMenuStyle.Setters.Add(new Setter(Menu.FontSizeProperty, fontSize));
            Resources[typeof(Menu)] = newMenuStyle;

            var baseMenuItemStyle = (Style)FindResource(typeof(MenuItem));
            var newMenuItemStyle = new Style(typeof(MenuItem), baseMenuItemStyle);
            newMenuItemStyle.Setters.Add(new Setter(MenuItem.FontSizeProperty, fontSize));
            Resources[typeof(MenuItem)] = newMenuItemStyle;

            var baseCheckBoxStyle = (Style)FindResource(typeof(CheckBox));
            var newCheckBoxStyle = new Style(typeof(CheckBox), baseCheckBoxStyle);
            newCheckBoxStyle.Setters.Add(new Setter(CheckBox.FontSizeProperty, fontSize));
            Resources[typeof(CheckBox)] = newCheckBoxStyle;

            // Update subtitle overlay font size (slightly larger than UI font)
            SubtitleOverlay.FontSize = fontSize * 1.5;
        }

        private void ShowSuccessMessage(string message)
        {
            if (_showSuccessPopups)
            {
                CustomMessageBox.Show(message, "Success");
            }
        }

        // Window control event handlers
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
