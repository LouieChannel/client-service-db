FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /tmp
COPY /src ./src
COPY *.sln ./
RUN dotnet publish src/ -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /tmp/out ./
ENTRYPOINT ["dotnet", "Ascalon.ClientService.Migrations.dll"]
