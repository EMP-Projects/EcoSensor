FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID

WORKDIR /app

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG NUGET_USERNAME
ARG NUGET_PWD

ENV NUGET_USERNAME=${NUGET_USERNAME}
ENV NUGET_PWD=${NUGET_PWD}

WORKDIR /src
COPY ["backend/EcoSensorApi.csproj", "EcoSensorApi/"]
COPY ["backend/nuget.config", "EcoSensorApi/"]
RUN dotnet restore "EcoSensorApi/EcoSensorApi.csproj"

WORKDIR "/src/EcoSensorApi"
COPY . .
RUN dotnet build "EcoSensorApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EcoSensorApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Variabili d'ambiente per le credenziali AWS
ARG AWS_ACCESS_KEY_ID
ARG AWS_SECRET_ACCESS_KEY
ARG AWS_REGION

# Crea il file credentials nella cartella root
RUN mkdir -p /app/.aws && \
    echo "[default]" > /app/.aws/credentials && \
    echo "aws_access_key_id=$AWS_ACCESS_KEY_ID" >> /app/.aws/credentials && \
    echo "aws_secret_access_key=$AWS_SECRET_ACCESS_KEY" >> /app/.aws/credentials && \
    echo "region=$AWS_REGION" >> /app/.aws/credentials && \
    echo "[default]" > /app/.aws/config && \
    echo "region=$AWS_REGION" >> /app/.aws/config

# aggiungi ad appsettings.json le configurazioni per AWS
RUN echo "{" > /app/appsettings.json && \
    echo "  \"Logging\": {" >> /app/appsettings.json && \
    echo "    \"LogLevel\": {" >> /app/appsettings.json && \
    echo "      \"Default\": \"Information\"," >> /app/appsettings.json && \
    echo "      \"Microsoft\": \"Warning\"," >> /app/appsettings.json && \
    echo "      \"Microsoft.Hosting.Lifetime\": \"Information\"" >> /app/appsettings.json && \
    echo "    }" >> /app/appsettings.json && \
    echo "  }," >> /app/appsettings.json && \
    echo "  \"AllowedHosts\": \"*\"," >> /app/appsettings.json && \
    echo "  \"AWS\": {" >> /app/appsettings.json && \
    echo "    \"AWSProfilesLocation\": \"/app/.aws/credentials\"," >> /app/appsettings.json && \
    echo "    \"Profile\": \"default\"," >> /app/appsettings.json && \
    echo "    \"Region\": \"$AWS_REGION\"" >> /app/appsettings.json && \
    echo "  }" >> /app/appsettings.json && \
    echo "}" >> /app/appsettings.json

# imposta i permessi per credentials 
RUN chmod 600 /app/.aws/credentials

ENTRYPOINT ["dotnet", "EcoSensorApi.dll"]

