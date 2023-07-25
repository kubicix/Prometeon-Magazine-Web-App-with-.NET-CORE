using MagazineApp1.Models;
using MagazineApp1.Services;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;



namespace MagazineApp1.Controllers
{
    public class MagazineController : Controller
    {
        private readonly MagazineServices _magazineService;

        public MagazineController(MagazineServices magazineService)
        {
            _magazineService = magazineService;
        }

        public IActionResult Index(int? page = 1)
        {
            int pageSize = 6;
            List<MagazineTable> magazines = _magazineService.GetMagazines();

            int currentPage = page ?? 1;

            int totalItems = magazines.Count;
            int totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            // Eğer talep edilen sayfa sayısı geçerli bir aralıkta değilse, son sayfaya yönlendirme yapalım.
            currentPage = currentPage < 1 ? 1 : currentPage;
            currentPage = currentPage > totalPages ? totalPages : currentPage;

            int skip = (currentPage - 1) * pageSize;
            List<MagazineTable> pagedMagazines = magazines.Skip(skip).Take(pageSize).ToList();

            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = currentPage;

            return View(pagedMagazines);
        }




    }
}
