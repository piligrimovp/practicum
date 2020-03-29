using lab2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace lab2.Controllers
{
    [Route("bd/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private ApplicationContext db;

        public ClientsController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IEnumerable<Client> ClientsList()
        {
            return db.Clients;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetClient(int id)
        {
            Client client = db.Clients.Find(id);
            if(client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpGet("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> Add(string name, int age)
        {
            Client client = new Client(name, age);
            if (!TryValidateModel(client))
            {
                return BadRequest(ModelState);
            }
            db.Clients.Add(client);
            return updateDB();
        }

        [HttpGet("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> Delete(int id)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            db.Clients.Remove(client);
            return updateDB();
        }

        [HttpGet("upd")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> Update(int id, string name, int age)
        {
            Client client = db.Clients.Find(id);
            if (client != null)
            {
                client.Name = name;
                client.Age  = age;
                if(TryValidateModel(client))
                {
                    db.Clients.Update(client);
                    return updateDB();
                } else
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
