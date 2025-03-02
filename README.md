# E608 Captioning Tool

## Introduction

This open-source Windows-based application simplifies the process of embedding Closed Caption (CC) metadata into .MOV video files in the E608 SCC format, which is commonly used in TV broadcasting. Additionally, the tool can convert raw .WAV audio files into transcribed .TXT files with timestamps, allowing users to edit and convert them into the SCC format for embedding captions into video files. This tool is designed to help media producers, content creators, and broadcasters streamline their captioning workflow.

## Features

- **Audio Transcription**: Automatically transcribes .WAV audio files into .TXT files with timestamps, enabling the creation of a basic caption draft.
- **Text Editing & Synchronization**: Users can edit transcriptions through an intuitive GUI, ensuring captions are accurate and properly synchronized with the video content.
- **SCC File Creation**: Converts edited text into the E608 SCC format, which is used for embedding captions in broadcast media.
- **MOV File Captioning**: Embeds the generated SCC file into .MOV video files, ensuring captions are properly displayed during playback.

## Documentation

Here are some useful resources related to the project:

1. **Apple .MOV Developer Documentation**  
   [Apple QuickTime File Format](https://developer.apple.com/documentation/quicktime-file-format/movie_atoms)

2. **Closed Captioning History & Standards**  
   [EIA-608 on Wikipedia](https://en.wikipedia.org/wiki/EIA-608)

3. **Microsoft Working with Videos**  
   [Microsoft Documentation](https://learn.microsoft.com/en-us/windows/win32/medfound/workingwithvideo)

4. **Google Cloud API**  
   [Google Cloud API](https://cloud.google.com/apis)

## Expected Outcomes

- **Simplified Caption Creation Process**: Users can automatically transcribe, edit, and embed captions into video files without needing advanced technical skills, reducing time and effort.
- **Accurate, Synced Captions**: The transcription process includes timestamps to create precise captions that align with the audio and video content. The ability to edit the transcript ensures captions are accurate and well-synced.
- **User-Friendly Tool**: The application provides a clean and intuitive GUI, making it easy for users to transcribe audio, edit captions, and embed them into video files.

## Diagrams

- **Deployment Diagram**: Illustrates the structure of the system and how the components interact.
- **Project E608 Program Diagram**: Provides a visual overview of the application's flow and key processes.

## Goals

### 1. Audio Transcription

The system will leverage Google Cloud APIs and open-source libraries to transcribe audio from a .WAV file into a .TXT file with timestamps. These timestamps will correspond to the timing of the spoken words in the audio, providing users with a basic caption draft to work from.

### 2. Text Editing and Synchronization

After transcription, users will be able to edit the text through a simple graphical user interface (GUI). The interface will display the transcript alongside an audio player, enabling users to adjust timestamps and review the text based on the audio's timing to ensure accuracy.

### 3. SCC File Creation

Once the transcript is finalized, the tool will convert the edited text into the SCC (E608) format, which is the industry standard for embedding captions into broadcast media.

### 4. MOV File Captioning

The application will then embed the generated SCC file into the selected .MOV video file by manipulating the atoms of the .MOV file, ensuring the captions appear during playback.



