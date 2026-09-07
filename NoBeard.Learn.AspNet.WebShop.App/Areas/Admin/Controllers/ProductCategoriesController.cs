
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoBeard.Learn.AspNet.WebShop.App.Models;
using NoBeard.Learn.AspNet.WebShop.App.Data;

namespace NoBeard.Learn.AspNet.WebShop.App.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductCategoriesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductCategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: PRODUCTCATEGORYS
    public async Task<IActionResult> Index(int productId)    
    {
        var results = _context.ProductCategories
            .Where(_ => _.ProductId == productId)
            .ToList();

        return View(results);
    }

    // GET: PRODUCTCATEGORYS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var productcategory = await _context.ProductCategories
            .FirstOrDefaultAsync(m => m.Id == id);
        if (productcategory == null)
        {
            return NotFound();
        }

        return View(productcategory);
    }

    // GET: PRODUCTCATEGORYS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PRODUCTCATEGORYS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,ProductId,Product,CategoryId,Category")] ProductCategory productcategory)
    {
        ModelState.Remove("Product");
        ModelState.Remove("Category");

        if (ModelState.IsValid)
        {
            _context.Add(productcategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(productcategory);
    }

    // GET: PRODUCTCATEGORYS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var productcategory = await _context.ProductCategories.FindAsync(id);
        if (productcategory == null)
        {
            return NotFound();
        }
        return View(productcategory);
    }

    // POST: PRODUCTCATEGORYS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,ProductId,Product,CategoryId,Category")] ProductCategory productcategory)
    {
        if (id != productcategory.Id)
        {
            return NotFound();
        }

        ModelState.Remove("Product");
        ModelState.Remove("Category");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(productcategory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(productcategory.Id))
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
        return View(productcategory);
    }

    // GET: PRODUCTCATEGORYS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var productcategory = await _context.ProductCategories
            .FirstOrDefaultAsync(m => m.Id == id);
        if (productcategory == null)
        {
            return NotFound();
        }

        return View(productcategory);
    }

    // POST: PRODUCTCATEGORYS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var productcategory = await _context.ProductCategories.FindAsync(id);
        if (productcategory != null)
        {
            _context.ProductCategories.Remove(productcategory);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductCategoryExists(int? id)
    {
        return _context.ProductCategories.Any(e => e.Id == id);
    }
}
