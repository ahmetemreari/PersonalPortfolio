using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPortfolio.Models
{
    public class Menus
    {
        public int Id { get; set; }

        public string MenuTitle { get; set; }

        public string MenuLink { get; set; }

        public bool IsActive { get; set; } = true;

        public string? MenuIcon { get; set; }  
    }
}