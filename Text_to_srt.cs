using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace E_CC
{
    /// <summary>
    /// Class to convert a .txt file to .srt format.
    /// </summary>
    class TxtToSrtConverter
    {
        /// <summary>
        /// Converts a .txt file to .srt format.
        /// </summary>
        public void ConvertTxtToSrt(string selectedTxtFilePath)
        {
            // Use FilePath object to let the user pick the .txt file
            FilePath filePathObj = new FilePath();
            string txtFilePath = filePathObj.GetFilePath();
            if (string.IsNullOrEmpty(txtFilePath) || !txtFilePath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid TXT file. Please select a valid .txt file.");
                return;
            }
            Console.WriteLine("Selected TXT file: " + txtFilePath);

            // Let the user pick the output .srt file location
            string srtFilePath = filePathObj.GetSaveFilePath();
            if (string.IsNullOrEmpty(srtFilePath))
            {
                Console.WriteLine("No output file selected. Operation canceled.");
                return;
            }

            // Ensure the file has a .srt extension
            if (!srtFilePath.EndsWith(".srt", StringComparison.OrdinalIgnoreCase))
            {
                srtFilePath = Path.ChangeExtension(srtFilePath, ".srt");
            }

            // Read the .txt file
            string[] lines = File.ReadAllLines(txtFilePath);

            // Prepare the .srt file content
            List<string> srtLines = new List<string>();
            TimeSpan startTime = TimeSpan.Zero;
            TimeSpan interval = TimeSpan.FromSeconds(2); // 2-second intervals
            int index = 1;

            foreach (string line in lines)
            {
                string srtTimeCode = FormatSrtTimeCode(startTime, startTime + interval);
                srtLines.Add(index.ToString());
                srtLines.Add(srtTimeCode);
                srtLines.Add(line);
                srtLines.Add(""); // Add an empty line for separation

                startTime += interval;
                index++;
            }

            // Write the .srt file
            File.WriteAllLines(srtFilePath, srtLines);
            Console.WriteLine($"SRT file saved to: {srtFilePath}");
        }

        /// <summary>
        /// Formats the time code for an .srt file.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private string FormatSrtTimeCode(TimeSpan start, TimeSpan end)
        {
            return $"{FormatTime(start)} --> {FormatTime(end)}";
        }

        /// <summary>
        /// Formats a TimeSpan object as a string.
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        private string FormatTime(TimeSpan ts)
        {
            return ts.ToString(@"hh\:mm\:ss\,fff");
        }
    }
}
