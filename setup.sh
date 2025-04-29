#!/bin/bash

# cpp
ROOT_DIR=$(dirname "$(realpath "$0")")
BUILD_DIR="$ROOT_DIR/src/cpp/build"

if [ ! -d "$BUILD_DIR" ]; then
  mkdir -p "$BUILD_DIR"
fi

cd "$BUILD_DIR"
cmake ..
make

if [ $? -eq 0 ]; then
  echo "CMakeビルドが成功しました。"
else
  echo "CMakeビルドに失敗しました。" >&2
  exit 1
fi

# cs

CS_PROJECTS=(
  "$ROOT_DIR/src/cs/client/cs.csproj"
  "$ROOT_DIR/src/cs/server/cs.csproj"
)

for PROJECT in "${CS_PROJECTS[@]}"; do
  echo "Building C# project: $PROJECT"
  dotnet build "$PROJECT" -c Release

  if [ $? -eq 0 ]; then
    echo "$PROJECT のビルドが成功しました。"
  else
    echo "$PROJECT のビルドに失敗しました。" >&2
    exit 1
  fi
done