namespace E06_V2;
using Program;
using Google.Cloud.Speech.V1;
using E_CC;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void btnConvertToMP3_Click(object sender, EventArgs e)
    {
        // Convert MP4 to MP3
        MediaConverter converter = new MediaConverter();
        converter.ConvertMp4ToMp3(); // Call the method to create MP3
    }

    private void btnConvertToWAV_Click(object sender, EventArgs e)
    {
        // Convert MP4 to WAV
        MediaConverter converter = new MediaConverter();
        converter.ConvertMp4ToWav(); // Call the method to create WAV
    }

    private void btnTimeCode_Click(object sender, EventArgs e)
    {
        Program.TimeCode();
    }

    private void btnTextToSRT_Click(object sender, EventArgs e)
    {
        TxtToSrtConverter converter = new TxtToSrtConverter();
        converter.ConvertTxtToSrt(null); // Allow the user to select the file
    }

    private void btnTextToSCC_Click(object sender, EventArgs e)
    {
        txt_to_scc converter = new txt_to_scc();
        converter.ConvertTxtToScc(); // Allow the user to select the file
    }

    private void btnTranscribeAPI_Click(object sender, EventArgs e)
    {
        Transcription_API transcription = new Transcription_API();
        transcription.TranscribeAudioToText(null); // Allow the user to select the file
    }
}


