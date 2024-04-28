using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDocker.DataBase;
using WebApiDocker.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _context.Customers.ToListAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            var result =await _context.Customers.SingleOrDefaultAsync(x => x.Id==id);
            if (result == null) return NotFound();
            result.Name = customer.Name;
            result.Email = customer.Email;

            _context.Attach(result);
             await _context.SaveChangesAsync();
            return Ok(result);
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);
            if (result == null) return NotFound();
            _context.Customers.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
