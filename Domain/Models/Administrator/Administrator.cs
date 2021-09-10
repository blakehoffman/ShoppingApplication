using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Administrator
{
    public class Administrator
    {
        public Administrator(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Email cannot be null or empty");
            }

            if (email.Length > 100)
            {
                throw new ArgumentException("Email cannot be greater than 100 characters");
            }

            Email = email;
        }

        public string Email { get; }
    }
}
