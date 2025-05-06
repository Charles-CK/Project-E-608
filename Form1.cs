namespace E06_V2;
using Program;
using Google.Cloud.Speech.V1;
using E_CC;
using EO6_v3; // Include the namespace for APItimecodes

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

    private void btnFormatTranscription_Click(object sender, EventArgs e)
    {
        // Format the transcription output using APItimecodes
        APItimecodes apiTimecodes = new APItimecodes();

        // Allow the user to select the input file
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.Title = "Select Transcription Output File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string inputFilePath = openFileDialog.FileName;

                // Allow the user to select the output file location
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    saveFileDialog.Title = "Save Formatted Transcription File";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string outputFilePath = saveFileDialog.FileName;

                        // Process the transcription file
                        apiTimecodes.ProcessTranscriptionFile(inputFilePath, outputFilePath);
                    }
                }
            }
        }
    }
}
