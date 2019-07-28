FROM microsoft/dotnet:2.0-sdk
WORKDIR /app
 
COPY /C:/Users/yusuf/Source/Repos/ogrenci-kariyeri/StudentCareerApp/bin/Release/netcoreapp2.2/ .
 
ENTRYPOINT ["dotnet", "StudentCareerApp.dll"]