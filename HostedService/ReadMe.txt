To create NuGet Pkg
-------------------

- Open cmd terminal
- Go to csproj folder
- Run
	dotnet pack OcrHostedService.csproj -c Release --force --output "C:\temp\jos\NugetLocal" --no-dependencies --include-symbols -p:PackageVersion=1.5.0 