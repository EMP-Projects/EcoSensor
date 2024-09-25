using NetTopologySuite.Geometries;

namespace EcoSensorApi.MeasurementPoints;

/// <inheritdoc />
public class MatrixPoints : IMatrixPoints
{
    /// <inheritdoc />
    public int Count { get; set; }

    /// <inheritdoc />
    public List<Point?> Points { get; set; } = [];
}