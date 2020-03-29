using lab2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab2.Controllers
{
    [Route("bd/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private ApplicationContext db;

        public OrdersController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<string> ServicesList()
        {
            var s = JsonConvert.SerializeObject(db.Orders.Include(x => x.Client).Include(x => x.Service).Select(x => new {
                Id = x.id,
                ClientId = x.Client.Id,
                ClientName = x.Client.Name,
                ServiceId = x.ServiceId, 
                ServiceName = x.Service.Name,
                ServicePrice = x.Service.Price,
                Count = x.Count, 
                Date = x.Date, 
            }).ToList());
            return s;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetOrder(int id)
        {
            var order = db.Orders.Where(t => t.id == id).Include(x => x.Client).Include(x => x.Service).Select(x => new {
                Id = x.id,
                ClientId = x.Client.Id,
                ClientName = x.Client.Name,
                ServiceId = x.ServiceId,
                ServiceName = x.Service.Name,
                ServicePrice = x.Service.Price,
                Count = x.Count,
                Date = x.Date,
            }).FirstOrDefault();
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }



        [HttpGet("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> Add(int client, int service, int count, DateTime date)
        {
            if(date == new DateTime())
            {
                date = DateTime.Now;
            }
            Order order = new Order(client, service, count, date);
            if (!TryValidateModel(order))
            {
                return BadRequest(ModelState);
            }
            db.Orders.Add(order);
            return updateDB();
        }

        [HttpGet("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            db.Orders.Remove(order);
            return updateDB();
        }

        [HttpGet("upd")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> Update(int id, int client, int service, int count, DateTime date)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                if(client > 0)
                {
                    order.СlientId = client;
                }
                if(service > 0)
                {
                    order.ServiceId = service;
                }
                if(count > 0)
                {
                    order.Count = count;
                }
                if(date != new DateTime())
                {
                    order.Date = date;
                }
                if (TryValidateModel(order))
                {
                    db.Orders.Update(order);
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
