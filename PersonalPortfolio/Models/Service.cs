using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPortfolio.Models
{
    public class Service
    {
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
    }
}