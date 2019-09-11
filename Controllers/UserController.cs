using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// agregado
using Microsoft.EntityFrameworkCore;
using apiUsuarioCrud.Models;
using apiUsuarioCrud.Data.Repository;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiUsuarioCrud.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserContext _context;
        private readonly BaseRepository _basRep;

        public UserController(UserContext context,BaseRepository basRep)
        {
            _context = context;
            _basRep = basRep;
            
            if (_context.Users.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Users.Add(new User { UserName = "Jose7", Name = "Jose", LastName = "Perez", Age = 20, LastSessionDateTime = DateTime.Now });
                _context.SaveChanges();
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/users/1
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        

        [HttpPost]
        public async Task Post([FromBody] User user)
        {
            //Console.WriteLine(user);
            //await _context.Insert(user);
            await _basRep.Insert(user);
        }





        /*
        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }*/


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromBody] User user,long id)
        {
           

            //_context.Entry(user).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            await _basRep.UpdateUser(user, id);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task DeleteUser(long id)
        {
            //await _repository.DeleteById(id);
            await _basRep.DeleteById(id);
        }


        

        /*
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/


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
        }

        */
    }
}
