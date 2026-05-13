using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BagsakNowAPI.Models;

namespace BagsakNowAPI.Controllers
{
    [Route("api/v1/members")] // Changed from "users" to match your controller name
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly BagsakContext _context;

        public MembersController(BagsakContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var members = await _context.Members.ToListAsync();
            return Ok(new
            {
                status = "success",
                data = members, // Fixed: was 'users'
                message = "Members retrieved." // Fixed: string format
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Member not found."
                });
            }

            return Ok(new
            {
                status = "success",
                data = member, // Fixed: was 'user'
                message = "Member retrieved."
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Member newMember) // Fixed: likely 'Member' model
        {
            // Note: image_72a3a2.png doesn't show a CreatedAt column. 
            // Ensure this exists in your model/DB before using it.
            _context.Members.Add(newMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newMember.Id }, new
            {
                status = "success",
                data = newMember,
                message = "Member created."
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Member updatedMember)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Member not found."
                });
            }

            // Mapping values from image_72a3a2.png columns
            member.FullName = updatedMember.FullName;
            member.Email = updatedMember.Email;
            member.Username = updatedMember.Username;
            member.Role = updatedMember.Role;
            member.IsActive = updatedMember.IsActive;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                status = "success",
                data = member, // Fixed: was 'user'
                message = "Member updated."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Member not found."
                });
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                status = "success",
                data = (object?)null,
                message = "Member deleted."
            });
        }
    }
}