using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Encryption
{
    public class PasswordManager
    {
        public async Task<bool> Login(string email, string password)
        {
            string hash = "";
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                //return hash;
            }

            using (var cn = new SqlConnection("Data Source=datafinchisland.c4zvhtgu2ttu.us-east-1.rds.amazonaws.com,1433;Initial Catalog=Licenses;User id=admin;Password=gerald01"))
            {
                string query = @$"SELECT * FROM PrimaryLicenses WHERE Email = '{email}'";
                var result = await cn.QueryFirstOrDefaultAsync<Account>(query);
                if (result == null)
                {
                    Console.WriteLine("Email not registered!");
                    return false;
                }
                else if (result.Password == hash)
                {
                    Console.WriteLine($@"Password Accepted!");
                    Console.WriteLine($@"Welcome: {result.FirstName}");
                    return true;
                }
                else
                {
                    Console.WriteLine("Password is incorrect!");
                    return false;
                }
            }
        }
    }

    public class Account
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int MaxSubLicenses { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}