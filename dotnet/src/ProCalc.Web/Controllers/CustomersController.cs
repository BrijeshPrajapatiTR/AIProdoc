using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProCalc.Core.Domain;
using ProCalc.Core.Infrastructure;

namespace ProCalc.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> Get() => await db.Customers.AsNoTracking().ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetById(int id)
        => await db.Customers.FindAsync(id) is { } c ? Ok(c) : NotFound();

    [HttpPost]
    public async Task<ActionResult<Customer>> Post(Customer c)
    {
        db.Customers.Add(c);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = c.Id }, c);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Customer c)
    {
        if (id != c.Id) return BadRequest();
        db.Entry(c).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var c = await db.Customers.FindAsync(id);
        if (c is null) return NotFound();
        db.Customers.Remove(c);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
