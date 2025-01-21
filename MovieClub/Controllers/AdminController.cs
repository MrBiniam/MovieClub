using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieClub.Models;
using MovieClub.Data;
using System.Linq;
using System.Threading.Tasks;


[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly MvcTextContext _context;

    public AdminController(MvcTextContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult CustomerList()
    {
        var customers = _context.Customers.ToList();
        return View(customers);
    }

    [HttpGet]
    public async Task<IActionResult> BlockCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        customer.IsBlocked = true;
        _context.Update(customer);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(CustomerList));
    }
}
