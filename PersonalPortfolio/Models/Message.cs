using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPortfolio.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; } 

        public bool IsRead { get; set; } = false;
    }
}