using TeamSviluppo.Auth;
namespace EcoSensorApi.Auth;

public static class AuthEcoSensorManager
{
    public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication<UserModel, UserDto>();
        builder.Services.AddScoped<AuthEcoSensorRepository>();
        builder.Services.AddScoped<IAuthService, AuthEcoSensorService>();
        
        return builder;
    }
}