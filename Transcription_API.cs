using Google.Cloud.Speech.V1;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace E_CC
{
    class Transcription_API
    {
        private FilePath filePath;

        public Transcription_API()
        {
            filePath = new FilePath();
        }

        public void TranscribeAudioToText()
        {
            string audioFilePath = filePath.GetFilePath();
            if (string.IsNullOrEmpty(audioFilePath) || !audioFilePath.EndsWith(".wav"))
            {
                Console.WriteLine("Invalid WAV file path.");
                return;
            }

            string outputFilePath = GetOutputFilePath();
            if (string.IsNullOrEmpty(outputFilePath))
            {
                Console.WriteLine("Invalid output file path.");
                return;
            }

            var speech = SpeechClient.Create();
            var response = speech.Recognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 16000,
                LanguageCode = "en",
                EnableWordTimeOffsets = true
            }, RecognitionAudio.FromFile(audioFilePath));

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
        }



        private string GetOutputFilePath()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return saveFileDialog.FileName;
                }
            }
            return null;
        }



        private string FormatTime(Google.Protobuf.WellKnownTypes.Duration duration)
        {
            var ts = TimeSpan.FromSeconds(duration.Seconds + duration.Nanos / 1e9);
            return $"{ts.Hours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2},{ts.Milliseconds:D3}";
        }
    }

}


