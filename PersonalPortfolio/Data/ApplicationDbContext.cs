using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Models;
using System;
using PersonalPortfolio.Helpers;

namespace PersonalPortfolio.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Sliders> Sliders { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Experience> Experiences { get; set; }

        public DbSet<Menus> Menus { get; set; }

        public DbSet<Pages> Pages { get; set; }

        public DbSet<SiteSettings> SiteSettings { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // SiteSettings entity configuration

            modelBuilder.Entity<SiteSettings>(entity =>
            {

                //genel ayarlar
                entity.Property(e => e.SiteTitle).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SiteLogoText).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SiteDescription).IsRequired().HasMaxLength(500);
                




                //Sosyal medya
                entity.Property(e => e.FacebookUrl).IsRequired(false).HasMaxLength(255);
                entity.Property(e => e.TwitterUrl).IsRequired(false).HasMaxLength(255);
                entity.Property(e => e.InstagramUrl).IsRequired(false).HasMaxLength(255);
                entity.Property(e => e.LinkedInUrl).IsRequired(false).HasMaxLength(255);
                entity.Property(e => e.GithubUrl).IsRequired(false).HasMaxLength(255);
                entity.Property(e => e.YoutubeUrl).IsRequired(false).HasMaxLength(255);

                //Footer Ayarları

                entity.Property(e => e.CopyRightText).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FooterDescription).IsRequired().HasMaxLength(500);
                

                // CV ve Portföy

                entity.Property(e => e.CvUrl).IsRequired(false).HasMaxLength(500);
                entity.Property(e => e.PortfolioEmail).IsRequired(false).HasMaxLength(100);
                entity.Property(e => e.PortfolioPhone).IsRequired(false).HasMaxLength(100);

             

                

            });

            // Pages entity configuration

            modelBuilder.Entity<Pages>(entity =>
            {
                entity.Property(e => e.PageTitle).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PageContent).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.PageSlug).IsRequired().HasMaxLength(100);
                entity.Property(e => e.IsActive).IsRequired();
            });

            // About entity configuration
            modelBuilder.Entity<About>(entity =>
            {
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.ImageUrl).IsRequired(false).HasMaxLength(500);
            });

            // Service entity configuration
            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e => e.Icon).IsRequired(false).HasMaxLength(100);
            });

            // Contact entity configuration
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Subject).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(1000);
            });

            // SocialMedia entity configuration
            modelBuilder.Entity<SocialMedia>(entity =>
            {
                entity.Property(e => e.Platform).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Url).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Icon).IsRequired(false).HasMaxLength(100);
            });

            // Slider entity configuration
            modelBuilder.Entity<Sliders>(entity =>
            {
                entity.Property(e => e.SliderUrl).IsRequired().HasMaxLength(255);
                entity.Property(e => e.IsActive).IsRequired();
                entity.Property(e => e.SliderTitle).IsRequired();
                entity.Property(e => e.SliderDescription).IsRequired();
                entity.Property(e => e.SliderButtonText).IsRequired();
                entity.Property(e => e.SliderButtonUrl).IsRequired();
            });

            // Projects entity configuration
            modelBuilder.Entity<Projects>(entity =>
            {
                entity.Property(e => e.ProjectTitle).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ProjectDescription).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.ProjectImageUrl).IsRequired().HasMaxLength(500);
                entity.Property(e => e.ProjectUrl).IsRequired().HasMaxLength(500);
                entity.Property(e => e.ProjectGithubUrl).IsRequired().HasMaxLength(500);
            });
            
            // Menus entity configuration

            modelBuilder.Entity<Menus>(entity =>
            {
                entity.Property(e => e.MenuTitle).IsRequired().HasMaxLength(100);
                entity.Property(e => e.MenuLink).IsRequired().HasMaxLength(100);
                entity.Property(e => e.MenuIcon).IsRequired().HasMaxLength(100);
            });
            // Experience entity configuration
            modelBuilder.Entity<Experience>(entity =>
            {
                entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.JobTitle).IsRequired().HasMaxLength(200);
                entity.Property(e => e.JobDescription).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.JobImageUrl).IsRequired().HasMaxLength(500);
            });

            // Seed Data
            modelBuilder.Entity<Projects>().HasData(
                new Projects
                {
                    Id = 1,
                    ProjectTitle = "Personal Portfolio",
                    ProjectDescription = "Kişisel portfolyo web sitesi.",
                    ProjectImageUrl = "/images/projects/project1.jpg",
                    ProjectUrl = "https://example.com",
                    ProjectGithubUrl = "https://github.com/example",
                    IsActive = true,
                });

            modelBuilder.Entity<Experience>().HasData(
                new Experience
                {
                    Id = 1,
                    CompanyName = "Company Name",
                    JobTitle = "Software Developer",
                    JobDescription = "Software Developer",
                    JobImageUrl = "/images/experience/experience1.jpg",
                    JobStartDate = DateTime.Parse("2021-01-01"),
                    JobEndDate = DateTime.Parse("2021-12-31"),
                    IsActive = true,
                });

            modelBuilder.Entity<About>().HasData(
                new About
                {
                    Id = 1,
                    Title = "Hakkımda",
                    Description = "Yazılım geliştirici olarak profesyonel deneyimlerim ve projelerim.",
                    IsActive = true,
                    CreatedDate = new DateTime(2024, 2, 14)
                });


                //admin seed data
     modelBuilder.Entity<User>().HasData(
    new User
    {
        Id = 1,
        Email = "admin@example.com",
        Password = PasswordHelper.HashPassword("admin123", forSeed: true), // forSeed parametresi eklendi
        FirstName = "Admin",
        LastName = "User",
        IsActive = true,
        CreatedDate = new DateTime(2024, 2, 14),
        LastLoginDate = null
    }
);

                // SiteSettings seed data

            modelBuilder.Entity<SiteSettings>().HasData(
                new SiteSettings
                {
                    Id = 1,
                    SiteTitle = "Personal Portfolio",
                    SiteLogoText = "Portfolio",
                    SiteDescription = "Kişisel portfolyo web sitesi.",
                    //sosyal medya
                    FacebookUrl = "https://facebook.com",
                    TwitterUrl = "https://twitter.com",
                    InstagramUrl = "https://instagram.com",
                    LinkedInUrl = "https://linkedin.com",
                    GithubUrl = "https://github.com",
                    YoutubeUrl = "https://youtube.com",
                    //footer
                    CopyRightText = "© 2021 Personal Portfolio",
                    FooterDescription = "Kişisel portfolyo web sitesi.",
                    //cv
                    CvUrl = "https://example.com",
                    PortfolioEmail = "info@aemreari.com.tr",
                    PortfolioPhone = "+1234567890",
                });

            modelBuilder.Entity<Sliders>().HasData(
                new Sliders
                {
                    Id = 1,
                    SliderUrl = "/images/slider/slider1.jpg",
                    SliderTitle = "Slider Title",
                    SliderDescription = "Slider Description",
                    SliderButtonText = "Slider Button",
                    SliderButtonUrl = "https://picsum.photos/536/354",
                    IsActive = true,
                });

            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = 1,
                    Name = "Web Geliştirme",
                    Description = "Modern web teknolojileri ile web siteleri ve uygulamalar.",
                    Price = 1000M,
                    Icon = "fas fa-code",
                    IsActive = true,
                    CreatedDate = new DateTime(2024, 2, 14)
                },
                new Service
                {
                    Id = 2,
                    Name = "Mobil Uygulama",
                    Description = "iOS ve Android için native mobil uygulamalar.",
                    Price = 2000M,
                    Icon = "fas fa-mobile-alt",
                    IsActive = true,
                    CreatedDate = new DateTime(2024, 2, 14)
                });
        }
    }
}