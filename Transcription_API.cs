using Google.Cloud.Speech.V1;
using Google.Cloud.Storage.V1;
using Google.Api.Gax; // Ensure this namespace is included for PollSettings
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
                    LogErrorToDesktop(new Exception("No file selected."));
                    return;
                }
                Console.WriteLine($"Selected file: {inputFilePath}");
                Console.WriteLine($"File exists: {File.Exists(inputFilePath)}");

                // Set and verify Google Cloud credentials
                string credentialsPath = APIkey.json";
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);
                Console.WriteLine($"Credentials path: {credentialsPath}");

                if (string.IsNullOrEmpty(credentialsPath) || !File.Exists(credentialsPath))
                {
                    Console.WriteLine("Google Cloud credentials are not set or the file does not exist.");
                    LogErrorToDesktop(new Exception("Google Cloud credentials are not set or the file does not exist."));
                    return;
                }

                // Check if the file is in the correct format
                string audioFilePath = inputFilePath;
                if (Path.GetExtension(inputFilePath).ToLower() != ".wav")
                {
                    // Convert the file to Linear16 WAV format using FFmpeg
                    string convertedAudioPath = Path.Combine(Path.GetDirectoryName(inputFilePath), "converted_audio.wav");
                    Console.WriteLine("Converting audio to Linear16 WAV format...");
                    var process = new System.Diagnostics.Process
                    {
                        StartInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = "ffmpeg",
                            Arguments = $"-i \"{inputFilePath}\" -ar 16000 -ac 1 -f wav \"{convertedAudioPath}\"",
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                    };
                    process.Start();
                    process.WaitForExit();

                    if (!File.Exists(convertedAudioPath))
                    {
                        Console.WriteLine("Failed to convert audio to Linear16 WAV format.");
                        LogErrorToDesktop(new Exception("Failed to convert audio to Linear16 WAV format."));
                        return;
                    }
                    audioFilePath = convertedAudioPath;
                    Console.WriteLine($"Audio converted to: {audioFilePath}");
                }

                // Upload the audio file to Google Cloud Storage
                string bucketName = ; // Replace with your GCS bucket name
                string gcsUri = UploadToGcs(audioFilePath, bucketName);

                // Use FilePath object to let the user pick the output file path
                string outputFilePath = filePath.GetSaveFilePath();
                if (string.IsNullOrEmpty(outputFilePath))
                {
                    Console.WriteLine("No output file selected. Operation canceled.");
                    LogErrorToDesktop(new Exception("No output file selected. Operation canceled."));
                    return;
                }
                Console.WriteLine($"Output file path: {outputFilePath}");

                // Transcribe the audio file using the GCS URI
                var speech = SpeechClient.Create();
                var longRunningResponse = speech.LongRunningRecognize(new LongRunningRecognizeRequest
                {
                    Config = new RecognitionConfig
                    {
                        Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                        SampleRateHertz = 16000,
                        LanguageCode = "en-US"
                    },
                    Audio = RecognitionAudio.FromStorageUri(gcsUri) // Use GCS URI
                });

                // Add timeout settings for the transcription process
                longRunningResponse = longRunningResponse.PollUntilCompleted(
                    new PollSettings(
                        Expiration.FromTimeout(TimeSpan.FromMinutes(2)), // Timeout after 2 minutes
                        TimeSpan.FromSeconds(5) // Poll every 5 seconds
                    )
                );

                var response = longRunningResponse.Result;

                // Save the transcription to a .txt file with time codes and debugging
                var sb = new StringBuilder();
                if (response.Results.Count == 0)
                {
                    Console.WriteLine("No transcription results returned by the API.");
                    File.WriteAllText(outputFilePath, "No transcription results returned by the API.");
                    LogErrorToDesktop(new Exception("No transcription results returned by the API."));
                    return;
                }

                // Log the full response for debugging
                Console.WriteLine("Processing transcription results...");
                foreach (var result in response.Results)
                {
                    foreach (var alternative in result.Alternatives)
                    {
                        if (alternative.Words.Count > 0)
                        {
                            foreach (var wordInfo in alternative.Words)
                            {
                                var startTime = wordInfo.StartTime;
                                var endTime = wordInfo.EndTime;
                                var word = wordInfo.Word;

                                // Append time codes and the word to the StringBuilder
                                sb.AppendLine($"{FormatTime(startTime)} --> {FormatTime(endTime)}");
                                sb.AppendLine(word);
                                sb.AppendLine(); // Add an empty line for separation
                            }
                        }
                        else
                        {
                            // If no word-level information is available, log the transcript
                            sb.AppendLine($"Transcript: {alternative.Transcript}");
                            sb.AppendLine($"Confidence: {alternative.Confidence}");
                            sb.AppendLine(); // Add an empty line for separation
                        }
                    }
                }

                // Write the transcription results to the output file
                File.WriteAllText(outputFilePath, sb.ToString());
                Console.WriteLine($"Transcription saved to: {outputFilePath}");


            }
            catch (Google.GoogleApiException apiEx)
            {
                Console.WriteLine($"Google API Error: {apiEx.Message}");
                LogErrorToDesktop(apiEx);
            }
            catch (Exception ex)
            {
                LogErrorToDesktop(ex);
            }
            Console.WriteLine("Transcription completed.");
        }

        /// <summary>
        /// Uploads a file to Google Cloud Storage and returns the GCS URI.
        /// </summary>
        /// <param name="localFilePath">The local file path.</param>
        /// <param name="bucketName">The GCS bucket name.</param>
        /// <returns>The GCS URI of the uploaded file.</returns>
        private string UploadToGcs(string localFilePath, string bucketName)
        {
            try
            {
                var storageClient = StorageClient.Create();
                string objectName = Path.GetFileName(localFilePath);
                using (var fileStream = File.OpenRead(localFilePath))
                {
                    storageClient.UploadObject(bucketName, objectName, null, fileStream);
                    Console.WriteLine($"Uploaded {localFilePath} to gs://{bucketName}/{objectName}");
                }
                return $"gs://{bucketName}/{objectName}";
            }
            catch (Exception ex)
            {
                LogErrorToDesktop(ex);
                throw;
            }
        }

        /// <summary>
        /// Logs an exception to a .txt file on the desktop.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        private void LogErrorToDesktop(Exception ex)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string logFilePath = Path.Combine(desktopPath, "Transcription_Errors.txt");

                using (var writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] An error occurred:");
                    writer.WriteLine($"Message: {ex.Message}");
                    writer.WriteLine($"Stack Trace: {ex.StackTrace}");
                    writer.WriteLine(new string('-', 80));
                }

                Console.WriteLine($"Error logged to: {logFilePath}");
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"Failed to log error: {logEx.Message}");
            }
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

