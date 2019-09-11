using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiUsuarioCrud.Data.Repository;
using apiUsuarioCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiUsuarioCrud.Controllers
{
    [Route("api/assignment")]
    [ApiController]

    public class AssignmentController : Controller
    {
        private readonly UserContext _context;
        private readonly BaseRepository _basRep;

        public AssignmentController(UserContext context, BaseRepository basRep)
        {
            _context = context;
            _basRep = basRep;
            this._basRep = basRep ?? throw new ArgumentNullException(nameof(basRep));
           
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAssignment()
        {
            return await _context.Assignments.ToListAsync();
        }

        // GET: api/assignment/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignment([FromRoute] long id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var assignment = await _context.Assignment.Where(x => x.UserID == id).ToListAsync();
            var assignment = await _context.Assignments.Where(x => x.UserID == id).ToListAsync();

            if (assignment == null)
            {
                return NotFound();
            }

            return Ok(assignment);
        }

        // este codigo es para traerme los datos del hardware y software 
        //en una lista de las asignaciones por parte de un usuario en especifico

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetHardwareByUserID([FromRoute] long id)
        {

            var response = await _basRep.GetForIdUser(id);
            if (response == null) { return NotFound(); }


            return Ok(response);
        }

        
        [HttpPost]
        public async Task<IActionResult> PostAssignment([FromBody] Assignment assignment)
        {
            assignment.Software = null;
            assignment.Hardware = null;
            assignment.User = null;


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Assignments.Add(assignment);

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAssignment), new { id = assignment.UserID }, assignment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignment([FromBody] Assignment assign,long id)
        {
            await _basRep.UpdateAssignment(assign, id);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task DeleteAssign(long id)
        {
            //await _repository.DeleteById(id);
            await _basRep.DeleteAssignment(id);
        }


        /*
        //[HttpPost("delete")]
        [HttpDelete("{id}")]
        public async Task DeleteAsig(/*[FromBody] Assignment assignment,long Id)
        {
            Console.WriteLine(assignment);
            assignment.Software = null;
            assignment.Hardware = null;
            assignment.User = null;
          
            await _basRep.DeleteForIdUser(assignment,Id);

        }*/

    }

}
