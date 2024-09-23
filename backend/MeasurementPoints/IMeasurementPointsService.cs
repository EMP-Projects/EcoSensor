namespace EcoSensorApi.MeasurementPoints;

/// <summary>
/// Provides methods for synchronizing measurement points, seeding geographic features, and reading air quality values.
/// </summary>
public interface IMeasurementPointsService
{
    /// <summary>
    /// Synchronize the geometries read from the data sources (OpenStreetMap, etc.) and detect the measurement points
    /// starting from the centroid of each geometry, the South-West and North-East coordinates, if they are further away
    /// by a predefined constant distance from the centroid.
    /// </summary>
    /// <exception cref="Exception"></exception>
    Task<int> MeasurementPoints();

    /// <summary>
    /// Creates the database for geographic features and populates it with the data read from the data sources
    /// </summary>
    /// <returns></returns>
    Task<int> SeedFeatures();

    /// <summary>
    /// Reads air quality values from the API and stores them in the database
    /// </summary>
    /// <returns></returns>
    Task<int> AirQuality();
    
    /// <summary>
    /// Deletes old records from the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, with an integer result indicating the number of records deleted.</returns>
    Task<int> DeleteOldRecords();
    
    /// <summary>
    /// Serializes the measurements query and uploads the result to an S3 bucket.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UploadFeatureCollection();
    
    /// <summary>
    /// Gets the next timestamp based on the provided measurements query.
    /// </summary>
    /// <param name="query">The measurements query used to determine the next timestamp.</param>
    /// <returns>A task that represents the asynchronous operation, with a string result indicating the next timestamp.</returns>
    Task<string> GetNextTimeStamp(MeasurementsQuery query);
}