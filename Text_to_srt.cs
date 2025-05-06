using System;
using System.Collections.Generic;
using System.IO;

namespace E_CC
{
    /// <summary>
    /// Class to convert a .txt file to .srt format while ensuring proper formatting.
    /// </summary>
    class TxtToSrtConverter
    {
        /// <summary>
        /// Converts a .txt file to .srt format while ensuring proper formatting.
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
            int index = 1;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (string.IsNullOrWhiteSpace(line)) // Skip empty lines
                    continue;

                if (line.Contains("-->")) // Time code line
                {
                    // Add the section number
                    srtLines.Add(index.ToString());
                    index++;

                    // Add the time code
                    srtLines.Add(line);

                    // Add the text (next line)
                    if (i + 1 < lines.Length && !string.IsNullOrWhiteSpace(lines[i + 1]))
                    {
                        srtLines.Add(lines[i + 1].Trim());
                        i++; // Skip the next line since it's already processed
                    }

                    // Add an empty line for separation
                    srtLines.Add("");
                }
            }

            // Write the .srt file
            File.WriteAllLines(srtFilePath, srtLines);
            Console.WriteLine($"SRT file saved to: {srtFilePath}");
        }
    }
}
