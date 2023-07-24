using MagazineApp1.Models;
using MagazineApp1.Services;
using Microsoft.AspNetCore.Mvc;


namespace MagazineApp1.Controllers
{
    public class MagazineController : Controller
    {
        private readonly MagazineServices _magazineService;

        public MagazineController(MagazineServices magazineService)
        {
            _magazineService = magazineService;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 6;
            List<MagazineTable> magazines = _magazineService.GetMagazines();

            // Sayfalama işlemleri
            int totalItems = magazines.Count;
            int totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            int skip = (page - 1) * pageSize;
            List<MagazineTable> pagedMagazines = magazines.Skip(skip).Take(pageSize).ToList();

            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = page;

            return View(pagedMagazines);
        }

    }
}
