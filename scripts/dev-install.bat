dotnet tool uninstall -g TaskStak 
dotnet pack 
dotnet tool install -g --add-source "src/TaskStak.CLI/bin/Release" TaskStak