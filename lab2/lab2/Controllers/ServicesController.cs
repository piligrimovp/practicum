using lab2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace lab2.Controllers
{
    [Route("bd/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private ApplicationContext db;

        public ServicesController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<string> ServicesList()
        {
            return JsonConvert.SerializeObject(db.Services.Select(x => new {
                x.Id,
                x.Name,
                x.Price
            }).ToList());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetService(int id)
        {
            var service = db.Services.Where(x => x.Id == id)
                .Select(x => new { 
                    x.Id,
                    x.Name,
                    x.Price
                });
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        [HttpGet("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> Add(string name, double price)
        {
            Service service = new Service(name, price);
            if (!TryValidateModel(service))
            {
                return BadRequest(ModelState);
            }
            db.Services.Add(service);
            return updateDB();
        }

        [HttpGet("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> Delete(int id)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }
            db.Services.Remove(service);
            return updateDB();
        }

        [HttpGet("upd")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> Update(int id, string name, double price)
        {
            Service service = db.Services.Find(id);
            if (service != null)
            {
                if(name != null)
                {
                    service.Name = name;
                }
                service.Price = price;
                if (TryValidateModel(service))
                {
                    db.Services.Update(service);
                    return updateDB();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            return NotFound();
        }

        private ActionResult updateDB()
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e);
            }
            return Ok();
        }
    }
}
