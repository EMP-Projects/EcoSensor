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

# Aggiungi le credenziali AWS come variabili d'ambiente
ARG AWS_ACCESS_KEY_ID
ARG AWS_SECRET_ACCESS_KEY
ARG AWS_REGION

# Crea il file credentials nella cartella root
RUN mkdir -p ~/.aws && \
    echo "[default]" > ~/.aws/credentials && \
    echo "aws_access_key_id=$AWS_ACCESS_KEY_ID" >> ~/.aws/credentials && \
    echo "aws_secret_access_key=$AWS_SECRET_ACCESS_KEY" >> ~/.aws/credentials && \
    echo "[default]" > ~/.aws/config && \
    echo "region=$AWS_REGION" >> ~/.aws/config

# Ensure the credentials file has the correct permissions
RUN chmod 600 /root/.aws/credentials

ENTRYPOINT ["dotnet", "EcoSensorApi.dll"]