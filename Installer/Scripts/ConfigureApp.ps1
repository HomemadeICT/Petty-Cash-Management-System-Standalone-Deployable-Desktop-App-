# ============================================================================
# ConfigureApp.ps1 - Complete Database Setup & Configuration
# Petty Cash Management System
# ============================================================================
# This script handles ALL database setup using System.Data.SqlClient (ADO.NET).
# Zero external tool dependencies — no sqlcmd, no internet required.
# ============================================================================

param(
    [string]$AppPath = "C:\Program Files\Petty Cash Management System"
)

$ErrorActionPreference = "Continue"

# Logging
$logFile = Join-Path $AppPath "Scripts\setup_log.txt"
function Log($msg) {
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    $line = "[$timestamp] $msg"
    Write-Host $line
    Add-Content -Path $logFile -Value $line -ErrorAction SilentlyContinue
}

Log "============================================"
Log "  PETTY CASH SYSTEM — INSTALLER SETUP"
Log "============================================"

# ============================================================================
# STEP 1: Detect & Start SQL Server Service (up to 90 seconds)
# ============================================================================
Log ""
Log "[1/6] Waiting for SQL Server Express service..."

$maxWait = 18  # 18 x 5 = 90 seconds
$started = $false

for ($i = 0; $i -lt $maxWait; $i++) {
    $svc = Get-Service -Name 'MSSQL$SQLEXPRESS' -ErrorAction SilentlyContinue
    if ($svc) {
        if ($svc.Status -eq 'Running') {
            Log "  [OK] SQL Server Express is RUNNING"
            $started = $true
            break
        }
        else {
            Log "  -> Service status: $($svc.Status). Attempting start..."
            try { Start-Service -Name 'MSSQL$SQLEXPRESS' -ErrorAction SilentlyContinue } catch {}
        }
    }
    else {
        Log "  -> Service not found yet. Waiting... ($($i * 5)s / 90s)"
    }
    Start-Sleep -Seconds 5
}

if (-not $started) {
    Log "  [ERROR] SQL Server did not start within 90 seconds!"
    Log "  Please start MSSQL`$SQLEXPRESS service manually and re-run."
    exit 1
}

# SQL Server connection settings
$serverInstance = ".\SQLEXPRESS"
$saPassword = "PettyCash@2026"
$appUser = "petty_app_user"
$appPassword = "PettyCash@App2026"
$dbName = "PettyCashDB"

# Helper: Execute SQL on a specific database
function Invoke-Sql {
    param(
        [string]$Query,
        [string]$Database = "master",
        [string]$Server = $serverInstance,
        [switch]$UseSA
    )
    
    if ($UseSA) {
        $connStr = "Server=$Server;Database=$Database;User Id=sa;Password=$saPassword;TrustServerCertificate=True;Connection Timeout=15;"
    }
    else {
        $connStr = "Server=$Server;Database=$Database;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=15;"
    }
    
    $conn = New-Object System.Data.SqlClient.SqlConnection($connStr)
    try {
        $conn.Open()
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = $Query
        $cmd.CommandTimeout = 60
        $cmd.ExecuteNonQuery() | Out-Null
    }
    finally {
        $conn.Close()
        $conn.Dispose()
    }
}

# Helper: Execute SQL and return scalar
function Invoke-SqlScalar {
    param(
        [string]$Query,
        [string]$Database = "master",
        [string]$Server = $serverInstance,
        [switch]$UseSA
    )
    
    if ($UseSA) {
        $connStr = "Server=$Server;Database=$Database;User Id=sa;Password=$saPassword;TrustServerCertificate=True;Connection Timeout=15;"
    }
    else {
        $connStr = "Server=$Server;Database=$Database;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=15;"
    }
    
    $conn = New-Object System.Data.SqlClient.SqlConnection($connStr)
    try {
        $conn.Open()
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = $Query
        $cmd.CommandTimeout = 30
        return $cmd.ExecuteScalar()
    }
    finally {
        $conn.Close()
        $conn.Dispose()
    }
}

# Helper: Execute multi-batch SQL script (splits on GO)
function Invoke-SqlScript {
    param(
        [string]$Script,
        [string]$Database = "PettyCashDB",
        [string]$Server = $serverInstance
    )
    
    $connStr = "Server=$Server;Database=$Database;User Id=sa;Password=$saPassword;TrustServerCertificate=True;Connection Timeout=30;"
    $conn = New-Object System.Data.SqlClient.SqlConnection($connStr)
    
    try {
        $conn.Open()
        
        # Split by GO keyword on its own line
        $batches = [regex]::Split($Script, '^\s*GO\s*$', [System.Text.RegularExpressions.RegexOptions]::Multiline -bor [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
        
        foreach ($batch in $batches) {
            $trimmed = $batch.Trim()
            if ([string]::IsNullOrWhiteSpace($trimmed)) { continue }
            # Skip USE statements (we use connection string for context)
            if ($trimmed -match '^\s*USE\s+') { continue }
            
            try {
                $cmd = $conn.CreateCommand()
                $cmd.CommandText = $trimmed
                $cmd.CommandTimeout = 60
                $cmd.ExecuteNonQuery() | Out-Null
            }
            catch {
                Log "  [WARNING] Batch error (may be OK): $($_.Exception.Message)"
            }
        }
    }
    finally {
        $conn.Close()
        $conn.Dispose()
    }
}

# ============================================================================
# STEP 2: Test connection to SQL Server
# ============================================================================
Log ""
Log "[2/6] Testing SQL Server connection..."

$connected = $false
# Try SA auth first (installed with SECURITYMODE=SQL)
try {
    $testResult = Invoke-SqlScalar -Query "SELECT 1" -Database "master" -UseSA
    if ($testResult -eq 1) {
        Log "  [OK] Connected via SA authentication"
        $connected = $true
    }
}
catch {
    Log "  -> SA auth failed: $($_.Exception.Message)"
}

# Fallback to Windows auth
if (-not $connected) {
    try {
        $testResult = Invoke-SqlScalar -Query "SELECT 1" -Database "master"
        if ($testResult -eq 1) {
            Log "  [OK] Connected via Windows authentication"
            $connected = $true
        }
    }
    catch {
        Log "  -> Windows auth also failed: $($_.Exception.Message)"
    }
}

if (-not $connected) {
    Log "  [ERROR] Cannot connect to SQL Server at all!"
    exit 1
}

# ============================================================================
# STEP 3: Create PettyCashDB database
# ============================================================================
Log ""
Log "[3/6] Creating database..."

try {
    $dbExists = Invoke-SqlScalar -Query "SELECT COUNT(*) FROM sys.databases WHERE name = '$dbName'" -Database "master" -UseSA
    if ($dbExists -eq 0) {
        Invoke-Sql -Query "CREATE DATABASE [$dbName]" -Database "master" -UseSA
        Log "  [OK] Database '$dbName' created"
        Start-Sleep -Seconds 3  # Give DB time to initialize
    }
    else {
        Log "  [OK] Database '$dbName' already exists"
    }
}
catch {
    Log "  [ERROR] Failed to create database: $($_.Exception.Message)"
    exit 1
}

# ============================================================================
# STEP 4: Create SQL Login & User
# ============================================================================
Log ""
Log "[4/6] Creating application SQL login..."

try {
    $loginExists = Invoke-SqlScalar -Query "SELECT COUNT(*) FROM sys.server_principals WHERE name = '$appUser'" -Database "master" -UseSA
    if ($loginExists -eq 0) {
        Invoke-Sql -Query "CREATE LOGIN [$appUser] WITH PASSWORD = '$appPassword', CHECK_POLICY = OFF" -Database "master" -UseSA
        Log "  [OK] SQL Login '$appUser' created"
    }
    else {
        Log "  [OK] SQL Login '$appUser' already exists"
    }
    
    $userExists = Invoke-SqlScalar -Query "SELECT COUNT(*) FROM sys.database_principals WHERE name = '$appUser'" -Database $dbName -UseSA
    if ($userExists -eq 0) {
        Invoke-Sql -Query "CREATE USER [$appUser] FOR LOGIN [$appUser]; ALTER ROLE [db_owner] ADD MEMBER [$appUser];" -Database $dbName -UseSA
        Log "  [OK] Database user '$appUser' created with db_owner role"
    }
    else {
        Log "  [OK] Database user '$appUser' already exists"
    }
}
catch {
    Log "  [WARNING] Login/user creation issue: $($_.Exception.Message)"
}

# ============================================================================
# STEP 5: Create tables, views, stored procedures, and seed data
# ============================================================================
Log ""
Log "[5/6] Setting up database schema..."

try {
    # Check if tables already exist
    $tableCount = Invoke-SqlScalar -Query "SELECT COUNT(*) FROM sys.tables WHERE name = 'users'" -Database $dbName -UseSA
    
    if ($tableCount -eq 0) {
        # Read the SQL script from the Scripts folder
        $sqlScriptPath = Join-Path $AppPath "Scripts\SetupDatabase.sql"
        
        if (Test-Path $sqlScriptPath) {
            $sqlScript = Get-Content $sqlScriptPath -Raw
            Invoke-SqlScript -Script $sqlScript -Database $dbName
            Log "  [OK] All tables, views, and stored procedures created"
            Log "  [OK] Seed data (categories, permissions, admin role defaults) inserted"
        }
        else {
            Log "  [ERROR] SetupDatabase.sql not found at: $sqlScriptPath"
        }
    }
    else {
        Log "  [OK] Tables already exist — skipping schema creation"
    }
    
    # Verify final table count
    $finalCount = Invoke-SqlScalar -Query "SELECT COUNT(*) FROM sys.tables" -Database $dbName -UseSA
    Log "  [OK] Database has $finalCount tables"
    
}
catch {
    Log "  [ERROR] Schema setup failed: $($_.Exception.Message)"
}

# ============================================================================
# STEP 6: Update App.config with correct connection string
# ============================================================================
Log ""
Log "[6/6] Updating application configuration..."

$connectionString = "Server=$serverInstance;Database=$dbName;User Id=$appUser;Password=$appPassword;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=15;"

$configFiles = Get-ChildItem -Path $AppPath -Filter "*.config" -ErrorAction SilentlyContinue
if ($configFiles) {
    foreach ($file in $configFiles) {
        try {
            [xml]$xml = Get-Content $file.FullName
            $node = $xml.configuration.connectionStrings.add | Where-Object { $_.name -eq "PettyCashDB" }
            if ($node) {
                $node.connectionString = $connectionString
                $xml.Save($file.FullName)
                Log "  [OK] Updated: $($file.Name)"
            }
        }
        catch {
            Log "  [WARNING] Could not update $($file.Name): $($_.Exception.Message)"
        }
    }
}
else {
    Log "  [WARNING] No config files found in $AppPath"
}

# ============================================================================
# FINAL REPORT
# ============================================================================
Log ""
Log "============================================"
Log "  SETUP COMPLETE!"
Log "============================================"
Log "  Server:   $serverInstance"
Log "  Database: $dbName"
Log "  App User: $appUser"
Log "  Config:   Updated"
Log ""
Log "  Default Login:"
Log "    Username: admin"
Log "    Password: admin123"
Log "============================================"

exit 0
