using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieClub.Models;
using MovieClub.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class CustomerController : Controller
{
    private readonly MvcTextContext _context;

    public CustomerController(MvcTextContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = User.Identity.Name;
        var customers = await _context.Customers.Where(c => c.Email == userId).ToListAsync();
        return View(customers);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null || customer.Email != User.Identity.Name)
        {
            return NotFound();
        }
        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        var existingCustomer = await _context.Customers.FindAsync(customer.Id);
        if (existingCustomer == null || existingCustomer.Email != User.Identity.Name)
        {
            return NotFound();
        }

        existingCustomer.FirstName = customer.FirstName;
        existingCustomer.LastName = customer.LastName;
        existingCustomer.PhoneNumber = customer.PhoneNumber;

        _context.Update(existingCustomer);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
