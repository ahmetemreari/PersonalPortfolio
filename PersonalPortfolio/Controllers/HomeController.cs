using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Models;
using PersonalPortfolio.Data;

namespace PersonalPortfolio.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    private void SetGlobalViewBags()
    {
        ViewBag.SiteSettings = _context.SiteSettings.FirstOrDefault();
        ViewBag.LayoutMenus = _context.Menus.Where(m => m.IsActive).OrderBy(m => m.Id).ToList();
    }

    public IActionResult Index()
    {
        SetGlobalViewBags();
        ViewBag.About = _context.Abouts.FirstOrDefault(x => x.IsActive);
        ViewBag.Services = _context.Services.Where(x => x.IsActive).ToList();
        ViewBag.SocialMedia = _context.SocialMedias.Where(x => x.IsActive).ToList();
        ViewBag.Sliders = _context.Sliders.Where(x => x.IsActive).ToList();
        ViewBag.Projects = _context.Projects.Where(x => x.IsActive).ToList();
        ViewBag.Experiences = _context.Experiences.Where(x => x.IsActive).ToList();
        ViewBag.NewMessageCount = _context.Contacts.Count(x => x.IsRead == false);
        ViewBag.LastMessages = _context.Contacts.OrderByDescending(x => x.CreatedDate).Take(5).ToList();
        ViewBag.Pages = _context.Pages.Where(x => x.IsActive).ToList();
        return View();
    }


    
    [HttpGet("Page/{slug}")]
    public IActionResult Page(string slug)
    {
        SetGlobalViewBags();
        if (string.IsNullOrEmpty(slug))
            return NotFound();

        var page = _context.Pages.FirstOrDefault(x => x.PageSlug == slug && x.IsActive);
        if (page == null)
            return NotFound();

        return View(page);
    }

    public IActionResult Messages()
    {
        SetGlobalViewBags();
        var messages = _context.Contacts.OrderByDescending(x => x.CreatedDate).ToList();
        return View(messages);
    }

    public IActionResult Menus()
    {
        SetGlobalViewBags();
        var menus = _context.Menus.ToList();
        return View(menus);
    }

    public IActionResult About()
    {
        SetGlobalViewBags();
        var about = _context.Abouts.FirstOrDefault(x => x.IsActive);
        return View(about);
    }

    public IActionResult Sliders()
    {
        SetGlobalViewBags();
        var sliders = _context.Sliders.ToList();
        return View(sliders);
    }

    public IActionResult Experience()
    {
        SetGlobalViewBags();
        var experiences = _context.Experiences.Where(x => x.IsActive).ToList();
        return View(experiences);
    }

    public IActionResult Portfolio()
    {
        SetGlobalViewBags();
        var projects = _context.Projects.Where(x => x.IsActive).ToList();
        return View(projects);
    }

    public IActionResult Experiences()
    {
        SetGlobalViewBags();
        var experiences = _context.Experiences.Where(x => x.IsActive).ToList();
        return View(experiences);
    }

    public IActionResult Projects()
    {
        SetGlobalViewBags();
        var projects = _context.Projects.Where(x => x.IsActive).ToList();
        return View(projects);
    }

    public IActionResult SocialMedias()
    {
        SetGlobalViewBags();
        var socialmedia = _context.SocialMedias.Where(x => x.IsActive).ToList();
        return View(socialmedia);
    }

    public IActionResult Services()
    {
        SetGlobalViewBags();
        var services = _context.Services.Where(x => x.IsActive).ToList();
        return View(services);
    }

    public IActionResult Contact()
    {
        SetGlobalViewBags();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Contact(Contact model)
    {
        SetGlobalViewBags();
        if (ModelState.IsValid)
        {
            model.CreatedDate = DateTime.Now;
            model.IsRead = false;
            _context.Contacts.Add(model);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Mesajınız başarıyla gönderildi.";
            return RedirectToAction(nameof(Contact));
        }
        return View(model);
    }
}
