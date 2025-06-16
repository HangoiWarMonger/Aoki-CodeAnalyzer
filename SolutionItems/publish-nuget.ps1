# ================= НАСТРОЙКИ =================
$Version = "1.0.1"
$ApiKey = "CHANGE-ME"
$Configuration = "Release"
$ProjectPath = "../src/Analyzers/Aoki.CodeAnalyzer/Aoki.CodeAnalyzer.csproj"
$PackageId = "Aoki.CodeAnalyzer"
# ============================================

if (-not $ApiKey -or $ApiKey -eq "ТВОЙ_NUGET_API_KEY") {
    Write-Error "❌ Укажи свой NuGet API ключ в переменной `$ApiKey` в скрипте"
    exit 1
}

# Сборка проекта
Write-Host "🔨 Building project..."
dotnet build $ProjectPath -c $Configuration
if ($LASTEXITCODE -ne 0) {
    Write-Error "❌ Build failed."
    exit 1
}

# Упаковка проекта
Write-Host "📦 Packing project with version $Version..."
dotnet pack $ProjectPath -c $Configuration /p:PackageVersion=$Version --no-build
if ($LASTEXITCODE -ne 0) {
    Write-Error "❌ Pack failed."
    exit 1
}

# Путь к выходной папке и поиск пакета
$projectDir = Split-Path $ProjectPath -Parent
$outputDir = Join-Path $projectDir "bin\$Configuration"
$packagePattern = "$PackageId.$Version*.nupkg"

$package = Get-ChildItem -Path $outputDir -Filter $packagePattern | Sort-Object LastWriteTime -Descending | Select-Object -First 1

if (-not $package) {
    Write-Error "❌ Package not found in $outputDir matching pattern $packagePattern"
    exit 1
}

Write-Host "🚀 Publishing package $($package.Name)..."
dotnet nuget push $package.FullName `
    --api-key $ApiKey `
    --source https://api.nuget.org/v3/index.json `
    --skip-duplicate
if ($LASTEXITCODE -ne 0) {
    Write-Error "❌ Package publish failed."
    exit 1
}

Write-Host "✅ Package published successfully."