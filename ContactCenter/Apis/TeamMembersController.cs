using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using presidio.Models;
using presidio.Repository;

namespace presidio.Apis
{
    [Produces("application/json")]
    [Route("api/TeamMembers")]
    public class TeamMembersController : Controller
    {
        private readonly ContactCenterDbContext _context;

        public TeamMembersController(ContactCenterDbContext context)
        {
            _context = context;
        }

        // GET: api/TeamMembers
        [HttpGet]
        public IEnumerable<TeamMembers> GetTeamMembers()
        {
            return _context.TeamMembers;
        }

        // GET: api/TeamMembers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamMembers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teamMembers = await _context.TeamMembers.SingleOrDefaultAsync(m => m.TeamMemberId == id);

            if (teamMembers == null)
            {
                return NotFound();
            }

            return Ok(teamMembers);
        }

        // PUT: api/TeamMembers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamMembers([FromRoute] int id, [FromBody] TeamMembers teamMembers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teamMembers.TeamMemberId)
            {
                return BadRequest();
            }

            _context.Entry(teamMembers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamMembersExists(id))
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

        // POST: api/TeamMembers
        [HttpPost]
        public async Task<IActionResult> PostTeamMembers([FromBody] TeamMembers teamMembers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TeamMembers.Add(teamMembers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeamMembers", new { id = teamMembers.TeamMemberId }, teamMembers);
        }

        // DELETE: api/TeamMembers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamMembers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teamMembers = await _context.TeamMembers.SingleOrDefaultAsync(m => m.TeamMemberId == id);
            if (teamMembers == null)
            {
                return NotFound();
            }

            _context.TeamMembers.Remove(teamMembers);
            await _context.SaveChangesAsync();

            return Ok(teamMembers);
        }

        private bool TeamMembersExists(int id)
        {
            return _context.TeamMembers.Any(e => e.TeamMemberId == id);
        }
    }
}