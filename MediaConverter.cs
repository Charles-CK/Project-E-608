using System;
using System.Diagnostics;
using System.IO;

namespace E_CC
{
    class MediaConverter
    {
        private FilePath filePath;

        public MediaConverter()
        {
            filePath = new FilePath();
        }

        /// <summary>
        /// Converts an MP4 file to MP3 format.
        /// </summary>
        public void ConvertMp4ToMp3()
        {
            // Let the user pick the input MP4 file
            string mp4Path = filePath.GetFilePath();
            if (string.IsNullOrEmpty(mp4Path) || !mp4Path.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid MP4 file path.");
                return;
            }

            // Let the user pick the output MP3 file location
            string mp3Path = filePath.GetSaveFilePath();
            if (string.IsNullOrEmpty(mp3Path))
            {
                Console.WriteLine("No output file selected. Operation canceled.");
                return;
            }

            // Ensure the file has an .mp3 extension
            if (!mp3Path.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                mp3Path = Path.ChangeExtension(mp3Path, ".mp3");
            }

            // Convert MP4 to MP3
            RunFFmpegCommand($"-i \"{mp4Path}\" \"{mp3Path}\"");
            Console.WriteLine($"MP3 file created: {mp3Path}");
        }

        /// <summary>
        /// Converts an MP4 file to WAV format.
        /// </summary>
        public void ConvertMp4ToWav()
        {
            // Let the user pick the input MP4 file
            string mp4Path = filePath.GetFilePath();
            if (string.IsNullOrEmpty(mp4Path) || !mp4Path.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid MP4 file path.");
                return;
            }

            // Let the user pick the output WAV file location
            string wavPath = filePath.GetSaveFilePath();
            if (string.IsNullOrEmpty(wavPath))
            {
                Console.WriteLine("No output file selected. Operation canceled.");
                return;
            }

            // Ensure the file has a .wav extension
            if (!wavPath.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
            {
                wavPath = Path.ChangeExtension(wavPath, ".wav");
            }

            // Convert MP4 to WAV in PCM 16-bit, mono, 16kHz
            RunFFmpegCommand($"-i \"{mp4Path}\" -ac 1 -ar 16000 -sample_fmt s16 \"{wavPath}\"");
            Console.WriteLine($"WAV file created: {wavPath}");
        }

        /// <summary>
        /// Runs an FFmpeg command with the specified arguments.
        /// </summary>
        /// <param name="arguments">The FFmpeg command arguments.</param>
        private void RunFFmpegCommand(string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "FFmpeg",
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }
        }
    }
}

