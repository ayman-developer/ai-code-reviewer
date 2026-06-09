using System;
using System.IO;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EnterpriseApp.Services
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Raw text password field
        public string Email { get; set; }
    }

    public class UserService
    {
        private readonly string _connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";

        // FLAW 1: Storing and printing raw plain-text passwords (Major Security Issue)
        public void RegisterUser(User user)
        {
            if (user == null) return;

            Console.WriteLine($"Registering user {user.Username} with password: {user.Password}");
            SaveUserToDatabase(user);
        }

        // FLAW 2: Direct string concatenation in SQL queries (SQL Injection Vulnerability)
        private void SaveUserToDatabase(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (Username, Password, Email) VALUES ('" + user.Username + "', '" + user.Password + "', '" + user.Email + "')";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // FLAW 3: Missing a 'using' statement or .Dispose() on Stream (Resource Leak)
        public void ExportUserAuditLog(string filePath, string logData)
        {
            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine($"{DateTime.UtcNow}: {logData}");
            // File is never closed or disposed properly!
        }

        // FLAW 4: Async method using 'async void' instead of 'async Task' (Bad Async Practice)
        // This makes exception handling impossible for the caller and can crash the process.
        public async void SendWelcomeEmailAsync(string email)
        {
            await Task.Delay(1000); // Simulating email delay
            Console.WriteLine($"Welcome email sent successfully to: {email}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing User Management System App...");
            UserService userService = new UserService();

            User newUser = new User 
            { 
                Id = 1, 
                Username = "test_user", 
                Password = "SuperSecretPassword123!", 
                Email = "user@example.com" 
            };

            userService.RegisterUser(newUser);
            userService.ExportUserAuditLog("audit.txt", "User registered successfully.");
            userService.SendWelcomeEmailAsync(newUser.Email);
        }
    }
}
