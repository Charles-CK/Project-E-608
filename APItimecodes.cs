using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace EO6_v3
{
    /// <summary>
    /// Processes a transcription output file and replaces "Transcript:" and "Confidence:" lines with time codes.
    /// </summary>
    class APItimecodes
    {
        /// <summary>
        /// Processes the transcription output file and replaces "Transcript:" and "Confidence:" lines with time codes.
        /// </summary>
        /// <param name="inputFilePath">The path to the input transcription file.</param>
        /// <param name="outputFilePath">The path to save the processed file.</param>
        public void ProcessTranscriptionFile(string inputFilePath, string outputFilePath)
        {
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Input file does not exist.");
                return;
            }

            var lines = File.ReadAllLines(inputFilePath);
            var sb = new StringBuilder();

            TimeSpan currentTime = TimeSpan.Zero;
            TimeSpan interval = TimeSpan.FromSeconds(5); // 5-second intervals for time codes
            var wordBuffer = new List<string>(); // Buffer to hold words for grouping

            foreach (var line in lines)
            {
                if (line.StartsWith("Transcript:"))
                {
                    // Extract the transcript text
                    var transcript = line.Replace("Transcript:", "").Trim();
                    var words = transcript.Split(' '); // Split the transcript into words

                    foreach (var word in words)
                    {
                        wordBuffer.Add(word);

                        // If the buffer contains 6 words, process the chunk
                        if (wordBuffer.Count == 6)
                        {
                            // Add top time code
                            var startTime = currentTime;
                            var endTime = currentTime + interval;
                            sb.AppendLine($"{FormatTime(startTime)} --> {FormatTime(endTime)}");

                            // Add the grouped words as a single line
                            sb.AppendLine(string.Join(" ", wordBuffer));

                            // Increment the current time
                            currentTime = endTime;

                            // Clear the buffer
                            wordBuffer.Clear();
                        }
                    }
                }
                else if (line.StartsWith("Confidence:"))
                {
                    // Add an empty line for separation
                    sb.AppendLine();
                }
                else
                {
                    // Preserve other lines as-is
                    sb.AppendLine(line);
                }
            }

            // Process any remaining words in the buffer
            if (wordBuffer.Count > 0)
            {
                var startTime = currentTime;
                var endTime = currentTime + interval;
                sb.AppendLine($"{FormatTime(startTime)} --> {FormatTime(endTime)}");
                sb.AppendLine(string.Join(" ", wordBuffer));
            }

            // Write the processed content to the output file
            File.WriteAllText(outputFilePath, sb.ToString());
            Console.WriteLine($"Processed file saved to: {outputFilePath}");
        }

        /// <summary>
        /// Formats a TimeSpan into the format hh:mm:ss,fff.
        /// </summary>
        /// <param name="time">The TimeSpan to format.</param>
        /// <returns>A formatted time string.</returns>
        private string FormatTime(TimeSpan time)
        {
            return $"{time.Hours:D2}:{time.Minutes:D2}:{time.Seconds:D2},{time.Milliseconds:D3}";
        }
    }
}
