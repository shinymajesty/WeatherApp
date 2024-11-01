using Dapper;
using System.Data.SqlClient;
using WeatherApp.Data;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class UserService : IUserService
    {
        public async Task<User?> GetUserByEmail(string email)
        {
            using var connection = new SqlConnection(DbConnectionHelper.ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM users WHERE email = @Email",
                new { Email = email });
        }

        public async Task<bool> RegisterUser(RegisterModel model)
        {
            var existingUser = await GetUserByEmail(model.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }

            using var connection = new SqlConnection(DbConnectionHelper.ConnectionString);

            // Get the next available User_ID
            var nextId = await connection.QueryFirstOrDefaultAsync<long>(
                "SELECT ISNULL(MAX(User_ID), 0) + 1 FROM users");

            await connection.ExecuteAsync(@"
                INSERT INTO users (User_ID, username, email, password_hs) 
                VALUES (@UserId, @Username, @Email, @PasswordHash)",
                new
                {
                    UserId = nextId,
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = HashPassword(model.Password)
                });

            return true;
        }

        public async Task<bool> ValidateUser(string email, string password)
        {
            try
            {
                var result = await VerifyStoredHash(email, password);
                Console.WriteLine($"ValidateUser result: {result}"); // Debug line
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ValidateUser error: {ex.Message}"); // Debug line
                throw;
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        public async Task<bool> VerifyStoredHash(string email, string password)
        {
            using var connection = new SqlConnection(DbConnectionHelper.ConnectionString);
            var user = await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT email, password_hs FROM users WHERE email = @Email",
                new { Email = email });

            if (user == null) return false;

            var inputHash = HashPassword(password);
            Console.WriteLine($"Stored hash: {user.Password_hs}"); // Debug line
            Console.WriteLine($"Input hash: {inputHash}"); // Debug line
            return user.Password_hs == inputHash;
        }
    }
}