using System;
using System.Windows.Forms;
using Audio_Transcription;
using E_CC;
using Google.Cloud.Speech.V1;

namespace Program
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new E06_V2.Form1()); // Launch the form  
        }

        /// <summary>
        /// Converts an MP4 file to MP3 format.
        /// </summary>
        public static void ConvertToMP3()
        {
            MediaConverter mediaConverter = new MediaConverter();
            mediaConverter.ConvertMp4ToMp3(); // Call the new method for MP3 conversion
        }

        /// <summary>
        /// Converts an MP4 file to WAV format.
        /// </summary>
        public static void ConvertToWAV()
        {
            MediaConverter mediaConverter = new MediaConverter();
            mediaConverter.ConvertMp4ToWav(); // Call the new method for WAV conversion
        }

        /// <summary>
        /// Generates time codes for audio transcription.
        /// </summary>
        public static void TimeCode()
        {
            Audio_Transcription1 audioToText = new Audio_Transcription1();
            audioToText.GenerateTimeCodes();
        }

        /// <summary>
        /// Converts a text file to SRT format.
        /// </summary>
        public static void TextToSRT()
        {
            TxtToSrtConverter converter = new TxtToSrtConverter();
            converter.ConvertTxtToSrt(null);
        }

        /// <summary>
        /// Converts a text file to SCC format.
        /// </summary>
        public static void TextToSCC()
        {
            txt_to_scc converter = new txt_to_scc();
            converter.ConvertTxtToScc();
        }

        /// <summary>
        /// Transcribes audio to text using the Google Cloud Speech-to-Text API.
        /// </summary>
        public static void TranscribeAPI()
        {
            Transcription_API audioToText = new Transcription_API();
            audioToText.TranscribeAudioToText(null);
        }
    }
}

