@echo off
echo Creando solucion ColegioMilitar...

dotnet new sln -n ColegioMilitar

dotnet new classlib -n ColegioMilitar.Domain -f net10.0 -o src\ColegioMilitar.Domain
dotnet new classlib -n ColegioMilitar.Infrastructure -f net10.0 -o src\ColegioMilitar.Infrastructure
dotnet new classlib -n ColegioMilitar.Application -f net10.0 -o src\ColegioMilitar.Application
dotnet new classlib -n ColegioMilitar.Reports -f net10.0 -o src\ColegioMilitar.Reports
dotnet new winforms -n ColegioMilitar.UI -f net10.0 -o src\ColegioMilitar.UI

REM WinForms requiere net10.0-windows en el .csproj (no en el flag del template)
powershell -Command "(Get-Content 'src\ColegioMilitar.UI\ColegioMilitar.UI.csproj') -replace '<TargetFramework>net10.0</TargetFramework>', '<TargetFramework>net10.0-windows</TargetFramework>' | Set-Content 'src\ColegioMilitar.UI\ColegioMilitar.UI.csproj'"


dotnet sln add src\ColegioMilitar.Domain\ColegioMilitar.Domain.csproj
dotnet sln add src\ColegioMilitar.Infrastructure\ColegioMilitar.Infrastructure.csproj
dotnet sln add src\ColegioMilitar.Application\ColegioMilitar.Application.csproj
dotnet sln add src\ColegioMilitar.Reports\ColegioMilitar.Reports.csproj
dotnet sln add src\ColegioMilitar.UI\ColegioMilitar.UI.csproj

REM Referencias entre proyectos
dotnet add src\ColegioMilitar.Infrastructure\ColegioMilitar.Infrastructure.csproj reference src\ColegioMilitar.Domain\ColegioMilitar.Domain.csproj
dotnet add src\ColegioMilitar.Application\ColegioMilitar.Application.csproj reference src\ColegioMilitar.Domain\ColegioMilitar.Domain.csproj
dotnet add src\ColegioMilitar.Application\ColegioMilitar.Application.csproj reference src\ColegioMilitar.Infrastructure\ColegioMilitar.Infrastructure.csproj
dotnet add src\ColegioMilitar.Reports\ColegioMilitar.Reports.csproj reference src\ColegioMilitar.Domain\ColegioMilitar.Domain.csproj
dotnet add src\ColegioMilitar.Reports\ColegioMilitar.Reports.csproj reference src\ColegioMilitar.Application\ColegioMilitar.Application.csproj
dotnet add src\ColegioMilitar.UI\ColegioMilitar.UI.csproj reference src\ColegioMilitar.Application\ColegioMilitar.Application.csproj
dotnet add src\ColegioMilitar.UI\ColegioMilitar.UI.csproj reference src\ColegioMilitar.Reports\ColegioMilitar.Reports.csproj

REM Paquetes NuGet
dotnet add src\ColegioMilitar.Infrastructure\ColegioMilitar.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Sqlite
dotnet add src\ColegioMilitar.Infrastructure\ColegioMilitar.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Tools
dotnet add src\ColegioMilitar.Reports\ColegioMilitar.Reports.csproj package ClosedXML

echo.
echo Listo! Abre ColegioMilitar.sln en Visual Studio.
pause
