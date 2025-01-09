using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppointDoc.Domain.DbModels
{
    public sealed class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public void SetPassword(string password) 
        { 
            PasswordHash = HashPassword(password); 
        }
        private string HashPassword(string password) 
        { 
            using (SHA256 sha256 = SHA256.Create()) 
            { 
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password)); 
                StringBuilder builder = new StringBuilder(); 
                foreach (byte b in bytes) 
                { 
                    builder.Append(b.ToString("x2")); 
                } 
                return builder.ToString(); 
            } 
        }
    }

}
