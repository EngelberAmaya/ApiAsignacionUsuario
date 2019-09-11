using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using apiUsuarioCrud.Models;
using apiUsuarioCrud.Data.Repository;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiUsuarioCrud.Controllers
{
    [Route("api/hardware")]
    public class HardwareController : Controller
    {
        private readonly UserContext _context;
        private readonly BaseRepository _basRep;

        public HardwareController(UserContext context, BaseRepository basRep)
        {
            _context = context;
            _basRep = basRep;
            
            if (_context.Hardware.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Hardware.Add(new Hardware { HardwareName = "Mouse Optico" });
                _context.SaveChanges();
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hardware>>> GetHardware()
        {
            return await _context.Hardware.ToListAsync();
        }


        // GET: api/users/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Hardware>> GetHardware(long id)
        {
            var hardware = await _context.Hardware.FindAsync(id);

            if (hardware == null)
            {
                return NotFound();
            }

            return hardware;
        }


        [HttpPost]
        public async Task PostHardware([FromBody] Hardware hard)
        {
            
            await _basRep.InsertHardware(hard);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutHardware([FromBody] Hardware hard, long id)
        {
            await _basRep.UpdateHardware(hard, id);

            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task DeleteHardware(long id)
        {
            //await _repository.DeleteById(id);
            await _basRep.DeleteHardware(id);
        }


        /*
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
