$prjName = "Excelerator.ClosedXml"
$basePath = split-path -parent $MyInvocation.MyCommand.Definition
$targetPath = "$($basePath)\..\bin\Release"
Write-Host $targetPath;
Remove-Item -path "$($targetPath)\*" -Recurse
$pkg = "$($targetPath)\$($prjName).*.nupkg"
dotnet build -c Release ../"$($prjName)".csproj
$version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$( $targetPath )\netcoreapp2.2\$( $prjName ).dll").ProductVersion
Write-Host "Версия $version"
dotnet pack -c Release /p:PackageVersion=$version ../"$($prjName)".csproj
#dotnet nuget push -s http://baget.ix.local:5555/v3/index.json -k NUGET-SERVER-API-KEY $pkg
dotnet nuget push -s https://api.nuget.org/v3/index.json -k oy2mxen3sv3kfc5y5xtaaexrgitrn74eflcuvosksawgna $pkg

