using Microsoft.AspNetCore.Mvc;
using StartupInvestorMatcher.Model.Entities;
using StartupInvestorMatcher.Model.Repositories;
using System.Collections.Generic;

namespace StartupInvestorMatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartupController : ControllerBase
    {
        protected StartupRepository Repository { get; }

        // Constructor to inject the StartupRepository
        public StartupController(StartupRepository repository)
        {
            Repository = repository;
        }

        // Get a specific Startup by ID
        [HttpGet("{id}")]
        public ActionResult<Startup> GetStartup([FromRoute] int id)
        {
            Startup startup = Repository.GetStartupById(id);
            if (startup == null)
            {
                return NotFound($"Startup with id {id} not found");
            }
            return Ok(startup);
        }

        // Get all Startups
        [HttpGet]
        public ActionResult<IEnumerable<Startup>> GetStartups()
        {
            return Ok(Repository.GetStartups());
        }

        // Add a new Startup
        [HttpPost]
        public ActionResult Post([FromBody] Startup startup)
        {
            if (startup == null)
            {
                return BadRequest("Startup data is invalid");
            }

            bool status = Repository.InsertStartup(startup);
            if (status)
            {
                return Ok();
            }

            return BadRequest("Failed to insert startup");
        }

        // Update an existing Startup
        [HttpPut]
        public ActionResult UpdateStartup([FromBody] Startup startup)
        {
            if (startup == null)
            {
                return BadRequest("Startup data is invalid");
            }

            var existingStartup = Repository.GetStartupById(startup.StartupId);
            if (existingStartup == null)
            {
                return NotFound($"Startup with id {startup.StartupId} not found");
            }

            bool status = Repository.UpdateStartup(startup);
            if (status)
            {
                return Ok();
            }

            return BadRequest("Failed to update startup");
        }

        // Delete a Startup by ID
        [HttpDelete("{id}")]
        public ActionResult DeleteStartup([FromRoute] int id)
        {
            var existingStartup = Repository.GetStartupById(id);
            if (existingStartup == null)
            {
                return NotFound($"Startup with id {id} not found");
            }

            bool status = Repository.DeleteStartup(id);
            if (status)
            {
                return NoContent();
            }

            return BadRequest($"Failed to delete startup with id {id}");
        }
    }
}
