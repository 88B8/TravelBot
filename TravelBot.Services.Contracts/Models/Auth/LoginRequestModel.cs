namespace TravelBot.Services.Contracts.Models.Auth;

public class LoginRequestModel
{
    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}