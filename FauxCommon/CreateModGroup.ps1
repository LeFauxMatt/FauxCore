# Self-Elevate
if (-Not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] 'Administrator')) {
    if ([int](Get-CimInstance -Class Win32_OperatingSystem | Select-Object -ExpandProperty BuildNumber) -ge 6000) {
        $CommandLine = "-File `"" + $MyInvocation.MyCommand.Path + "`" " + $MyInvocation.UnboundArguments
        Start-Process -FilePath Pwsh.exe -Verb Runas -ArgumentList $CommandLine
        Exit
    }
}

# Define the solution directory
$SolutionDir = Split-Path -Parent (Get-Location).Path

# Navigate to the 'mods' directory within the solution directory
Set-Location "$SolutionDir\mods"

# Remove existing symbolic link if it exists
Get-ChildItem -Directory | Where-Object { $_.LinkType -eq "SymbolicLink" } | ForEach-Object { cmd /c rmdir /s /q $_.FullName }

# Iterate through each mod entry and create symlinks
(Get-Content -Path "$SolutionDir\ModsToTest.json" | ConvertFrom-Json) | ForEach-Object {
    # Create symbolic links to mod folder
    $ModFolder = [System.IO.Path]::GetRelativePath((Get-Location), "X:\Games\Stardew Valley\Mods\$_") -Replace "\\", "\\"
    cmd /c mklink /D "$_" "$ModFolder"
}