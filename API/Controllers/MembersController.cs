using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase
    {
        /// <summary>
        /// Retrieves all members from the database
        /// </summary>
        /// <returns>A list of all app users</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            try
            {
                var members = await context.Users.ToListAsync();
                return Ok(members);
            }
            catch (Exception)
            {
                // Log the exception here in production (e.g., using ILogger)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while retrieving members");
            }
        }

        /// <summary>
        /// Retrieves a specific member by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the member</param>
        /// <returns>The requested member if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest("Member ID cannot be empty");
                }

                var member = await context.Users.FindAsync(id);

                if (member == null)
                {
                    return NotFound($"Member with ID '{id}' not found");
                }

                return Ok(member);
            }
            catch (Exception)
            {
                // Log the exception here in production (e.g., using ILogger)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while retrieving the member");
            }
        }
    }
}
