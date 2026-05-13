# Use the ASP.NET Core 10.0 runtime for the final image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Use the .NET 10.0 SDK to build the project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy everything and restore dependencies
COPY . .
RUN dotnet restore "BagsakNowAPI/BagsakNowAPI.csproj"

# Build and publish the application
RUN dotnet publish "BagsakNowAPI/BagsakNowAPI.csproj" -c Release -o /app/out

# Final stage: copy the build results to the runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "BagsakNowAPI.dll"]
