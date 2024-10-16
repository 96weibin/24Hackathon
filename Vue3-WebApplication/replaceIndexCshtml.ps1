Write-Host "Starting Replace"
$file = Get-Content -Path "./dist/Index.cshtml"
$file = $file -replace '/css/', "/Vue3-WebApplication/dist/css/"
$file = $file -replace '/js/', '/Vue3-WebApplication/dist/js/' | Out-File "../Views/_ViewStart.cshtml"

