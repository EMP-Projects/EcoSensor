namespace EcoSensorApi.MeasurementPoints;

public interface IMeasurementPointsService
{
    /// <summary>
    /// Sincronizza le geometrie lette dalle sorgenti di Dati (OpenStreetMap,ecc.) e rileva i punti di misura
    /// partendo dal centroide di ogni geometria, le coordinate Sud-Ovest e Nord-Est, se sono distanti più
    /// di una distanza costante predefinita
    /// </summary>
    /// <exception cref="Exception"></exception>
    Task<int> MeasurementPoints();

    /// <summary>
    /// crea la base dati per features geografiche
    /// </summary>
    /// <returns></returns>
    Task<int> SeedFeatures();

    /// <summary>
    /// legge dalle API i valori di qualità dell'aria
    /// </summary>
    /// <returns></returns>
    Task<int> AirQuality();
}