$services = @(
    "appointments-management",
    "authentication-management",
    "clinical-data-grid",
    "patient-management",
    "prescription-management",
    "staff-management"
)

$root = Split-Path -Parent $MyInvocation.MyCommand.Path

foreach ($service in $services) {
    $servicePath = Join-Path $root $service
    if (Test-Path $servicePath) {
        Start-Process wt -ArgumentList "new-tab", "--title", "$service", "-d", "`"$servicePath`"", "powershell", "-NoExit", "-Command", "dotnet watch run"
    }
    else {
        Write-Host "Directory $servicePath does not exist."
    }
}