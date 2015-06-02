Remove-Item *.nupkg

nuget pack RoutePlannerLib.csproj -Build -Symbols -Properties Configuration=Release

nuget push FHNW.ecnf.RoutePlannerLib.?.?.?.?.nupkg e9c479e2-9ef4-447d-8498-6237040e8c35

Write-Host -NoNewLine 'Process finished. Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');