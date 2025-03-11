# Video Player

A Windows desktop application built with WPF (.NET) for playing videos.

## Prerequisites

- [.NET SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0) or later
- Windows operating system

## Project Structure

```
VideoPlayer/
├── App.xaml             # Application XAML file
├── App.xaml.cs          # Application entry point
├── MainWindow.xaml      # Main window XAML layout
├── MainWindow.xaml.cs   # Main window logic
└── VideoPlayer.csproj   # Project configuration
```

## Building the Project

To build the project, navigate to the project directory and run:

```powershell
cd VideoPlayer
dotnet build
```

## Running the Application

To run the application, use either of these methods:

1. Using the .NET CLI:
```powershell
cd VideoPlayer
dotnet run
```

2. Using Visual Studio:
   - Open the `VideoPlayer.csproj` file
   - Press F5 or click the "Start Debugging" button

## Development

This project is built using:
- C# as the programming language
- WPF (Windows Presentation Foundation) for the UI
- .NET 7.0 framework 