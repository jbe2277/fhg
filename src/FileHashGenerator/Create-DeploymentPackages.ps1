<# Preconditions:
 # 1. Release build exists
 # 2. ClickOnce publish exists
 # 3. Visual Studio is closed and nothing else has a file open.
 # 
 # Run: Start this script from within the solution directory!
 #>

$PackageProductName = "FileHashGenerator"
$PackageVersionNumber = "3.1.1.450"
$ProductFolder = "Waf File Hash Generator"
$BinaryFiles = 
    "FileHashGenerator.exe",
    "FileHashGenerator.exe.config",
    "Waf.FileHashGenerator.Applications.dll",
    "Waf.FileHashGenerator.Domain.dll",
    "WpfApplicationFramework.dll",
    "de\FileHashGenerator.resources.dll",
    "de\Waf.FileHashGenerator.Applications.resources.dll"


function Create-Zip([String] $FileName, [String] $SourceFolder, [Bool] $IncludeBaseDirectory)
{
    If (Test-Path $FileName) { Remove-Item $FileName }
    
    Add-Type -Assembly System.IO.Compression.FileSystem
    $compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
    [System.IO.Compression.ZipFile]::CreateFromDirectory($SourceFolder,
        $FileName, $compressionLevel, $IncludeBaseDirectory)
}


#
# Copy all binary files to a temporary directory and then create the zip file.
#
$TempFolder = [System.IO.Path]::Combine([System.IO.Path]::GetTempPath(), "Waf-Deploy", $ProductFolder)
Write-Output "TempFolder: $($TempFolder)"

New-Item $TempFolder\de -Type Directory
$BinaryFiles | ForEach { 
    Copy-Item (Join-Path (pwd) (Join-Path ".\Output\Release" $_)) -Destination (Join-Path $TempFolder $_) 
}
Create-Zip (Join-Path (pwd) ..\$PackageProductName-Binaries-$PackageVersionNumber.zip) -SourceFolder $TempFolder -IncludeBaseDirectory $True
Remove-Item $TempFolder -Recurse


#
# Create a ClickOnce zip file of the Publish folder
#
Create-Zip (Join-Path (pwd) ..\$PackageProductName-ClickOnce-$PackageVersionNumber.zip) -SourceFolder (Join-Path (pwd) ".\Output\Publish") -IncludeBaseDirectory $False


#
# Clean up solution folder so that only source code remains.
#
If (Test-Path .\Output) { Remove-Item .\Output -Recurse }
If (Test-Path .\TestResults) { Remove-Item .\TestResults -Recurse  }
Get-ChildItem .\*.user -Recurse | Remove-Item
Remove-Item .\*.suo -Force
Get-ChildItem . -Directory | Get-ChildItem -Filter obj -Directory | Remove-Item -Recurse
Get-ChildItem . -Directory | Get-ChildItem -Filter bin -Directory | Remove-Item -Recurse

Create-Zip (Join-Path (pwd) ..\$PackageProductName-Source-$PackageVersionNumber.zip) -SourceFolder (pwd) -IncludeBaseDirectory $True
