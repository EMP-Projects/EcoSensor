using TeamSviluppo.Auth;
using TeamSviluppo.Auth.Services;
using TeamSviluppo.Services;
namespace EcoSensorApi.Auth;

public class AuthEcoSensorService : AuthService<UserModel, UserDto>
{

    /// <inheritdoc />
    public AuthEcoSensorService(ILogger<AuthEcoSensorService> logger, AuthEcoSensorRepository repository, IConfiguration configuration, ISessionInfoService session) : base(logger, repository, configuration, session)
    {
    }
}