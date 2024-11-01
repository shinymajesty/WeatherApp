using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByEmail(string email);
        Task<bool> RegisterUser(RegisterModel model);
        Task<bool> ValidateUser(string email, string password);
    }
}