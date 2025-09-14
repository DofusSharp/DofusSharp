param (
    [Parameter(Mandatory=$true)] $TemplatePath = ".\README.md",
    $OutputPath
)

if (-not (Test-Path $TemplatePath)) {
    Write-Error "Template file not found: $TemplatePath"
    exit 1
}

if ($OutputPath -eq $null) {
    if ($TemplatePath -match "^(.*)\.tpl$") {
        $OutputPath = $Matches[1]
    } else {
        $OutputPath = (Split-Path -Path $TemplatePath -Parent) + "result.md"
    }
}

Write-Host "Generating README from template '$TemplatePath' to '$OutputPath'..."

$Template = Get-Content $TemplatePath -Raw
$Result = ""

$Match = [Regex]::Match($Template, "\{\{run:(.*?)\}\}");
while ($Match.Success) {    
    $Before = $Template.Substring(0, $Match.Index)
    $After = $Template.Substring($Match.Index + $Match.Length)
    $Command = $Match.Groups[1].Value.Trim()

    # post process command
    $Command = $Command -replace "dofusdb", "$PSScriptRoot/../dofusdb/bin/Debug/net10.0/dofusdb.exe"
    
    Write-Host "Executing command: $Command"
    
    try {
        $Env:COLUMNS = 305
        $CommandOutput = (Invoke-Expression $Command | Out-String)
        if ($LASTEXITCODE -ne 0) {
            throw "Command failed with exit code $LASTEXITCODE"
        }
    } catch {
        Write-Error "Error executing command '$Command': $_"
        exit 1
    }

    $Result += $Before + $CommandOutput.Trim()
    $Template = $After
    
    $Match = [Regex]::Match($Template, "\{\{run:(.*?)\}\}");
}

$Result += $Template

Out-File -FilePath $OutputPath -InputObject $Result -Encoding UTF8

Write-Host "README generated successfully at '$OutputPath'."