using TeamSviluppo.Auth;
using TeamSviluppo.Repositories;
using TeamSviluppo.Services;
namespace EcoSensorApi.Auth;

public class AuthEcoSensorRepository : AuthRepository<UserModel, UserDto>
{

    /// <inheritdoc />
    public AuthEcoSensorRepository(ILogger<AuthEcoSensorRepository> logger, IAppDbContext context, RepositoryDependencies repositoryDependencies) : 
        base(logger, context, repositoryDependencies)
    {
    }
}