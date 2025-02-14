using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPortfolio.Models
{
    public class SocialMedia
    {
    public int Id { get; set; }
    public string Platform { get; set; }
    public string Url { get; set; }
    public string Icon { get; set; }
    public bool IsActive { get; set; } 
    }
}