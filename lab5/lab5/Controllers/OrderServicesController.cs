using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab5.Data;
using lab5.Models;

namespace lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderServicesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public OrderServicesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/OrderServices
        [HttpGet]
        public IEnumerable<OrderServices> GetOrderServices()
        {
            return _context.OrderServices;
        }

        // GET: api/OrderServices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderServices([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderServices = await _context.OrderServices.FindAsync(id);

            if (orderServices == null)
            {
                return NotFound();
            }

            return Ok(orderServices);
        }

        // PUT: api/OrderServices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderServices([FromRoute] int id, [FromBody] OrderServices orderServices)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderServices.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderServices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderServicesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderServices
        [HttpPost]
        public async Task<IActionResult> PostOrderServices([FromBody] OrderServices orderServices)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.OrderServices.Add(orderServices);
            await _context.SaveChangesAsync();
            var new_orderServices = _context.OrderServices.Select(y => new
            {
                y.Id,
                y.Date,
                y.Count,
                y.OrderId,
                Service = new {
                    y.Service.Id,
                    y.Service.Name,
                    y.Service.Price
            }
            }).Where(x => x.Id == orderServices.Id).FirstOrDefault();

            return CreatedAtAction("GetOrderServices", new { id = orderServices.Id }, new_orderServices);
        }

        // DELETE: api/OrderServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderServices([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderServices = await _context.OrderServices.FindAsync(id);
            if (orderServices == null)
            {
                return NotFound();
            }

            _context.OrderServices.Remove(orderServices);
            await _context.SaveChangesAsync();

            return Ok(orderServices);
        }

        private bool OrderServicesExists(int id)
        {
            return _context.OrderServices.Any(e => e.Id == id);
        }
    }
}