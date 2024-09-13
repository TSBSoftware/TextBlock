#!/bin/bash

set -e

if [[ -z "$API_KEY" ]]; then
    echo "API_KEY must be set to publish nuget package."
    exit 1
fi

# Run unit tests
dotnet test tests/*.fsproj

# Clean the build
dotnet clean
rm -rf src/bin

# Create a release build.
dotnet build -c Release src/*.csproj

# Push nuget package
dotnet nuget push src/bin/Release/TSBSoftware.TextBlock.*.nupkg --api-key $API_KEY --source https://api.nuget.org/v3/index.json
