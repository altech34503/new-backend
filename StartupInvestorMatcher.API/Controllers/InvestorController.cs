using Microsoft.AspNetCore.Mvc;
using StartupInvestorMatcher.Model.Entities;
using StartupInvestorMatcher.Model.Repositories;
using System.Collections.Generic;

namespace StartupInvestorMatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestorController : ControllerBase
    {
        protected InvestorRepository Repository { get; }

        // Constructor to inject the InvestorRepository
        public InvestorController(InvestorRepository repository)
        {
            Repository = repository;
        }

        // Get a specific Investor by ID
        [HttpGet("{id}")]
        public ActionResult<Investor> GetInvestor([FromRoute] int id)
        {
            Investor investor = Repository.GetInvestorById(id);
            if (investor == null)
            {
                return NotFound($"Investor with id {id} not found");
            }
            return Ok(investor);
        }

        // Get all Investors
        [HttpGet]
        public ActionResult<IEnumerable<Investor>> GetInvestors()
        {
            return Ok(Repository.GetInvestors());
        }

        // Add a new Investor
        [HttpPost]
        public ActionResult Post([FromBody] Investor investor)
        {
            if (investor == null)
            {
                return BadRequest("Investor data is invalid");
            }

            bool status = Repository.InsertInvestor(investor);
            if (status)
            {
                return Ok(investor);
            }

            return BadRequest("Failed to insert investor");
        }

        // Update an existing Investor
        [HttpPut]
        public ActionResult UpdateInvestor([FromBody] Investor investor)
        {
            if (investor == null)
            {
                return BadRequest("Investor data is invalid");
            }

            var existingInvestor = Repository.GetInvestorById(investor.InvestorId);
            if (existingInvestor == null)
            {
                return NotFound($"Investor with id {investor.InvestorId} not found");
            }

            bool status = Repository.UpdateInvestor(investor);
            if (status)
            {
                return Ok();
            }

            return BadRequest("Failed to update investor");
        }
        
        // ...existing code...

// Delete an Investor by ID
[HttpDelete("{id}")]
public ActionResult DeleteInvestor([FromRoute] int id)
{
    var existingInvestor = Repository.GetInvestorById(id);
    if (existingInvestor == null)
    {
        return NotFound($"Investor with id {id} not found");
    }

    bool status = Repository.DeleteInvestor(id);
    if (status)
    {
        return NoContent();
    }

    return BadRequest($"Unable to delete investor with id {id}");
}}}

// ...existing code...}}

     
