# ================= НАСТРОЙКИ =================
$Version = "1.0.7"
$VersionAlpha = "$Version-alpha"
$Configuration = "Release"
$ProjectPath = "../src/Analyzers/Aoki.CodeAnalyzer/Aoki.CodeAnalyzer.csproj"
$PackageId = "Aoki.CodeAnalyzer"
$LocalNugetSource = "C:\test\NuGetLocal\asd"   # <-- укажи путь к локальному источнику
# ============================================

# Создаем папку локального источника, если нет
if (-not (Test-Path $LocalNugetSource)) {
    New-Item -ItemType Directory -Path $LocalNugetSource | Out-Null
}

# Сборка проекта
Write-Host "🔨 Building project..."
dotnet build $ProjectPath -c $Configuration
if ($LASTEXITCODE -ne 0) {
    Write-Error "❌ Build failed."
    exit 1
}

# Упаковка проекта с версией с суффиксом -alpha
Write-Host "📦 Packing project with version $VersionAlpha..."
dotnet pack $ProjectPath -c $Configuration /p:PackageVersion=$VersionAlpha --no-build
if ($LASTEXITCODE -ne 0) {
    Write-Error "❌ Pack failed."
    exit 1
}

# Путь к выходной папке и поиск пакета
$projectDir = Split-Path $ProjectPath -Parent
$outputDir = Join-Path $projectDir "bin\$Configuration"
$packagePattern = "$PackageId.$VersionAlpha*.nupkg"

$package = Get-ChildItem -Path $outputDir -Filter $packagePattern | Sort-Object LastWriteTime -Descending | Select-Object -First 1

if (-not $package) {
    Write-Error "❌ Package not found in $outputDir matching pattern $packagePattern"
    exit 1
}

# Копирование пакета в локальный NuGet источник
$destinationPath = Join-Path $LocalNugetSource $package.Name
Write-Host "📁 Copying package $($package.Name) to local feed $LocalNugetSource..."
Copy-Item -Path $package.FullName -Destination $destinationPath -Force
if ($LASTEXITCODE -ne 0) {
    Write-Error "❌ Failed to copy package to local feed."
    exit 1
}

Write-Host "✅ Package copied successfully to local NuGet source."
Write-Host "👉 Чтобы использовать пакет, добавь локальный источник:"
Write-Host "   dotnet nuget add source '$LocalNugetSource' -n LocalFeed"
Write-Host "   и устанавливай пакет через dotnet add package $PackageId --version $VersionAlpha"