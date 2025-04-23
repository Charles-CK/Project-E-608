using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace E_CC
{
    /// <summary>
    /// Class to convert a .txt file to .scc format.
    /// </summary>
    class txt_to_scc
    {
        /// <summary>
        /// Converts a .txt file to .scc format.
        /// </summary>
        public void ConvertTxtToScc()
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

            // Let the user pick the output .scc file location
            string sccFilePath = filePathObj.GetSaveFilePath();
            if (string.IsNullOrEmpty(sccFilePath))
            {
                Console.WriteLine("No output file selected. Operation canceled.");
                return;
            }

            // Ensure the file has a .scc extension
            if (!sccFilePath.EndsWith(".scc", StringComparison.OrdinalIgnoreCase))
            {
                sccFilePath = Path.ChangeExtension(sccFilePath, ".scc");
            }

            // Read the .txt file
            string[] lines = File.ReadAllLines(txtFilePath);

            // Prepare the .scc file content
            List<string> sccLines = new List<string>();
            TimeSpan startTime = TimeSpan.Zero;
            TimeSpan interval = TimeSpan.FromSeconds(1); // 1-second intervals

            foreach (string line in lines)
            {
                string sccTimeCode = FormatSccTimeCode(startTime);
                sccLines.Add(sccTimeCode);
                sccLines.Add(ConvertTextToSccFormat(line));
                sccLines.Add(""); // Add an empty line for separation

                startTime += interval;
            }

            // Write the .scc file
            File.WriteAllLines(sccFilePath, sccLines);
            Console.WriteLine($"SCC file saved to: {sccFilePath}");
        }

        private string FormatSccTimeCode(TimeSpan ts)
        {
            // SCC time code format: hh:mm:ss:ff (frames)
            int frames = (int)(ts.Milliseconds / (1000 / 30)); // Assuming 30 fps
            return $"{ts.Hours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}:{frames:D2}";
        }

        private string ConvertTextToSccFormat(string text)
        {
            // Convert text to SCC format (simple example)
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                sb.AppendFormat("{0:X2} ", (int)c);
            }
            return sb.ToString().Trim();
        }
    }
}
