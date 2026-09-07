
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoBeard.Learn.AspNet.WebShop.App.Models;
using NoBeard.Learn.AspNet.WebShop.App.Data;

namespace NoBeard.Learn.AspNet.WebShop.App.Areas.Admin.Controllers;

[Area("Admin")]
public class OrderItemsController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderItemsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: ORDERITEMS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.OrderItems.ToListAsync());
    }

    // GET: ORDERITEMS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderitem = await _context.OrderItems
            .FirstOrDefaultAsync(m => m.Id == id);
        if (orderitem == null)
        {
            return NotFound();
        }

        return View(orderitem);
    }

    // GET: ORDERITEMS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ORDERITEMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,OrderId,ProductId,Quantity,Price,Total")] OrderItem orderitem)
    {
        if (ModelState.IsValid)
        {
            _context.Add(orderitem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(orderitem);
    }

    // GET: ORDERITEMS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderitem = await _context.OrderItems.FindAsync(id);
        if (orderitem == null)
        {
            return NotFound();
        }
        return View(orderitem);
    }

    // POST: ORDERITEMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,OrderId,ProductId,Quantity,Price,Total")] OrderItem orderitem)
    {
        if (id != orderitem.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(orderitem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(orderitem.Id))
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
        return View(orderitem);
    }

    // GET: ORDERITEMS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderitem = await _context.OrderItems
            .FirstOrDefaultAsync(m => m.Id == id);
        if (orderitem == null)
        {
            return NotFound();
        }

        return View(orderitem);
    }

    // POST: ORDERITEMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var orderitem = await _context.OrderItems.FindAsync(id);
        if (orderitem != null)
        {
            _context.OrderItems.Remove(orderitem);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OrderItemExists(int? id)
    {
        return _context.OrderItems.Any(e => e.Id == id);
    }
}
