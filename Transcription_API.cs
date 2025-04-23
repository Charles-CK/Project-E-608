using Google.Cloud.Speech.V1;
using System;
using System.IO;
using System.Text;

namespace E_CC
{
    /// <summary>
    /// Transcription API class that transcribes audio files to text using the Google Cloud Speech-to-Text API.
    /// </summary>
    class Transcription_API
    {
        private FilePath filePath;

        public Transcription_API()
        {
            filePath = new FilePath();
        }

        /// <summary>
        /// Transcribes the audio file to text and saves the output to a .txt file.
        /// </summary>
        public void TranscribeAudioToText(object value)
        {
            Console.WriteLine("Starting transcription...");
            try
            {
                // Use FilePath object to let the user pick the input file
                string inputFilePath = filePath.GetFilePath();
                if (string.IsNullOrEmpty(inputFilePath))
                {
                    Console.WriteLine("No file selected.");
                    return;
                }
                Console.WriteLine($"Selected file: {inputFilePath}");
                Console.WriteLine($"File exists: {File.Exists(inputFilePath)}");

                // Verify Google Cloud credentials
                string credentialsPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
                Console.WriteLine($"Google credentials path: {credentialsPath}");
                if (string.IsNullOrEmpty(credentialsPath) || !File.Exists(credentialsPath))
                {
                    Console.WriteLine("Google Cloud credentials are not set or invalid.");
                    return;
                }

                // Determine if the file is a video or audio file
                string audioFilePath = inputFilePath;
                if (Path.GetExtension(inputFilePath).ToLower() == ".mp4")
                {
                    // Extract audio from video using FFmpeg
                    string extractedAudioPath = Path.Combine(Path.GetDirectoryName(inputFilePath), "extracted_audio.wav");
                    Console.WriteLine("Extracting audio from video...");
                    var process = new System.Diagnostics.Process
                    {
                        StartInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = "ffmpeg",
                            Arguments = $"-i \"{inputFilePath}\" -ar 16000 -ac 1 -f wav \"{extractedAudioPath}\"",
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                    };
                    process.Start();
                    process.WaitForExit();

                    if (!File.Exists(extractedAudioPath))
                    {
                        Console.WriteLine("Failed to extract audio from video.");
                        return;
                    }
                    audioFilePath = extractedAudioPath;
                    Console.WriteLine($"Audio extracted to: {audioFilePath}");
                }

                // Use FilePath object to let the user pick the output file path
                string outputFilePath = filePath.GetSaveFilePath();
                if (string.IsNullOrEmpty(outputFilePath))
                {
                    Console.WriteLine("No output file selected. Operation canceled.");
                    return;
                }
                Console.WriteLine($"Output file path: {outputFilePath}");

                // Transcribe the audio file
                var speech = SpeechClient.Create();
                var longRunningResponse = speech.LongRunningRecognize(new LongRunningRecognizeRequest
                {
                    Config = new RecognitionConfig
                    {
                        Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                        SampleRateHertz = 16000,
                        LanguageCode = "en"
                    },
                    Audio = RecognitionAudio.FromFile(audioFilePath)
                });

                longRunningResponse = longRunningResponse.PollUntilCompleted();
                var response = longRunningResponse.Result;

                if (response.Results.Count == 0)
                {
                    Console.WriteLine("No transcription results returned by the API.");
                    return;
                }

                // Save the transcription to a .txt file
                var sb = new StringBuilder();
                foreach (var result in response.Results)
                {
                    foreach (var alternative in result.Alternatives)
                    {
                        foreach (var wordInfo in alternative.Words)
                        {
                            var startTime = wordInfo.StartTime;
                            var endTime = wordInfo.EndTime;
                            var word = wordInfo.Word;

                            sb.AppendLine($"{FormatTime(startTime)} --> {FormatTime(endTime)}");
                            sb.AppendLine(word);
                            sb.AppendLine();
                        }
                    }
                }

                File.WriteAllText(outputFilePath, sb.ToString());
                Console.WriteLine($"Transcription saved to: {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            Console.WriteLine("Transcription completed.");
        }


        /// <summary>
        /// Formats the time in the format hh:mm:ss,fff.
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        private string FormatTime(Google.Protobuf.WellKnownTypes.Duration duration)
        {
            var ts = TimeSpan.FromSeconds(duration.Seconds + duration.Nanos / 1e9);
            return $"{ts.Hours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2},{ts.Milliseconds:D3}";
        }
    }
}
