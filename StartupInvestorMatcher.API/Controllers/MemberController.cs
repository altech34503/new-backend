using StartupInvestorMatcher.Model.Entities;  // Change to your actual namespace
using StartupInvestorMatcher.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace StartupInvestorMatcher.API.Controllers  // Adjust namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        protected MemberRepository Repository { get; }

        // Constructor to inject the MemberRepository
        public MemberController(MemberRepository repository)
        {
            Repository = repository;
        }

        // Get a specific Member by ID
        [HttpGet("{id}")]
        public ActionResult<Member> GetMember([FromRoute] int id)
        {
            Member member = Repository.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        // Get all Members
        [HttpGet]
        public ActionResult<IEnumerable<Member>> GetMembers()
        {
            return Ok(Repository.GetMembers());
        }

        // Add a new Member
[HttpPost]
public ActionResult Post([FromBody] Member member)
{
    if (member == null)
    {
        return BadRequest(new { message = "Member info not correct" });
    }
    bool status = Repository.InsertMember(member);
    if (status)
    {
        return Ok(new { message = "Member added successfully", data = member });
    }
    return BadRequest(new { message = "Failed to add member" });
}

// Update an existing Member
[HttpPut]
public ActionResult UpdateMember([FromBody] Member member)
{
    if (member == null)
    {
        return BadRequest(new { message = "Member info not correct" });
    }

    Member existingMember = Repository.GetMemberById(member.MemberId);
    if (existingMember == null)
    {
        return NotFound(new { message = $"Member with id {member.MemberId} not found" });
    }

    bool status = Repository.UpdateMember(member);
    if (status)
    {
        return Ok(new { message = "Member updated successfully", data = member });
    }
    return BadRequest(new { message = "Failed to update member" });
}

// Delete a Member by ID
[HttpDelete("{id}")]
public ActionResult DeleteMember([FromRoute] int id)
{
    Member existingMember = Repository.GetMemberById(id);
    if (existingMember == null)
    {
        return NotFound(new { message = $"Member with id {id} not found" });
    }

    bool status = Repository.DeleteMember(id);
    if (status)
    {
        return Ok(new { message = "Member deleted successfully" });
    }
    return BadRequest(new { message = $"Unable to delete member with id {id}" });
}}}
