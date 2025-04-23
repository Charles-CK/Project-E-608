using System;
using System.Windows.Forms;

namespace E_CC
{
    class FilePath
    {
        public string GetFilePath()
        {
            // Allows the user to pick a file through the file explorer for use in other objects
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    return openFileDialog.FileName;
                }
            }
            return null;
        }

        public string GetSaveFilePath()
        {
            // Allows the user to pick a save location through the file explorer
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;
                saveFileDialog.Title = "Select Output File Location";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of the specified save location
                    return saveFileDialog.FileName;
                }
            }
            return null;
        }
    }
}
