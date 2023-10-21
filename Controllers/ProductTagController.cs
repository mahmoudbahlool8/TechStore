using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Controllers
{
    public class ProductTagController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductTagController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductTagController
        public async Task<IActionResult> Index()
        {
            if (_context.ProductTag != null)
            {
                return View(await _context.ProductTag.ToListAsync());
            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");

            }
        }

        // GET: ProductTagController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.ProductTag == null)
            {
                return NotFound();
            }

            var productTag = await _context.ProductTag.FirstOrDefaultAsync(m => m.Id == id);
            if (productTag == null)
            {
                return NotFound();
            }

            return View(productTag);
        }

        // GET: ProductTagController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductTagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTag productTag)
        {
            if (ModelState.IsValid)
            {
                _context.ProductTag.Add(productTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTag);
        }

        // GET: ProductTagController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductTag == null)
            {
                return NotFound();
            }

            var productTag = await _context.ProductTag.FindAsync(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }

        // POST: ProductTagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductTag productTag)
        {
            if (id != productTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ProductTag.Update(productTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTagExists(productTag.Id))
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
            return View(productTag);
        }

        // GET: ProductTagController/Delete/5


        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductTag == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductTag'  is null.");
            }
            var productTag = await _context.ProductTag.FindAsync(id);
            if (productTag != null)
            {
                _context.ProductTag.Remove(productTag);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTagExists(int id)
        {
            return (_context.ProductTag?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
