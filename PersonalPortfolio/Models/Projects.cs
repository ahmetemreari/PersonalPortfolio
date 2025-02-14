using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPortfolio.Models
{
    public class Projects
    {
        public int Id { get; set; }

        public string ProjectTitle { get; set; }

        public string ProjectDescription { get; set; }

        public string ProjectImageUrl { get; set; }

        public string ProjectUrl { get; set; }

        public string ProjectGithubUrl { get; set; }

        public bool IsActive { get; set; }
        

    }
}