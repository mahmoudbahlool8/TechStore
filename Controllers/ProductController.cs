using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;
using System.Data;
using TechStore.ViewModels;
using TechStore.Utility;
using NToastNotify;

namespace TechStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;
        private readonly IToastNotification _toastNotification;

        public ProductController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IToastNotification toastNotification)
        {
            _context = context;
            _userManager = userManager;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index(string word = "", int BrandId = 0)
        {

            IEnumerable<Product> Products;

            if (BrandId > 0 && !String.IsNullOrEmpty(word))
            {
                Products = await _context.Products.Include(p => p.ProductBrand).Include(t => t.ProductType).Include(g => g.ProductTag).Where(b => b.ProductBrandId == BrandId && b.Name.Contains(word)).OrderBy(p => p.Name).ToArrayAsync();
            }
            else if (BrandId > 0)
            {
                Products = await _context.Products.Include(p => p.ProductBrand).Include(t => t.ProductType).Include(g => g.ProductTag).Where(b => b.ProductBrandId == BrandId).OrderBy(p => p.Name).ToArrayAsync();
            }
            else if (!String.IsNullOrEmpty(word))
            {
                Products = await _context.Products.Include(p => p.ProductBrand).Include(t => t.ProductType).Include(g => g.ProductTag).Where(b => b.Name.Contains(word)).OrderBy(p => p.Name).ToArrayAsync();
            }
            else
            {
                Products = await _context.Products.Include(p => p.ProductBrand).Include(t => t.ProductType).Include(g => g.ProductTag).OrderBy(p => p.Name).ToArrayAsync();
            }

            IEnumerable<ProductBrand> Brands = await _context.ProductBrand.OrderBy(p => p.BrandName).ToArrayAsync();
            ProductSearchViewModel SearchVM = new()
            {
                products = Products,
                productBrands = Brands,
                productBrandId = BrandId,
                SearchWord = word
            };

            if (User.IsInRole("Admin"))
                return View(SearchVM);

            return View("Products", SearchVM);
        }



        // GET: Product/Edit/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p => p.ProductBrand).Include(t => t.ProductType).Include(g => g.ProductTag).FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            List<Product> products = new List<Product>();

            var product = await _context.Products.Include(p => p.ProductBrand).Include(t => t.ProductType).Include(g => g.ProductTag).FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products = HttpContext.Session.Get<List<Product>>("Products");
            if (products is null)
            {
                products = new List<Product>();
            }
            products.Add(product);
            HttpContext.Session.Set("Products", products);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cart()
        {
            List<Product> products = new List<Product>();
            
            products = HttpContext.Session.Get<List<Product>>("Products");
            if (products is null)
            {
                products = new List<Product>();
            }
            
            return View(products);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int? id,String view)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("Products");

            if (products is not null)
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product is not null)
                {
                    products.Remove(product);
                    _toastNotification.AddSuccessToastMessage("Product removed successfully");
                    HttpContext.Session.Set("Products", products);
                }
            }
            if (view == "2")
            {
                return View("Cart", products);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            var productVM = new ProductViewModel
            {
                productType = await _context.ProductTypes.OrderBy(n => n.TypeName).ToListAsync(),
                productTag = await _context.ProductTag.OrderBy(n => n.TagName).ToListAsync(),
                productBrand = await _context.ProductBrand.OrderBy(n => n.BrandName).ToListAsync(),
            } ;
            
            return View(productVM);
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Image is null)
                    product.Image = "data:image/png;base64,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";

                _context.Products.Add(product);
                _toastNotification.AddSuccessToastMessage("Product created successfully");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productVM = new ProductViewModel
            {
                Name = product.Name,
                Image = product.Image,
                Description = product.Description,
                IsAvailable = product.IsAvailable,
                Price = product.Price,
                ProductBrandId = product.ProductBrandId,
                ProductTypeId = product.ProductTypeId,
                ProductTagId = product.ProductTagId,
                productType = await _context.ProductTypes.OrderBy(n => n.TypeName).ToListAsync(),
                productTag = await _context.ProductTag.OrderBy(n => n.TagName).ToListAsync(),
                productBrand = await _context.ProductBrand.OrderBy(n => n.BrandName).ToListAsync()
            };

            return View(productVM);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Products.Update(product);
                    _toastNotification.AddSuccessToastMessage("Product updated successfully");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }


        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _toastNotification.AddSuccessToastMessage("Product removed successfully");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public JsonResult getProducts()
        {
            var Products = _context.Products.ToList();
            return Json(Products);
        }

    }
}
