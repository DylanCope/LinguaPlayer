using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VideoPlayer.Models;

namespace VideoPlayer.Services
{
    public class SubtitleService
    {
        public async Task<List<SubtitleItem>> LoadSubtitlesAsync(string filePath)
        {
            var subtitles = new List<SubtitleItem>();
            var lines = await File.ReadAllLinesAsync(filePath);
            
            for (int i = 0; i < lines.Length;)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    i++;
                    continue;
                }

                var subtitle = new SubtitleItem();
                
                // Parse index
                if (int.TryParse(lines[i], out int index))
                {
                    subtitle.Index = index;
                    i++;
                }
                else continue;

                // Parse timestamp
                if (i < lines.Length)
                {
                    var timeParts = lines[i].Split(" --> ");
                    if (timeParts.Length == 2)
                    {
                        subtitle.StartTime = ParseTimeStamp(timeParts[0]);
                        subtitle.EndTime = ParseTimeStamp(timeParts[1]);
                    }
                    i++;
                }

                // Parse text
                var textBuilder = new System.Text.StringBuilder();
                while (i < lines.Length && !string.IsNullOrWhiteSpace(lines[i]))
                {
                    textBuilder.AppendLine(lines[i]);
                    i++;
                }
                subtitle.Text = textBuilder.ToString().Trim();
                
                subtitles.Add(subtitle);
            }

            return subtitles;
        }

        public async Task SaveSubtitlesAsync(string filePath, List<SubtitleItem> subtitles)
        {
            using var writer = new StreamWriter(filePath, false);
            foreach (var subtitle in subtitles)
            {
                await writer.WriteLineAsync(subtitle.Index.ToString());
                await writer.WriteLineAsync(subtitle.FormattedTime);
                await writer.WriteLineAsync(subtitle.Text);
                await writer.WriteLineAsync();
            }
        }

        private TimeSpan ParseTimeStamp(string timestamp)
        {
            var regex = new Regex(@"(\d{2}):(\d{2}):(\d{2}),(\d{3})");
            var match = regex.Match(timestamp);
            
            if (match.Success)
            {
                return new TimeSpan(
                    0,
                    int.Parse(match.Groups[1].Value), // hours
                    int.Parse(match.Groups[2].Value), // minutes
                    int.Parse(match.Groups[3].Value), // seconds
                    int.Parse(match.Groups[4].Value)  // milliseconds
                );
            }
            
            return TimeSpan.Zero;
        }
    }
} 