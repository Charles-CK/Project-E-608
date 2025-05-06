# User Guide: E06 Media and Transcription Tool

## **Overview**
This program is a versatile media and transcription tool designed to:
- Convert media files (e.g., MP4 to MP3/WAV).
- Transcribe audio files using the Google Cloud Speech-to-Text API.
- Format transcription outputs into `.srt` or `.scc` subtitle formats.
- Generate time codes for transcription files.
- Process and format text files for subtitle creation.

The program features a user-friendly interface with buttons for each functionality.

---

## **System Requirements**
- **.NET Version**: .NET 8
- **Dependencies**:
  - Google Cloud Speech-to-Text API (requires an API key). [API Info](https://cloud.google.com/apis)
  - FFmpeg (for audio conversion). [Download FFmpeg](https://ffmpeg.org/download.html)
- **Supported File Formats**:
  - **Input**: `.mp4`, `.mp3`, `.wav`, `.txt`
  - **Output**: `.mp3`, `.wav`, `.srt`, `.scc`

---

## **Features and How to Use Them**

### **Convert MP4 to MP3**
- **Purpose**: Converts an MP4 video file to an MP3 audio file.
- **How to Use**:
  1. Click the `Convert to MP3` button.
  2. Select the MP4 file you want to convert.
  3. Choose the destination folder for the MP3 file.
  4. The program will process the file and save the MP3.

### **Convert MP4 to WAV**
- **Purpose**: Converts an MP4 video file to a WAV audio file.
- **How to Use**:
  1. Click the `Convert to WAV` button.
  2. Select the MP4 file you want to convert.
  3. Choose the destination folder for the WAV file.
  4. The program will process the file and save the WAV.

### **Transcribe Audio Using Google Cloud API**
- **Purpose**: Transcribes audio files to text using the Google Cloud Speech-to-Text API.
- **How to Use**:
  1. Click the `Transcribe API` button.
  2. Select the audio file you want to transcribe (must be in WAV format).
  3. Choose the destination folder for the transcription output.
  4. The program will process the file and save the transcription as a `.txt` file.
- **Prerequisites**:
  - Ensure the Google Cloud API key (`APIKey.json`) is configured correctly.
  - The audio file must be in Linear16 WAV format.

### **Format Transcription with Time Codes**
- **Purpose**: Processes transcription output files and adds time codes for each section.
- **How to Use**:
  1. Click the `Format Transcription` button.
  2. Select the transcription `.txt` file from the API.
  3. Choose the destination folder for the formatted file.
  4. The program will process the file and save it with time codes.

### **Convert Text to SRT**
- **Purpose**: Converts a `.txt` file to `.srt` subtitle format.
- **How to Use**:
  1. Click the `Convert Text to SRT` button.
  2. Select the `.txt` file you want to convert.
  3. Choose the destination folder for the `.srt` file.
  4. The program will process the file and save it as `.srt`.

### **Convert Text to SCC**
- **Purpose**: Converts a `.txt` file to `.scc` subtitle format.
- **How to Use**:
  1. Click the `Convert Text to SCC` button.
  2. Select the `.txt` file you want to convert.
  3. Choose the destination folder for the `.scc` file.
  4. The program will process the file and save it as `.scc`.

---

## **Common Issues and Troubleshooting**

### **Google Cloud API Errors**
- **Issue**: Transcription fails due to API errors.
- **Solution**:
  - Ensure the `APIKey.json` file is correctly configured.
  - Verify that the audio file is in Linear16 WAV format.

### **FFmpeg Not Found**
- **Issue**: Media conversion fails.
- **Solution**:
  - Ensure FFmpeg is installed and added to the system's PATH.

### **Incorrect File Formats**
- **Issue**: The program does not process the selected file.
- **Solution**:
  - Ensure the input file matches the required format (e.g., `.mp4`, `.wav`, `.txt`).

### **Duplicate Time Codes in SRT**
- **Issue**: Duplicate time codes appear in the `.srt` file.
- **Solution**:
  - Use the updated `TxtToSrtConverter` class to ensure proper formatting.

---

Let me know if you need further adjustments or additional sections!
