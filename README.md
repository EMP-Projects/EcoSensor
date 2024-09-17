# EcoSensor

Progetto di Monitoraggio Ambientale

## Descrizione

Questo progetto è dedicato al monitoraggio ambientale utilizzando i dati aperti di OpenStreetMap (OSM) e le previsioni di inquinamento atmosferico. 
L'obiettivo è raccogliere, analizzare e visualizzare i dati relativi alla qualità dell'aria in diverse località, fornendo informazioni utili per la salute pubblica e la ricerca ambientale.

## Caratteristiche

- **Integrazione con OpenStreetMap**: Utilizzo dei dati geografici di OSM per mappare le aree di interesse.
- **Previsioni di Inquinamento Atmosferico**: Raccolta e analisi delle previsioni di inquinamento atmosferico da fonti affidabili.
- **Rest API per Visualizzazione dei Dati**: Interfaccia utente per visualizzare i dati di qualità dell'aria su una mappa interattiva.
- **Notifiche e Avvisi** (ToDo): Sistema di notifiche per avvisare gli utenti in caso di livelli di inquinamento elevati.

## Requisiti

- .NET 8.0
- ASP.NET Core
- Entity Framework Core
- OpenStreetMap API
- Servizi di previsioni di inquinamento atmosferico (es. OpenWeatherMap)

## Installazione

1. Clona il repository:
    ```sh
    git clone https://github.com/tuo-username/progetto-monitoraggio-ambientale.git
    ```

2. Naviga nella directory del progetto:
    ```sh
    cd progetto-monitoraggio-ambientale
    ```

3. Installa le dipendenze:
    ```sh
    dotnet restore
    ```

4. Configura le chiavi API per OpenStreetMap e il servizio di previsioni di inquinamento atmosferico nel file `appsettings.json`.

5. Avvia l'applicazione:
    ```sh
    dotnet run
    ```

## Utilizzo

1. Apri il browser e naviga all'indirizzo `http://localhost:5000`.
2. Utilizza la mappa interattiva per visualizzare i dati di qualità dell'aria nelle diverse località.
3. Configura le notifiche per ricevere avvisi in caso di livelli di inquinamento elevati.

## Contribuire

1. Fai un fork del progetto.
2. Crea un nuovo branch per la tua feature o bugfix:
    ```sh
    git checkout -b feature/nome-feature
    ```
3. Fai commit delle tue modifiche:
    ```sh
    git commit -m 'Aggiunta nuova feature'
    ```
4. Fai push del branch:
    ```sh
    git push origin feature/nome-feature
    ```
5. Apri una Pull Request.

## Licenza

Questo progetto è distribuito sotto la licenza MIT. Vedi il file `LICENSE` per maggiori dettagli.
