using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using VideoPlayer.Models;

namespace VideoPlayer.Services
{
    public class AudioExportService
    {
        private string GetFFmpegPath()
        {
            // Get the directory where the application executable is located
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            
            // List of possible FFmpeg locations relative to the app directory
            string[] possiblePaths = new[]
            {
                Path.Combine(appDirectory, "FFmpeg", "ffmpeg.exe"),
                Path.Combine(appDirectory, "FFmpeg", "bin", "ffmpeg.exe"),
                Path.Combine(appDirectory, "FFmpeg", "ffmpeg-master-latest-win64-gpl", "bin", "ffmpeg.exe")
            };

            // Try each possible path
            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }
            
            throw new FileNotFoundException(
                "FFmpeg executable not found. Please ensure FFmpeg is properly installed with the application. " +
                "Check the FFmpeg/README.txt file for installation instructions.",
                "ffmpeg.exe");
        }

        public async Task ExportAudioAsync(string videoPath, string outputPath, List<SubtitleItem> subtitles)
        {
            if (string.IsNullOrEmpty(videoPath))
                throw new ArgumentException("Video path cannot be empty", nameof(videoPath));

            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentException("Output path cannot be empty", nameof(outputPath));

            if (subtitles == null || subtitles.Count == 0)
                throw new ArgumentException("No subtitles provided", nameof(subtitles));

            // Sort subtitles by start time
            subtitles.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));

            // Create FFmpeg filter complex command for concatenating audio segments
            var filterComplex = "";
            for (int i = 0; i < subtitles.Count; i++)
            {
                var start = subtitles[i].StartTime.TotalSeconds;
                var duration = (subtitles[i].EndTime - subtitles[i].StartTime).TotalSeconds;
                filterComplex += $"[0:a]atrim={start}:{start + duration},asetpts=PTS-STARTPTS[a{i}];";
            }

            for (int i = 0; i < subtitles.Count; i++)
            {
                filterComplex += $"[a{i}]";
            }
            filterComplex += $"concat=n={subtitles.Count}:v=0:a=1[out]";

            // Build FFmpeg command
            var startInfo = new ProcessStartInfo
            {
                FileName = GetFFmpegPath(),
                Arguments = $"-i \"{videoPath}\" -filter_complex \"{filterComplex}\" -map \"[out]\" \"{outputPath}\"",
                UseShellExecute = false,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = startInfo };
            var tcs = new TaskCompletionSource<bool>();

            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data == null)
                    tcs.TrySetResult(true);
                else
                    Debug.WriteLine($"FFmpeg: {e.Data}");
            };

            process.Start();
            process.BeginErrorReadLine();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                throw new Exception($"FFmpeg exited with code {process.ExitCode}");

            await tcs.Task;
        }
    }
} 