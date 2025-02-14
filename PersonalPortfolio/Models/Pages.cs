using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPortfolio.Models
{
    public class Pages
    {
        public int Id { get; set; }

        public string PageTitle { get; set; }

        public string PageContent { get; set; }

        public string PageSlug { get; set; }

        public bool IsActive { get; set; } = true;

        
    }
}