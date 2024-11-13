# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ReceiptProcessorChallenge-CSharp/ReceiptProcessorChallenge-CSharp.csproj", "ReceiptProcessorChallenge-CSharp/"]
RUN dotnet restore "./ReceiptProcessorChallenge-CSharp/ReceiptProcessorChallenge-CSharp.csproj"
COPY . .
WORKDIR "/src/ReceiptProcessorChallenge-CSharp"
RUN dotnet build "./ReceiptProcessorChallenge-CSharp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ReceiptProcessorChallenge-CSharp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReceiptProcessorChallenge-CSharp.dll"]