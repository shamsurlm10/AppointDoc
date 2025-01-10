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
        public string Password { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
