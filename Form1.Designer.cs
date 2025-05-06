namespace E06_V2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private System.Windows.Forms.Button btnTimeCode;
        private System.Windows.Forms.Button btnTextToSRT;
        private System.Windows.Forms.Button btnTextToSCC;
        private System.Windows.Forms.Button btnTranscribeAPI;
        private System.Windows.Forms.Button btnConvertToMP3;
        private System.Windows.Forms.Button btnConvertToWAV;
        private System.Windows.Forms.Button btnFormatTranscription; // New button

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTimeCode = new System.Windows.Forms.Button();
            this.btnTextToSRT = new System.Windows.Forms.Button();
            this.btnTextToSCC = new System.Windows.Forms.Button();
            this.btnTranscribeAPI = new System.Windows.Forms.Button();
            this.btnConvertToMP3 = new System.Windows.Forms.Button();
            this.btnConvertToWAV = new System.Windows.Forms.Button();
            this.btnFormatTranscription = new System.Windows.Forms.Button(); // Initialize new button

            // 
            // btnTimeCode
            // 
            this.btnTimeCode.Location = new System.Drawing.Point(20, 20);
            this.btnTimeCode.Name = "btnTimeCode";
            this.btnTimeCode.Size = new System.Drawing.Size(150, 30);
            this.btnTimeCode.Text = "Generate Time Codes";
            this.btnTimeCode.UseVisualStyleBackColor = true;
            this.btnTimeCode.Click += new System.EventHandler(this.btnTimeCode_Click);

            // 
            // btnTextToSRT
            // 
            this.btnTextToSRT.Location = new System.Drawing.Point(20, 60);
            this.btnTextToSRT.Name = "btnTextToSRT";
            this.btnTextToSRT.Size = new System.Drawing.Size(150, 30);
            this.btnTextToSRT.Text = "Convert Text to SRT";
            this.btnTextToSRT.UseVisualStyleBackColor = true;
            this.btnTextToSRT.Click += new System.EventHandler(this.btnTextToSRT_Click);

            // 
            // btnTextToSCC
            // 
            this.btnTextToSCC.Location = new System.Drawing.Point(20, 100);
            this.btnTextToSCC.Name = "btnTextToSCC";
            this.btnTextToSCC.Size = new System.Drawing.Size(150, 30);
            this.btnTextToSCC.Text = "Convert Text to SCC";
            this.btnTextToSCC.UseVisualStyleBackColor = true;
            this.btnTextToSCC.Click += new System.EventHandler(this.btnTextToSCC_Click);

            // 
            // btnTranscribeAPI
            // 
            this.btnTranscribeAPI.Location = new System.Drawing.Point(20, 140);
            this.btnTranscribeAPI.Name = "btnTranscribeAPI";
            this.btnTranscribeAPI.Size = new System.Drawing.Size(150, 30);
            this.btnTranscribeAPI.Text = "Transcribe API";
            this.btnTranscribeAPI.UseVisualStyleBackColor = true;
            this.btnTranscribeAPI.Click += new System.EventHandler(this.btnTranscribeAPI_Click);

            // 
            // btnConvertToMP3
            // 
            this.btnConvertToMP3.Location = new System.Drawing.Point(20, 180);
            this.btnConvertToMP3.Name = "btnConvertToMP3";
            this.btnConvertToMP3.Size = new System.Drawing.Size(150, 30);
            this.btnConvertToMP3.Text = "Convert to MP3";
            this.btnConvertToMP3.UseVisualStyleBackColor = true;
            this.btnConvertToMP3.Click += new System.EventHandler(this.btnConvertToMP3_Click);

            // 
            // btnConvertToWAV
            // 
            this.btnConvertToWAV.Location = new System.Drawing.Point(20, 220);
            this.btnConvertToWAV.Name = "btnConvertToWAV";
            this.btnConvertToWAV.Size = new System.Drawing.Size(150, 30);
            this.btnConvertToWAV.Text = "Convert to WAV";
            this.btnConvertToWAV.UseVisualStyleBackColor = true;
            this.btnConvertToWAV.Click += new System.EventHandler(this.btnConvertToWAV_Click);

            // 
            // btnFormatTranscription
            // 
            this.btnFormatTranscription.Location = new System.Drawing.Point(20, 260); // Position below the other buttons
            this.btnFormatTranscription.Name = "btnFormatTranscription";
            this.btnFormatTranscription.Size = new System.Drawing.Size(150, 30);
            this.btnFormatTranscription.Text = "Format Transcription";
            this.btnFormatTranscription.UseVisualStyleBackColor = true;
            this.btnFormatTranscription.Click += new System.EventHandler(this.btnFormatTranscription_Click);

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(400, 350); // Adjust height to fit the new button
            this.Controls.Add(this.btnTimeCode);
            this.Controls.Add(this.btnTextToSRT);
            this.Controls.Add(this.btnTextToSCC);
            this.Controls.Add(this.btnTranscribeAPI);
            this.Controls.Add(this.btnConvertToMP3);
            this.Controls.Add(this.btnConvertToWAV);
            this.Controls.Add(this.btnFormatTranscription); // Add the new button to the form
            this.Text = "Media Converter";
        }

        #endregion
    }
}

