using NetTopologySuite.Geometries;

namespace EcoSensorApi.MeasurementPoints;

/// <summary>
/// Interface representing a collection of matrix points.
/// </summary>
public interface IMatrixPoints
{
    /// <summary>
    /// Gets or sets the count of matrix points.
    /// </summary>
    /// <value>The count of matrix points.</value>
    int Count { get; set; }

    /// <summary>
    /// Gets or sets the list of geographical points.
    /// </summary>
    /// <value>A list of geographical points.</value>
    List<Point?> Points { get; set; }
}