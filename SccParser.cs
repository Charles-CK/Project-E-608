using System;
using System.IO;
using System.Text;

namespace E_CC
{
    class SccParser
    {
        public string[] ParseSccFile(string sccFilePath)
        {
            if (string.IsNullOrEmpty(sccFilePath) || !sccFilePath.EndsWith(".scc", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid SCC file. Please select a valid .scc file.");
                return null;
            }

            string[] lines = File.ReadAllLines(sccFilePath);
            foreach (string line in lines)
            {
                // Parse the SCC line and extract caption data
                string captionData = ParseSccLine(line);
                Console.WriteLine(captionData);
            }
            return lines;
        }

        private string ParseSccLine(string line)
        {
            // Implement SCC line parsing logic here
            // For simplicity, this example just returns the line as-is
            return line;
        }
    }
}


