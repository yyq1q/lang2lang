# PowerShell スクリプト

# cpp
$RootDir = Split-Path -Parent (Resolve-Path $MyInvocation.MyCommand.Path)
$BuildDir = Join-Path $RootDir "src/cpp/build"

if (-Not (Test-Path $BuildDir)) {
    New-Item -ItemType Directory -Path $BuildDir | Out-Null
}

Set-Location -Path $BuildDir
cmake ..
if ($LASTEXITCODE -ne 0) {
    Write-Error "CMake ビルドに失敗しました。"
    exit 1
}

& make
if ($LASTEXITCODE -ne 0) {
    Write-Error "Make ビルドに失敗しました。"
    exit 1
} else {
    Write-Host "CMake ビルドが成功しました。"
}

# cs
$CsProjects = @(
    Join-Path $RootDir "src/cs/client/cs.csproj"
    Join-Path $RootDir "src/cs/server/cs.csproj"
)

foreach ($Project in $CsProjects) {
    Write-Host "Building C# project: $Project"
    & dotnet build $Project -c Release
    if ($LASTEXITCODE -ne 0) {
        Write-Error "$Project のビルドに失敗しました。"
        exit 1
    } else {
        Write-Host "$Project のビルドが成功しました。"
    }
}