FFmpeg Setup Instructions
=======================

To use the audio export feature, you need to have FFmpeg available in the application's FFmpeg directory.

How to get FFmpeg:
1. Download FFmpeg for Windows from https://github.com/BtbN/FFmpeg-Builds/releases
   - Choose the "ffmpeg-master-latest-win64-gpl.zip" file
2. Extract the downloaded zip file
3. Setup instructions:
   - Copy the entire 'bin' directory from the extracted files into the VideoPlayer/FFmpeg/ directory
   - Make sure to include all DLL files along with ffmpeg.exe, as they are required for proper operation

The directory structure should look like this:
VideoPlayer/
└── FFmpeg/
    ├── README.txt (this file)
    ├── bin/
    │   ├── ffmpeg.exe
    │   ├── avcodec-61.dll
    │   ├── avdevice-61.dll
    │   ├── avfilter-10.dll
    │   ├── avformat-61.dll
    │   ├── avutil-59.dll
    │   ├── ffplay.exe
    │   ├── ffprobe.exe
    │   ├── postproc-58.dll
    │   ├── swresample-5.dll
    │   └── swscale-8.dll
    └── ... (other FFmpeg files if needed)

Note: The application will look for ffmpeg.exe in the bin directory. All DLL files must be kept together with ffmpeg.exe for proper functionality. If FFmpeg is not found or any required DLLs are missing, you will see an error message. 