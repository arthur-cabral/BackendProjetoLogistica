FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0 AS build-env
WORKDIR /app
EXPOSE 8001/tcp

COPY . ./
RUN dotnet restore
RUN mkdir -p /app/cert
RUN dotnet dev-certs https -ep /app/cert/API.pfx -p "#Admin123"
RUN dotnet dev-certs https --trust
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=build-env /app/cert /https
ENTRYPOINT ["dotnet", "API.dll"]