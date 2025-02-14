using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPortfolio.Models
{
    public class SiteSettings
    {
        public int  Id { get; set; }

        public string SiteTitle { get; set; }

        public string SiteLogoText { get; set; }

        public string SiteDescription { get; set; }


        //sosyal medya

        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string GithubUrl { get; set; }
        public string YoutubeUrl { get; set; }


    // Footer Ayarları
    public string CopyRightText { get; set; }
    public string FooterDescription { get; set; }

    
    // CV ve Portföy
    public string CvUrl { get; set; }
    public string PortfolioEmail { get; set; }
    public string PortfolioPhone { get; set; }






    }
}