echo
echo === Building win-x64 application
dotnet publish Socr.Main/Socr.Main.csproj -r win-x64 -c Release -o ../pub/win-x64

echo === Building self-contained win-x64 application
dotnet publish Socr.Main/Socr.Main.csproj -r win-x64 -c Release -o ../pub/win-x64_sc --self-contained true -p:PublishTrimmed=true

echo === Building linux-x64 application
dotnet publish Socr.Main/Socr.Main.csproj -r linux-x64 -c Release -o ../pub/linux-x64

echo === Building self-contained linux-x64 application
dotnet publish Socr.Main/Socr.Main.csproj -r linux-x64 -c Release -o ../pub/linux-x64_sc --self-contained true -p:PublishTrimmed=true
