using System.Security.Claims;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ViewSecurity")]
    public class SecurityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SecurityController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetAccess")]
        public async Task<IActionResult> GetAccess(string userId)
        {
            // Get the user based on the id
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                // User does not exist
                return NotFound();
            }

            // Get the user's claims
            var claims = await _userManager.GetClaimsAsync(user);

            // Initializze a new access object based on which claims the user has
            var access = new Access
            {
                UserId = user.Id,
                ViewInventory = claims.Any(claim => claim.Type == "ViewInventory" && claim.Value == "true"),
                EditInventory = claims.Any(claim => claim.Type == "EditInventory" && claim.Value == "true"),
                ViewEmployees = claims.Any(claim => claim.Type == "ViewEmployees" && claim.Value == "true"),
                EditEmployees = claims.Any(claim => claim.Type == "EditEmployees" && claim.Value == "true"),
                ViewSecurity = claims.Any(claim => claim.Type == "ViewSecurity" && claim.Value == "true"),
                EditSecurity = claims.Any(claim => claim.Type == "EditSecurity" && claim.Value == "true")
            };

            return Ok(access);
        }


        [Authorize(Policy = "EditSecurity")]
        [HttpPost]
        [Route("UpdateAccess")]
        public async Task<IActionResult> UpdateAccess([FromBody] Access access)
        {
            // Initialize access claims list
            var accessClaimTypes = new[]
            {
                "ViewInventory",
                "EditInventory",
                "ViewEmployees",
                "EditEmployees",
                "ViewSecurity",
                "EditSecurity"
            };

            //Get user based on user name
            var user = await _userManager.FindByIdAsync(access.UserId);

            if (user == null)
            {
                // User does not exist
                return NotFound();
            }

            // Get user's existing claims
            var existingClaims = await _userManager.GetClaimsAsync(user);

            // Determine which of the claim types are already assigned
            var claimsToRemove = existingClaims.Where(claim => accessClaimTypes.Contains(claim.Type)).ToList();

            // If any are assigned
            if (claimsToRemove.Any())
            {
                // Remove all assigned claims from user
                var removeResult = await _userManager.RemoveClaimsAsync(user, claimsToRemove);
                
                //If the removal fails
                if (!removeResult.Succeeded)
                {
                    return BadRequest(removeResult.Errors);
                }
            }

            // Set a list with the claims based on which are set to true
            var newClaims = new List<Claim>();
            if (access.ViewInventory) newClaims.Add(new Claim("ViewInventory", "true"));
            if (access.EditInventory) newClaims.Add(new Claim("EditInventory", "true"));
            if (access.ViewEmployees) newClaims.Add(new Claim("ViewEmployees", "true"));
            if (access.EditEmployees) newClaims.Add(new Claim("EditEmployees", "true"));
            if (access.ViewSecurity) newClaims.Add(new Claim("ViewSecurity", "true"));
            if (access.EditSecurity) newClaims.Add(new Claim("EditSecurity", "true"));

            // If any claims are being added
            if (newClaims.Any())
            {
                // Add claims to the user
                var addResult = await _userManager.AddClaimsAsync(user, newClaims);
                
                // If addition fails
                if (!addResult.Succeeded)
                {
                    return BadRequest(addResult.Errors);
                }
            }

            return Ok();
        }
    }
}
