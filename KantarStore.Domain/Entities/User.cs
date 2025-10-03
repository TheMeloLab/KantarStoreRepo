using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Domain.Entities
{
    public class User
    {
        protected User()
        {

        }

        public User(string firstName, string email, string passwordHash, string? lastName = null, string? phone = null)
        {
            Id = Guid.NewGuid();     
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            PasswordHash = passwordHash;
        }

        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public string Email { get; set; } = default!;
        public string? Phone { get; set; }
        public string PasswordHash { get; set; } = default!;

    }
}
