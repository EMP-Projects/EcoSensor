using Microsoft.EntityFrameworkCore;
using TeamSviluppo.Services;
namespace EcoSensorApi;

public class EcoSensorAppDbContext : IAppDbContext
{
    private readonly EcoSensorDbContext _context;
    
    public EcoSensorAppDbContext(EcoSensorDbContext context)
    {
        _context = context;
    }
    
    public DbContext GetDbContext() => _context;
}