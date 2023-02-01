FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Reda.Api/Reda.Api.csproj", "Reda.Api/"]
COPY ["src/Reda.Application/Reda.Application.csproj", "Reda.Application/"]
COPY ["src/Reda.Domain/Reda.Domain.csproj", "Reda.Domain/"]
COPY ["src/Reda.DataAccess/Reda.DataAccess.csproj", "Reda.DataAccess/"]
RUN dotnet restore "src/Reda.Api/Reda.Api.csproj"
COPY . .
WORKDIR "/src/Reda.Api"
RUN dotnet build "Reda.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Reda.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reda.Api.dll"]
