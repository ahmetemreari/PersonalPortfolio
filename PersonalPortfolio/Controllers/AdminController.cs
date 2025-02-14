using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PersonalPortfolio.Data;
using PersonalPortfolio.Models;

namespace PersonalPortfolio.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // Dashboard
        public IActionResult Index()
        {
            ViewBag.ServiceCount = _context.Services.Count();
            ViewBag.ContactCount = _context.Contacts.Count(x => !x.IsRead);
            ViewBag.SocialMediaCount = _context.SocialMedias.Count();
            ViewBag.ProjectCount = _context.Projects.Count();
            ViewBag.ExperienceCount = _context.Experiences.Count();
            ViewBag.NewMessageCount = _context.Contacts.Count(x => !x.IsRead);
            ViewBag.LastMessages = _context.Contacts.OrderByDescending(x => x.CreatedDate).Take(5).ToList();
            return View();
        }

        #region About CRUD
        public IActionResult About()
        {
            var about = _context.Abouts.FirstOrDefault() ?? new About();
            return View(about);
        }

        [HttpPost]
        public async Task<IActionResult> About([Bind("Id,Title,Description,ImageUrl,IsActive")] About model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingAbout = await _context.Abouts.FirstOrDefaultAsync();
                    if (existingAbout == null)
                    {
                        _context.Add(model);
                    }
                    else
                    {
                        existingAbout.Title = model.Title;
                        existingAbout.Description = model.Description;
                        existingAbout.IsActive = model.IsActive;
                        if (!string.IsNullOrEmpty(model.ImageUrl))
                        {
                            existingAbout.ImageUrl = model.ImageUrl;
                        }
                        _context.Update(existingAbout);
                    }

                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Hakkımda bilgileri başarıyla güncellendi.";
                    return RedirectToAction(nameof(About));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kaydetme sırasında bir hata oluştu: " + ex.Message);
                }
            }
            return View(model);
        }
        #endregion

        #region Experience CRUD
        public async Task<IActionResult> Experiences()
        {
            var experiences = await _context.Experiences.ToListAsync();
            return View(experiences);
        }

        public IActionResult CreateExperience()
        {
            return View(new Experience { JobStartDate = DateTime.Today, JobEndDate = DateTime.Today, IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExperience([Bind("Id,CompanyName,JobTitle,JobDescription,JobImageUrl,JobStartDate,JobEndDate,IsActive")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(experience);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Deneyim başarıyla eklendi.";
                    return RedirectToAction(nameof(Experiences));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kaydetme sırasında bir hata oluştu: " + ex.Message);
                }
            }
            return View(experience);
        }

        public async Task<IActionResult> EditExperience(int? id)
        {
            if (id == null) return NotFound();

            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null) return NotFound();

            return View(experience);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExperience(int id, [Bind("Id,CompanyName,JobTitle,JobDescription,JobImageUrl,JobStartDate,JobEndDate,IsActive")] Experience experience)
        {
            if (id != experience.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experience);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Deneyim başarıyla güncellendi.";
                    return RedirectToAction(nameof(Experiences));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienceExists(experience.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(experience);
        }

        public async Task<IActionResult> DeleteExperience(int? id)
        {
            if (id == null) return NotFound();

            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null) return NotFound();

            return View(experience);
        }

        [HttpPost, ActionName("DeleteExperience")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExperienceConfirmed(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience != null)
            {
                _context.Experiences.Remove(experience);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Deneyim başarıyla silindi.";
            }
            return RedirectToAction(nameof(Experiences));
        }

        private bool ExperienceExists(int id)
        {
            return _context.Experiences.Any(e => e.Id == id);
        }
        #endregion

        #region Project CRUD
        public async Task<IActionResult> Projects()
        {
            var projects = await _context.Projects.ToListAsync();
            return View(projects);
        }

        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProject([Bind("Id,ProjectTitle,ProjectDescription,ProjectImageUrl,ProjectUrl,ProjectGithubUrl,IsActive")] Projects project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(project);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Proje başarıyla eklendi.";
                    return RedirectToAction(nameof(Projects));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kaydetme sırasında bir hata oluştu: " + ex.Message);
                }
            }
            return View(project);
        }

        public async Task<IActionResult> EditProject(int? id)
        {
            if (id == null) return NotFound();

            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProject(int id, [Bind("Id,ProjectTitle,ProjectDescription,ProjectImageUrl,ProjectUrl,ProjectGithubUrl,IsActive")] Projects project)
        {
            if (id != project.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Proje başarıyla güncellendi.";
                    return RedirectToAction(nameof(Projects));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(project);
        }

        public async Task<IActionResult> DeleteProject(int? id)
        {
            if (id == null) return NotFound();

            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            return View(project);
        }

        [HttpPost, ActionName("DeleteProject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProjectConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Proje başarıyla silindi.";
            }
            return RedirectToAction(nameof(Projects));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
        #endregion

        #region Service CRUD
        public async Task<IActionResult> Services()
        {
            var services = await _context.Services.ToListAsync();
            return View(services);
        }

        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService([Bind("Id,Name,Description,Price,Icon,IsActive")] Service service)
        {
            if (ModelState.IsValid)
            {
                service.CreatedDate = DateTime.Now;
                _context.Add(service);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Servis başarıyla eklendi.";
                return RedirectToAction(nameof(Services));
            }
            return View(service);
        }

        public async Task<IActionResult> EditService(int? id)
        {
            if (id == null) return NotFound();

            var service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound();

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(int id, [Bind("Id,Name,Description,Price,Icon,IsActive,CreatedDate")] Service service)
        {
            if (id != service.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Servis başarıyla güncellendi.";
                    return RedirectToAction(nameof(Services));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(service);
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
        #endregion

        #region Contact Messages
        public async Task<IActionResult> Messages()
        {
            var messages = await _context.Contacts.OrderByDescending(x => x.CreatedDate).ToListAsync();
            return View(messages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var message = await _context.Contacts.FindAsync(id);
            if (message != null)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Mesaj okundu olarak işaretlendi.";
            }
            return RedirectToAction(nameof(Messages));
        }
        #endregion
 //Menüler
        #region menus

        public async Task<IActionResult> Menus()
        {
            var menus = await _context.Menus.ToListAsync();
            return View(menus);
        }

        public IActionResult CreateMenu()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMenu([Bind("Id,MenuTitle,MenuLink,MenuIcon")] Menus menu)
        {
            if (ModelState.IsValid)
            {
                menu.IsActive = true; // Varsayılan olarak aktif
                _context.Add(menu);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Menü başarıyla eklendi.";
                return RedirectToAction(nameof(Menus));
            }
            return View(menu);
        }

        public async Task<IActionResult> EditMenu(int? id)
        {
            if (id == null) return NotFound();

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null) return NotFound();

            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditMenu(int id, [Bind("Id,MenuTitle,MenuLink,MenuIcon")] Menus menu)
        {
            if (id != menu.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Menü başarıyla güncellendi.";
                    return RedirectToAction(nameof(Menus));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(menu);
        }

        public async Task<IActionResult> DeleteMenu(int? id)
        {
            if (id == null) return NotFound();

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null) return NotFound();

            return View(menu);
        }

        [HttpPost, ActionName("DeleteMenu")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMenuConfirmed(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Menü başarıyla silindi.";
            }
            return RedirectToAction(nameof(Menus));
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
        #endregion

        #region Social Media CRUD

        public async Task<IActionResult> SocialMedias()
        {
            var socialmedias = await _context.SocialMedias.ToListAsync();
            return View(socialmedias);
        }

        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSocialMedia([Bind("Id,Platform,Url,Icon,IsActive")] SocialMedia socialMedia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socialMedia);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Sosyal medya hesabı başarıyla eklendi.";
                return RedirectToAction(nameof(SocialMedias));
            }
            return View(socialMedia);
        }

        public async Task<IActionResult> EditSocialMedia(int? id)
        {
            if (id == null) return NotFound();

            var socialMedia = await _context.SocialMedias.FindAsync(id);
            if (socialMedia == null) return NotFound();

            return View(socialMedia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditSocialMedia(int id, [Bind("Id,Platform,Url,Icon,IsActive")] SocialMedia socialMedia)
        {
            if (id != socialMedia.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socialMedia);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Sosyal medya hesabı başarıyla güncellendi.";
                    return RedirectToAction(nameof(SocialMedias));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocialMediaExists(socialMedia.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(socialMedia);
        }

        public async Task<IActionResult> DeleteSocialMedia(int? id)
        {
            if (id == null) return NotFound();

            var socialMedia = await _context.SocialMedias.FindAsync(id);
            if (socialMedia == null) return NotFound();

            return View(socialMedia);
        }

        [HttpPost, ActionName("DeleteSocialMedia")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteSocialMediaConfirmed(int id)
        {
            var socialMedia = await _context.SocialMedias.FindAsync(id);
            if (socialMedia != null)
            {
                _context.SocialMedias.Remove(socialMedia);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Sosyal medya hesabı başarıyla silindi.";
            }
            return RedirectToAction(nameof(SocialMedias));
        }

        private bool SocialMediaExists(int id)
        {
            return _context.SocialMedias.Any(e => e.Id == id);
        }

        #endregion

        #region Slider CRUD

        public async Task<IActionResult> Sliders()
        {
            var sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }

        public IActionResult CreateSlider()
        {
            return View();
        }

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> CreateSlider([Bind("Id,SliderUrl,IsActive,SliderTitle,SliderDescription,SliderButtonUrl,SliderButtonText")] Sliders slider, IFormFile? file)
{
    if (ModelState.IsValid)
    {
        try
        {
            if (file != null && file.Length > 0)
            {
                // Güvenli dosya adı oluştur
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var uploadPath = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "sliders");
                
                // Klasör yoksa oluştur
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Dosyayı kaydet
                var filePath = Path.Combine(uploadPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                slider.SliderUrl = $"/uploads/sliders/{fileName}";
            }
            else if (string.IsNullOrEmpty(slider.SliderUrl))
            {
                ModelState.AddModelError("", "Lütfen bir resim yükleyin veya URL girin.");
                return View(slider);
            }

            slider.IsActive = true; // Varsayılan olarak aktif
            _context.Add(slider);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "Slider başarıyla eklendi.";
            return RedirectToAction(nameof(Sliders));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Kaydetme sırasında bir hata oluştu: " + ex.Message);
        }
    }
    return View(slider);
}
        public async Task<IActionResult> EditSlider(int? id)
        {
            if (id == null) return NotFound();

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();

            return View(slider);
        }

       [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> EditSlider(int id, [Bind("Id,SliderUrl,IsActive,SliderTitle,SliderDescription,SliderButtonUrl,SliderButtonText")] Sliders slider, IFormFile? file)
{
    if (id != slider.Id) return NotFound();

    if (ModelState.IsValid)
    {
        try
        {
            var existingSlider = await _context.Sliders.FindAsync(id);
            if (existingSlider == null) return NotFound();

            // Yeni dosya yüklendiyse
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var uploadPath = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "sliders");
                
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                
                var filePath = Path.Combine(uploadPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                
                existingSlider.SliderUrl = $"/uploads/sliders/{fileName}";
            }

            // Diğer alanları güncelle
            existingSlider.SliderTitle = slider.SliderTitle;
            existingSlider.SliderDescription = slider.SliderDescription;
            existingSlider.SliderButtonText = slider.SliderButtonText;
            existingSlider.SliderButtonUrl = slider.SliderButtonUrl;
            existingSlider.IsActive = slider.IsActive;

            _context.Update(existingSlider);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Slider başarıyla güncellendi.";
            return RedirectToAction(nameof(Sliders));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu: " + ex.Message);
        }
    }
    return View(slider);
}
        public async Task<IActionResult> DeleteSlider(int? id)
        {
            if (id == null) return NotFound();

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();

            return View(slider);
        }

        [HttpPost, ActionName("DeleteSlider")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteSliderConfirmed(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider != null)
            {
                _context.Sliders.Remove(slider);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Slider başarıyla silindi.";
            }
            return RedirectToAction(nameof(Sliders));
        }

        private bool SliderExists(int id)
        {
            return _context.Sliders.Any(e => e.Id == id);
        }

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ToggleActive(int id)  // ToogleActive yerine ToggleActive olarak düzeltildi
{
    var slider = await _context.Sliders.FindAsync(id);
    if (slider != null)
    {
        slider.IsActive = !slider.IsActive;
        await _context.SaveChangesAsync();
        TempData["Success"] = slider.IsActive ? 
            "Slider başarıyla aktif edildi." : 
            "Slider başarıyla pasif edildi.";
    }
    return RedirectToAction(nameof(Sliders));
}

        #endregion

        #region Message CRUD

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteMessage(int id)
{
    var message = await _context.Contacts.FindAsync(id);
    if (message != null)
    {
        _context.Contacts.Remove(message);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Mesaj başarıyla silindi.";
    }
    return RedirectToAction(nameof(Messages));
}

        #endregion


        #region Page CRUD
    public async Task<IActionResult> Pages()
    {
        var pages = await _context.Pages.ToListAsync();
            return View(pages);
        }

        //create
        public IActionResult CreatePage()
        {
            return View();
        }

        //create post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePage([Bind("Id,PageTitle,PageContent,PageSlug,IsActive")] Pages page)
        {
            if (ModelState.IsValid)
            {
                _context.Add(page);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Sayfa başarıyla eklendi.";
                return RedirectToAction(nameof(Pages));
            }
            return View(page);
        }

        //edit
        public async Task<IActionResult> EditPage(int? id)
        {
            if (id == null) return NotFound();

            var page = await _context.Pages.FindAsync(id);
            if (page == null) return NotFound();

            return View(page);
        }
        //edit post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditPage(int id, [Bind("Id,PageTitle,PageContent,PageSlug,IsActive")] Pages page)
        {
            if (id != page.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(page);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Sayfa başarıyla güncellendi.";
                    return RedirectToAction(nameof(Pages));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(page);
        }

        //delete
        public async Task<IActionResult> DeletePage(int? id)
        {
            if (id == null) return NotFound();

            var page = await _context.Pages.FindAsync(id);
            if (page == null) return NotFound();

            return View(page);
        }

        //delete post

        [HttpPost, ActionName("DeletePage")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeletePageConfirmed(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page != null)
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Sayfa başarıyla silindi.";
            }
            return RedirectToAction(nameof(Pages));
        }

        private bool PageExists(int id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }

        #endregion
#region SiteSettings CRUD
public IActionResult SiteSettings()
{
   var settings = _context.SiteSettings.FirstOrDefault();
   if (settings == null)
   {
       settings = new SiteSettings 
       { 
           // Varsayılan değerleri burada ayarlayın
           SiteTitle = "Varsayılan Site Başlığı",
           SiteLogoText = "Varsayılan Logo",
           SiteDescription = "Varsayılan Site Açıklaması", // Eklendi
           CopyRightText = "Tüm hakları saklıdır",
           FooterDescription = "Varsayılan Footer Açıklaması"
       };
       _context.SiteSettings.Add(settings);
       _context.SaveChanges();
   }
   return View(settings);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> SiteSettings([Bind("Id,SiteTitle,SiteLogoText,SiteDescription,FacebookUrl,TwitterUrl,InstagramUrl,LinkedInUrl,GithubUrl,YoutubeUrl,CopyRightText,FooterDescription,CvUrl,PortfolioEmail,PortfolioPhone")] SiteSettings model)
{
    if (ModelState.IsValid)
    {
        try
        {
            var settings = await _context.SiteSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                _context.Add(model);
            }
            else
            {
                // Genel Site Ayarları
                settings.SiteTitle = model.SiteTitle;
                settings.SiteLogoText = model.SiteLogoText;
                settings.SiteDescription = model.SiteDescription; // Eklendi

                // Sosyal Medya
                settings.FacebookUrl = model.FacebookUrl;
                settings.TwitterUrl = model.TwitterUrl;
                settings.InstagramUrl = model.InstagramUrl;
                settings.LinkedInUrl = model.LinkedInUrl;
                settings.GithubUrl = model.GithubUrl;
                settings.YoutubeUrl = model.YoutubeUrl;

                // Footer Ayarları
                settings.CopyRightText = model.CopyRightText;
                settings.FooterDescription = model.FooterDescription;

                // CV ve Portföy
                settings.CvUrl = model.CvUrl;
                settings.PortfolioEmail = model.PortfolioEmail;
                settings.PortfolioPhone = model.PortfolioPhone;

                _context.Update(settings);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Site ayarları başarıyla güncellendi.";
            return RedirectToAction(nameof(SiteSettings));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu: " + ex.Message);
        }
    }

    // ModelState hataları varsa
    foreach (var modelState in ModelState.Values)
    {
        foreach (var error in modelState.Errors)
        {
            ModelState.AddModelError("", error.ErrorMessage);
        }
    }
    
    return View(model);
}
#endregion
        
       
}
}