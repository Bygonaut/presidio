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
    [Route("api/Agents")]
    public class AgentsController : Controller
    {
        private readonly ContactCenterDbContext _context;

        public AgentsController(ContactCenterDbContext context)
        {
            _context = context;
        }

        // GET: api/Agents
        //GET localhost:51194/api/Agents/
        [HttpGet]
        public IEnumerable<Agents> GetAgents()
        {
            return _context.Agents;
        }

        // GET: api/Agents/5
        //Gets a specific agenct
        //GET localhost:51194/api/Agents/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<Agents>), 200)]
        public async Task<IActionResult> GetAgents([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agents = await _context.Agents.SingleOrDefaultAsync(m => m.AgentId == id);

            if (agents == null)
            {
                return NotFound();
            }

            return Ok(agents);
        }

        // PUT: api/Agents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgents([FromRoute] int id, [FromBody] Agents agents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agents.AgentId)
            {
                return BadRequest();
            }

            _context.Entry(agents).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentsExists(id))
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

        // POST: api/Agents
        //This will Create An Agent
        //POST localhost:51194/api/Agents/
        /*  In postman, set to POST and add these key value pairs underneath header: 
         * Key = Content-Type, Value = application/json
         * 
         * Underneath Body, set it to raw and set to JSON(application/json)
         * enter this in Body:
                  {
                    "firstName": "Mo",
                    "lastName": "Ahmad",
                    "phone": "6314452989",
                    "teamId": 1,
                    "isSupervisor": 1
                }
        
         * Now hit post!*/
        [HttpPost]
        public async Task<IActionResult> PostAgents([FromBody] Agents agents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Agents.Add(agents);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgents", new { id = agents.AgentId }, agents);
        }

        // DELETE: api/Agents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgents([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agents = await _context.Agents.SingleOrDefaultAsync(m => m.AgentId == id);
            if (agents == null)
            {
                return NotFound();
            }

            _context.Agents.Remove(agents);
            await _context.SaveChangesAsync();

            return Ok(agents);
        }

        private bool AgentsExists(int id)
        {
            return _context.Agents.Any(e => e.AgentId == id);
        }
    }
}