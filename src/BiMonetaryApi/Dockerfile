FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS base
# RUN apk update && apk add libc6-compat
WORKDIR /app

ARG service_version
ENV SERVICE_VERSION ${service_version:-0.0.1}

ARG api_version
ENV API_VERSION ${api_version:-1.0}

ENV ASPNETCORE_URLS http://+:80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR .
COPY . .

WORKDIR /src/BiMonetaryApi

RUN dotnet restore -nowarn:msb3202,nu1503
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NetCoreKit.Samples.BiMonetaryApi.dll"]
