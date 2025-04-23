using System;
using System.IO;
using System.Text;
using E_CC;  // for your FilePath class

namespace Audio_Transcription
{
    // Updated class to generate time codes in the desired format
    class Audio_Transcription1
    {
        public void GenerateTimeCodes()
        {
            FilePath filePath = new FilePath();

            // Let the user pick the output file location
            string outputTxtPath = filePath.GetSaveFilePath();
            if (string.IsNullOrEmpty(outputTxtPath))
            {
                Console.WriteLine("No output file selected. Operation canceled.");
                return;
            }

            // Generate time codes for 5 minutes
            StringBuilder sb = new StringBuilder();
            TimeSpan duration = TimeSpan.FromMinutes(5);
            TimeSpan interval = TimeSpan.FromSeconds(2); // 2-second intervals
            int index = 1;

            for (TimeSpan ts = TimeSpan.Zero; ts < duration; ts += interval)
            {
                TimeSpan endTime = ts + interval;

                // Add index
                sb.AppendLine(index.ToString());

                // Add time range
                sb.AppendLine($"{FormatTime(ts)} --> {FormatTime(endTime)}");

                // Add placeholder text
                sb.AppendLine("This is a test");

                // Add an empty line for separation
                sb.AppendLine();

                index++;
            }

            // Write the output to the selected file
            File.WriteAllText(outputTxtPath, sb.ToString());
            Console.WriteLine($"Time codes saved to: {outputTxtPath}");
        }

        // Simple time formatting for timestamps
        private string FormatTime(TimeSpan ts)
        {
            return ts.ToString(@"hh\:mm\:ss\,fff");
        }
    }
}
