# LinguaPlayer: Video Player with Subtitle Editor

A modern video player application with built-in subtitle editing capabilities, designed for efficient subtitle creation and management.

## Features

### Video Playback
- Support for common video formats (MP4, AVI, MKV, MOV)
- Modern, minimalist interface with custom-styled controls
- Real-time video position display

### Subtitle Management
- Load and save SRT subtitle files
- Real-time subtitle display during video playback
- Subtitle list view with timing information
- Merge consecutive subtitles
- Export audio clips based on subtitle selections

### Subtitle Editing
- Real-time subtitle editing with instant preview
- Synchronize subtitle timing with video position
- Lock editor to prevent accidental changes

## Keyboard Shortcuts

| Key | Function |
|-----|----------|
| A | Jump to previous subtitle |
| S | Jump to current subtitle |
| D | Jump to next subtitle |

## Setup Instructions

1. Download the latest release
2. Install .NET 7.0 Runtime if not already installed
3. Set up FFmpeg (required for audio export):
   - Follow the instructions in `FFmpeg/README.txt`
   - Download FFmpeg from the provided link
   - Copy the required files to the FFmpeg directory

## Usage Guide

### Loading Content
1. Click "Load Video" to open a video file
2. Click "Load Subtitles" to open an existing SRT file or start creating new subtitles

### Creating/Editing Subtitles
1. Use the timeline slider to navigate to the desired position
2. Edit subtitle text in the editing panel
3. Use "Sync" buttons to set timing to current video position
4. Click "Apply Changes" to save modifications

### Exporting Audio
1. Select one or more subtitles in the list. Shift-click to select multiple subtitles.
2. Right-click and choose "Export Audio"
3. Select the output location for the MP3 file
4. Wait for the export to complete

### Merging Subtitles
1. Shift-click to select multiple consecutive subtitles
2. Right-click and choose "Merge Subtitles"
3. The subtitles will be combined while preserving timing

## Preferences

Access preferences through File > Preferences to customize:
- Font size for better readability
- Success message display options

## System Requirements

- Windows 10 or later
- .NET 7.0 Runtime
- FFmpeg (for audio export functionality)
- Minimum 4GB RAM recommended
- Video codecs for your media files

## Troubleshooting

If you encounter issues:
1. Ensure all required components are installed
2. Check that FFmpeg is properly set up for audio export
3. Verify video codec compatibility
4. Check the application logs for error details

## License

This project is licensed under the MIT License - see the LICENSE file for details. 