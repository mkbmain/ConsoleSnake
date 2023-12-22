# Console Snake

## Dotnet
  To install you will need dotnet currently as of the time of writing the latest is .net8.0 
  Details to install can be found 
Windows
```
https://learn.microsoft.com/en-us/dotnet/core/install/windows?tabs=net80
```
Mac
```
https://learn.microsoft.com/en-us/dotnet/core/install/macos
```

Linux
```
https://learn.microsoft.com/en-us/dotnet/core/install/linux

My recomended on linux
https://learn.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual#scripted-install

its a simple bash script download chmod +x to be execturable and 1 line and add to path in your bash\zh\etc.. profile
```


Once dotnet is installed and added to path to build simply go to root

```
dotnet build -c Release ConsoleSnake.sln 
```

wolla a binary should now be created in 
```
Root/ConsoleSnake/bin/Release/net8.0
```
