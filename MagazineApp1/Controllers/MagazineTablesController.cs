using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MagazineApp1.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace MagazineApp1.Controllers
{
    public class MagazineTablesController : Controller
    {
        private readonly INTERNContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MagazineTablesController(INTERNContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        //public MagazineTablesController(INTERNContext context)
        //{
        //    _context = context;
        //}

        // GET: MagazineTables
        public async Task<IActionResult> Index()
        {
              return _context.MagazineTables != null ? 
                          View(await _context.MagazineTables.ToListAsync()) :
                          Problem("Entity set 'INTERNContext.MagazineTables'  is null.");
        }

        // GET: MagazineTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MagazineTables == null)
            {
                return NotFound();
            }

            var magazineTable = await _context.MagazineTables
                .FirstOrDefaultAsync(m => m.Magid == id);
            if (magazineTable == null)
            {
                return NotFound();
            }

            return View(magazineTable);
        }

        // GET: MagazineTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MagazineTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Magid,Magtitle,Magdesc,Magurl,Magdate,Magimg")] MagazineTable magazineTable, IFormFile Magimg)
        {
            if (ModelState.IsValid)
            {
                // Resmi yüklemek için wwwroot klasöründeki images altına dosya adıyla kaydedin
                if (Magimg != null && Magimg.Length > 0)
                {
                    var fileName = Path.GetFileName(Magimg.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Magimg.CopyToAsync(fileStream);
                    }
                    magazineTable.Magimg = fileName;
                }

                _context.Add(magazineTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(magazineTable);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazineTable = await _context.MagazineTables.FindAsync(id);
            if (magazineTable == null)
            {
                return NotFound();
            }
            return View(magazineTable);
        }

        // POST: MagazineTables/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Magid,Magtitle,Magdesc,Magurl,Magdate,Magimg")] MagazineTable magazineTable, IFormFile Magimg)
        {
            if (id != magazineTable.Magid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (Magimg != null && Magimg.Length > 0)
                {
                    var fileName = Path.GetFileName(Magimg.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Magimg.CopyToAsync(fileStream);
                    }
                    magazineTable.Magimg = fileName;
                }

                try
                {
                    _context.Update(magazineTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagazineTableExists(magazineTable.Magid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(magazineTable);
        }

        // GET: MagazineTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MagazineTables == null)
            {
                return NotFound();
            }

            var magazineTable = await _context.MagazineTables
                .FirstOrDefaultAsync(m => m.Magid == id);
            if (magazineTable == null)
            {
                return NotFound();
            }

            return View(magazineTable);
        }

        // POST: MagazineTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MagazineTables == null)
            {
                return Problem("Entity set 'INTERNContext.MagazineTables'  is null.");
            }
            var magazineTable = await _context.MagazineTables.FindAsync(id);
            if (magazineTable != null)
            {
                _context.MagazineTables.Remove(magazineTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MagazineTableExists(int id)
        {
          return (_context.MagazineTables?.Any(e => e.Magid == id)).GetValueOrDefault();
        }
    }
}
