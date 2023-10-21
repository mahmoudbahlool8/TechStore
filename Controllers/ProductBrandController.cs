using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Controllers
{
    public class ProductBrandController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductBrandController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductBrandController
        public async Task<IActionResult> Index()
        {
            if (_context.ProductBrand != null)
            {
                return View(await _context.ProductBrand.ToListAsync());
            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");

            }
        }

        // GET: ProductBrandController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.ProductBrand == null)
            {
                return NotFound();
            }

            var productBrand = await _context.ProductBrand.FirstOrDefaultAsync(m => m.Id == id);
            if (productBrand == null)
            {
                return NotFound();
            }

            return View(productBrand);
        }

        // GET: ProductBrandController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductBrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductBrand productBrand)
        {
            if (ModelState.IsValid)
            {
                _context.ProductBrand.Add(productBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productBrand);
        }

        // GET: ProductBrandController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductBrand == null)
            {
                return NotFound();
            }

            var productBrand = await _context.ProductBrand.FindAsync(id);
            if (productBrand == null)
            {
                return NotFound();
            }
            return View(productBrand);
        }

        // POST: ProductBrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductBrand productBrand)
        {
            if (id != productBrand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ProductBrand.Update(productBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductBrandExists(productBrand.Id))
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
            return View(productBrand);
        }

        // GET: ProductBrandController/Delete/5


        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductBrand == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductBrand'  is null.");
            }
            var productBrand = await _context.ProductBrand.FindAsync(id);
            if (productBrand != null)
            {
                _context.ProductBrand.Remove(productBrand);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductBrandExists(int id)
        {
            return (_context.ProductBrand?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
