$Currentlocation = Get-Location

cd ../tests/Detector.Tests
dotnet test
cd ../Detector.Tests.EndToEnd
dotnet test
cd $Currentlocation