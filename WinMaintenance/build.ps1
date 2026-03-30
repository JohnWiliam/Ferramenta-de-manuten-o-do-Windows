# Build script to compile the portable executable .NET 10 application

$projectPath = "WinMaintenance.csproj"
$outputPath = "Build"

if (!(Test-Path $outputPath)) {
    New-Item -ItemType Directory -Path $outputPath
}

Write-Host "Publishing the application as a portable executable..."

dotnet publish $projectPath -c Release -r win-x64 --self-contained true -o $outputPath -p:PublishSingleFile=true -p:PublishTrimmed=true -p:PublishReadyToRun=true

Write-Host "Build completed successfully. Executable is located in the $outputPath directory."
