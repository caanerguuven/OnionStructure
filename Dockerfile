FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0.103 AS build
WORKDIR /src

COPY OnionStructure.sln ./
COPY OnionStructure.Contract/*.csproj ./OnionStructure.Contract/
COPY OnionStructure.Domain/*.csproj ./OnionStructure.Domain/
COPY OnionStructure.Repository/*.csproj ./OnionStructure.Repository/
COPY OnionStructure.Service/*.csproj ./OnionStructure.Service/
COPY OnionStructure.API/*.csproj ./OnionStructure.API/

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs
RUN dotnet restore --disable-parallel "OnionStructure.API/OnionStructure.API.csproj"
COPY . .
WORKDIR /src/OnionStructure.Contract
RUN dotnet build -c Release -o /app

WORKDIR /src/OnionStructure.Domain
RUN dotnet build -c Release -o /app

WORKDIR /src/OnionStructure.Repository
RUN dotnet build -c Release -o /app

WORKDIR /src/OnionStructure.Service
RUN dotnet build -c Release -o /app

WORKDIR /src/OnionStructure.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OnionStructure.API.dll"]