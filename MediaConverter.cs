using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace E_CC
{
    class MediaConverter
    {
        private FilePath filePath;

        public MediaConverter()
        {
            filePath = new FilePath();
        }

        public void ConvertMp4ToMp3ToWav()
        {
            string mp4Path = filePath.GetFilePath();
            if (string.IsNullOrEmpty(mp4Path) || !mp4Path.EndsWith(".mp4"))
            {
                Console.WriteLine("Invalid MP4 file path.");
                return;
            }

            string outputFolder = GetOutputFolderPath();
            if (string.IsNullOrEmpty(outputFolder))
            {
                Console.WriteLine("Invalid output folder path.");
                return;
            }

            string mp3Path = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(mp4Path) + ".mp3");
            string wavPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(mp4Path) + ".wav");

            // Convert MP4 to MP3
            RunFFmpegCommand($"-i \"{mp4Path}\" \"{mp3Path}\"");

            // Convert MP3 to WAV in PCM 16-bit, mono, 16kHz
            RunFFmpegCommand($"-i \"{mp3Path}\" -ac 1 -ar 16000 -sample_fmt s16 \"{wavPath}\"");

            Console.WriteLine($"Conversion complete: {wavPath}");
        }

        public void ConvertMovToMp3ToWav()
        {
            string movPath = filePath.GetFilePath();
            if (string.IsNullOrEmpty(movPath) || !movPath.EndsWith(".mov"))
            {
                Console.WriteLine("Invalid MOV file path.");
                return;
            }

            string outputFolder = GetOutputFolderPath();
            if (string.IsNullOrEmpty(outputFolder))
            {
                Console.WriteLine("Invalid output folder path.");
                return;
            }

            string mp3Path = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(movPath) + ".mp3");
            string wavPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(movPath) + ".wav");

            // Convert MOV to MP3
            RunFFmpegCommand($"-i \"{movPath}\" \"{mp3Path}\"");

            // Convert MP3 to WAV in PCM 16-bit, mono, 16kHz
            RunFFmpegCommand($"-i \"{mp3Path}\" -ac 1 -ar 16000 -sample_fmt s16 \"{wavPath}\"");

            Console.WriteLine($"Conversion complete: {wavPath}");
        }

        private string GetOutputFolderPath()
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    return folderBrowserDialog.SelectedPath;
                }
            }
            return null;
        }

        private void RunFFmpegCommand(string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                // Corrected to "FFmpeg" (the official spelling)
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
