using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductTypeController
        public async Task<IActionResult> Index()
        {
            if (_context.ProductTypes != null)
            {
                return View(await _context.ProductTypes.ToListAsync());
            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");

            }
        }

        // GET: ProductTypeController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }

            var ProductTypes = await _context.ProductTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (ProductTypes == null)
            {
                return NotFound();
            }

            return View(ProductTypes);
        }

        // GET: ProductTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductType productTypes)
        {
            if (ModelState.IsValid)
            {
                _context.ProductTypes.Add(productTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        // GET: ProductTypeController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductTypes == null)
            {
                return NotFound();
            }

            var ProductTypes = await _context.ProductTypes.FindAsync(id);
            if (ProductTypes == null)
            {
                return NotFound();
            }
            return View(ProductTypes);
        }

        // POST: ProductTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductType ProductTypes)
        {
            if (id != ProductTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ProductTypes.Update(ProductTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypesExists(ProductTypes.Id))
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
            return View(ProductTypes);
        }

        // GET: ProductTypeController/Delete/5


        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductTypes'  is null.");
            }
            var ProductTypes = await _context.ProductTypes.FindAsync(id);
            if (ProductTypes != null)
            {
                _context.ProductTypes.Remove(ProductTypes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTypesExists(int id)
        {
            return (_context.ProductTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
