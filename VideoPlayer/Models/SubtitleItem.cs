using System;

namespace VideoPlayer.Models
{
    public class SubtitleItem
    {
        public int Index { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
        
        public string FormattedTime => $"{FormatTimeSpan(StartTime)} --> {FormatTimeSpan(EndTime)}";
        
        private string FormatTimeSpan(TimeSpan time)
        {
            return $"{time.Hours:D2}:{time.Minutes:D2}:{time.Seconds:D2},{time.Milliseconds:D3}";
        }
    }
} 