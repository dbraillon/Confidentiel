using Confidentiel.Data;
using Confidentiel.Data.Entities;
using Confidentiel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Confidentiel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext applicationDbContext, ILogger<HomeController> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Secret secret)
        {
            if (secret is null) return RedirectToAction(nameof(Index));
            if (secret.Value is null) return RedirectToAction(nameof(Index));

            _applicationDbContext.Add(secret);
            _applicationDbContext.SaveChanges();

            ViewBag.SecretUrl = Url.Action(nameof(Open), "Home", new { secret.Id }, "https");

            return View();
        }

        public IActionResult Open(Guid id)
        {
            var secret = _applicationDbContext.Secrets.Find(id);
            if (secret == null) return NotFound();

            return View(id);
        }

        public IActionResult Secret(Guid id)
        {
            var secret = _applicationDbContext.Secrets.Find(id);
            if (secret == null) return NotFound();

            _applicationDbContext.Secrets.Remove(secret);
            _applicationDbContext.SaveChanges();

            return View(secret);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}