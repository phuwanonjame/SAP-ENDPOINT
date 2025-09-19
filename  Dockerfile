# ใช้ official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# copy csproj และ restore
COPY *.csproj ./
RUN dotnet restore

# copy ทั้งโค้ดและ build
COPY . ./
RUN dotnet publish -c Release -o out

# runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "MyApi.dll"]
