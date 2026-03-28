# ============================================================================
# Download Prerequisites Script
# ============================================================================
# This script downloads required prerequisites for the installer
# ============================================================================

param(
    [string]$DestinationPath = ".\Prerequisites"
)

Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Downloading Installer Prerequisites" -ForegroundColor Cyan
Write-Host "============================================`n" -ForegroundColor Cyan

# Create Prerequisites directory if it doesn't exist
if (!(Test-Path $DestinationPath)) {
    New-Item -ItemType Directory -Path $DestinationPath | Out-Null
    Write-Host "✓ Created Prerequisites directory" -ForegroundColor Green
}

# Download SQL Server Express LocalDB
Write-Host "`n[1/2] Downloading SQL Server Express LocalDB..." -ForegroundColor Yellow
$sqlLocalDBUrl = "https://go.microsoft.com/fwlink/?linkid=2216019"
$sqlLocalDBPath = Join-Path $DestinationPath "SqlLocalDB.msi"

if (Test-Path $sqlLocalDBPath) {
    Write-Host "  ℹ SQL LocalDB already exists, skipping download" -ForegroundColor Gray
}
else {
    try {
        Write-Host "  Downloading from Microsoft..." -ForegroundColor Gray
        Invoke-WebRequest -Uri $sqlLocalDBUrl -OutFile $sqlLocalDBPath -UseBasicParsing
        Write-Host "  ✓ SQL Server Express LocalDB downloaded" -ForegroundColor Green
        Write-Host "  Size: $((Get-Item $sqlLocalDBPath).Length / 1MB) MB" -ForegroundColor Gray
    }
    catch {
        Write-Host "  ✗ Failed to download SQL LocalDB: $_" -ForegroundColor Red
        Write-Host "  Please download manually from: $sqlLocalDBUrl" -ForegroundColor Yellow
    }
}

# Iskoola Pota Font - Manual download required
Write-Host "`n[2/2] Iskoola Pota Font..." -ForegroundColor Yellow
$fontPath = Join-Path $DestinationPath "iskpota.ttf"

if (Test-Path $fontPath) {
    Write-Host "  ✓ Iskoola Pota font already exists" -ForegroundColor Green
}
else {
    Write-Host "  ℹ Iskoola Pota font needs to be downloaded manually" -ForegroundColor Yellow
    Write-Host "  " -NoNewline
    Write-Host "Option 1:" -ForegroundColor Cyan -NoNewline
    Write-Host " Copy from Windows Fonts folder (C:\Windows\Fonts\iskpota.ttf)"
    Write-Host "  " -NoNewline
    Write-Host "Option 2:" -ForegroundColor Cyan -NoNewline
    Write-Host " Download from https://www.fonts.lk/"
    Write-Host "  Save as: $fontPath" -ForegroundColor Gray
    
    # Try to copy from Windows Fonts
    $windowsFontPath = "C:\Windows\Fonts\iskpota.ttf"
    if (Test-Path $windowsFontPath) {
        Write-Host "`n  Found font in Windows Fonts folder. Copy it? (Y/N): " -ForegroundColor Yellow -NoNewline
        $response = Read-Host
        if ($response -eq 'Y' -or $response -eq 'y') {
            try {
                Copy-Item $windowsFontPath $fontPath
                Write-Host "  ✓ Font copied successfully" -ForegroundColor Green
            }
            catch {
                Write-Host "  ✗ Failed to copy font: $_" -ForegroundColor Red
            }
        }
    }
}

# Summary
Write-Host "`n============================================" -ForegroundColor Cyan
Write-Host "Download Summary" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan

$sqlExists = Test-Path $sqlLocalDBPath
$fontExists = Test-Path $fontPath

Write-Host "SQL Server LocalDB: " -NoNewline
if ($sqlExists) {
    Write-Host "✓ Ready" -ForegroundColor Green
}
else {
    Write-Host "✗ Missing" -ForegroundColor Red
}

Write-Host "Iskoola Pota Font:  " -NoNewline
if ($fontExists) {
    Write-Host "✓ Ready" -ForegroundColor Green
}
else {
    Write-Host "✗ Missing" -ForegroundColor Red
}

if ($sqlExists -and $fontExists) {
    Write-Host "`n✓ All prerequisites ready!" -ForegroundColor Green
    Write-Host "You can now build the installer using Inno Setup." -ForegroundColor White
}
else {
    Write-Host "`n⚠ Some prerequisites are missing." -ForegroundColor Yellow
    Write-Host "Please download the missing files before building the installer." -ForegroundColor White
}

Write-Host "`nNext Steps:" -ForegroundColor Cyan
Write-Host "1. Ensure all prerequisites are downloaded" -ForegroundColor White
Write-Host "2. Open Setup.iss in Inno Setup Compiler" -ForegroundColor White
Write-Host "3. Click Build → Compile" -ForegroundColor White
Write-Host "4. Find the installer in Output\PettyCashSetup_v1.0.0.exe`n" -ForegroundColor White
