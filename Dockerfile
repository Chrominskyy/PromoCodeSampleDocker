# Stage 1: Build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /src

# Copy csproj files and restore dependencies
COPY PromoCode.API/PromoCode.API.csproj PromoCode.API/
COPY PromoCode.Application/PromoCode.Application.csproj PromoCode.Application/
COPY PromoCode.Domain/PromoCode.Domain.csproj PromoCode.Domain/
COPY PromoCode.Infrastructure/PromoCode.Infrastructure.csproj PromoCode.Infrastructure/
COPY Chrominsky.Utils/Chrominsky.Utils.csproj Chrominsky.Utils/
RUN dotnet restore "PromoCode.API/PromoCode.API.csproj"

# Copy the remaining source code and build the project
COPY . .
WORKDIR /src/PromoCode.API
RUN dotnet build "PromoCode.API.csproj" -c Release -o /app/build

# Publish the project
RUN dotnet publish "PromoCode.API.csproj" -c Release -o /app

# Stage 2: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build-env /app .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "PromoCode.API.dll"]
