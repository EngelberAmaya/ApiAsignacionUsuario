using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiUsuarioCrud.Data.Repository;
using apiUsuarioCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiUsuarioCrud.Controllers
{
    [Route("api/software")]
    public class SoftwareController : Controller
    {
        private readonly UserContext _context;
        private readonly BaseRepository _basRep;

        public SoftwareController(UserContext context, BaseRepository basRep)
        {
            _context = context;
            _basRep = basRep;
            /*
            if (_context.Software.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Software.Add(new Software { SoftwareName = "Eword" });
                _context.SaveChanges();
            }*/
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Software>>> GetSoftware()
        {
            return await _context.Software.ToListAsync();
        }


        // GET: api/software/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Software>> GetSoftware(long id)
        {
            var software = await _context.Software.FindAsync(id);

            if (software == null)
            {
                return NotFound();
            }

            return software;
        }


        [HttpPost]
        public async Task PostSoftware([FromBody] Software soft)
        {
            
            await _basRep.InsertSoftware(soft);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoftware([FromBody] Software soft, long id)
        {
            await _basRep.UpdateSoftware(soft, id);

            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task DeleteSoftware(long id)
        {
            //await _repository.DeleteById(id);
            await _basRep.DeleteSoftware(id);
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
