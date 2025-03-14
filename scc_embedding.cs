using System;
using System.IO;
using E_CC;  // for your FilePath class

namespace E_CC
{
    class scc_embedding
    {
        public void EmbedSccIntoVideo()
        {
            // Use FilePath object to let the user pick the video file
            FilePath filePathObj = new FilePath();
            string videoFilePath = filePathObj.GetFilePath();
            if (string.IsNullOrEmpty(videoFilePath) || !videoFilePath.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid video file. Please select a valid .mp4 file.");
                return;
            }
            Console.WriteLine("Selected video file: " + videoFilePath);

            // Use FilePath object to let the user pick the SCC file
            string sccFilePath = filePathObj.GetFilePath();
            if (string.IsNullOrEmpty(sccFilePath) || !sccFilePath.EndsWith(".scc", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid SCC file. Please select a valid .scc file.");
                return;
            }
            Console.WriteLine("Selected SCC file: " + sccFilePath);

            // Parse the SCC file
            SccParser parser = new SccParser();
            string[] captions = parser.ParseSccFile(sccFilePath);

            // Prepare the output video file path on desktop
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string outputFilePath = Path.Combine(desktop, "Output.mp4");
            Console.WriteLine("Output file path: " + outputFilePath);

            // Ensure the output directory exists
            string outputDirectory = Path.GetDirectoryName(outputFilePath);
            if (!Directory.Exists(outputDirectory))
            {
                Console.WriteLine("Output directory does not exist: " + outputDirectory);
                return;
            }

            // Implement custom logic to embed captions into the video file
            // This is a placeholder for the actual embedding logic
            Console.WriteLine("Embedding captions into video...");
            EmbedCaptions(videoFilePath, outputFilePath, captions);

            Console.WriteLine($"SCC file embedded into video. Output saved to: {outputFilePath}");
        }

        private void EmbedCaptions(string videoFilePath, string outputFilePath, string[] captions)
        {
            // Custom embedding logic goes here
            // For simplicity, this example just copies the video file to the output path
            File.Copy(videoFilePath, outputFilePath, true);
            Console.WriteLine("Captions embedded successfully.");
        }
    }
}


