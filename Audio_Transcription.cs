using System;
using System.IO;
using System.Text;
using E_CC;  // for your FilePath class

namespace Audio_Transcription
{
    //Right now this is a test class just to get a .txt with time codes for testing
    class Audio_Transcription
    {
        public void GenerateTimeCodes()
        {
            // Prepare the output .txt file on desktop
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string outputTxtPath = Path.Combine(desktop, "TimeCodesOutput.txt");

            // Generate time codes for 5 minutes
            StringBuilder sb = new StringBuilder();
            TimeSpan duration = TimeSpan.FromMinutes(5);
            TimeSpan interval = TimeSpan.FromSeconds(1); // 1-second intervals

            for (TimeSpan ts = TimeSpan.Zero; ts < duration; ts += interval)
            {
                sb.AppendLine(FormatTime(ts));
                sb.AppendLine("This is a test");
                sb.AppendLine();
            }

            // Write the output to a .txt file on the desktop
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

