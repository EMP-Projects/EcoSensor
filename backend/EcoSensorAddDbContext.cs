using Gis.Net.Core.Entities;

namespace EcoSensorApi;

/// <inheritdoc />
public class EcoSensorAddDbContext : ApplicationDbContext<EcoSensorDbContext>
{
    /// <inheritdoc />
    public EcoSensorAddDbContext(EcoSensorDbContext context) : base(context)
    {
    }
}