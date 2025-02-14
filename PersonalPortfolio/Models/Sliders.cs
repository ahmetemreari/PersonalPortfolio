using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPortfolio.Models
{
    public class Sliders
    {
        public int Id { get; set; }

        public string SliderUrl { get; set; }

        public string SliderTitle { get; set; }

        public string SliderDescription { get; set; }

        public string SliderButtonText { get; set; }

        public string SliderButtonUrl { get; set; }

        public bool IsActive { get; set; } = true;

       
    }
}