using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonalPortfolio.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    JobImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    JobStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MenuLink = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MenuIcon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PageContent = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PageSlug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ProjectImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ProjectUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ProjectGithubUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SiteLogoText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SiteDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FacebookUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TwitterUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    InstagramUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GithubUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    YoutubeUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CopyRightText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FooterDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CvUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PortfolioEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PortfolioPhone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SliderTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SliderDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SliderButtonText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SliderButtonUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Platform = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Abouts",
                columns: new[] { "Id", "CreatedDate", "Description", "ImageUrl", "IsActive", "Title" },
                values: new object[] { 1, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yazılım geliştirici olarak profesyonel deneyimlerim ve projelerim.", null, true, "Hakkımda" });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "Id", "CompanyName", "IsActive", "JobDescription", "JobEndDate", "JobImageUrl", "JobStartDate", "JobTitle" },
                values: new object[] { 1, "Company Name", true, "Software Developer", new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/experience/experience1.jpg", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Software Developer" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "IsActive", "ProjectDescription", "ProjectGithubUrl", "ProjectImageUrl", "ProjectTitle", "ProjectUrl" },
                values: new object[] { 1, true, "Kişisel portfolyo web sitesi.", "https://github.com/example", "/images/projects/project1.jpg", "Personal Portfolio", "https://example.com" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CreatedDate", "Description", "Icon", "IsActive", "Name", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Modern web teknolojileri ile web siteleri ve uygulamalar.", "fas fa-code", true, "Web Geliştirme", 1000m },
                    { 2, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "iOS ve Android için native mobil uygulamalar.", "fas fa-mobile-alt", true, "Mobil Uygulama", 2000m }
                });

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "CopyRightText", "CvUrl", "FacebookUrl", "FooterDescription", "GithubUrl", "InstagramUrl", "LinkedInUrl", "PortfolioEmail", "PortfolioPhone", "SiteDescription", "SiteLogoText", "SiteTitle", "TwitterUrl", "YoutubeUrl" },
                values: new object[] { 1, "© 2021 Personal Portfolio", "https://example.com", "https://facebook.com", "Kişisel portfolyo web sitesi.", "https://github.com", "https://instagram.com", "https://linkedin.com", "info@aemreari.com.tr", "+1234567890", "Kişisel portfolyo web sitesi.", "Portfolio", "Personal Portfolio", "https://twitter.com", "https://youtube.com" });

            migrationBuilder.InsertData(
                table: "Sliders",
                columns: new[] { "Id", "IsActive", "SliderButtonText", "SliderButtonUrl", "SliderDescription", "SliderTitle", "SliderUrl" },
                values: new object[] { 1, true, "Slider Button", "https://picsum.photos/536/354", "Slider Description", "Slider Title", "/images/slider/slider1.jpg" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "IsActive", "LastLoginDate", "LastName", "Password" },
                values: new object[] { 1, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", "Admin", true, null, "User", "ICAgICAgICAgICAgICAgIA==.dGTl1MDSxuvQEQFnaJ9UD7oFCXYhUTboC4YNT5NRxN8=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
