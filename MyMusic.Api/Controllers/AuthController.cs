using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Api.Resources;
using MyMusic.Core.Models;

namespace MyMusic.Api.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public AuthController (IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager) {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost ("SignUp")]
        public async Task<ActionResult> SignUp (UserSignUpResource userSignUpResource) {
            var user = _mapper.Map<UserSignUpResource, User> (userSignUpResource);

            var userCreateResult = await _userManager.CreateAsync (user, userSignUpResource.Password);

            if (userCreateResult.Succeeded) {
                return Created (string.Empty, string.Empty);
            }

            return Problem (userCreateResult.Errors.First().Description, null, 500);
        }

        [HttpPost ("SignIn")]
        public async Task<ActionResult> SignIn (UserLoginResource userLoginResource) {
            var user = _userManager.Users.SingleOrDefault (u => u.UserName == userLoginResource.Email);
            if (user is null) {
                return NotFound ("User not found");
            }

            var userSignInResult = await _userManager.CheckPasswordAsync (user, userLoginResource.Password);

            if (userSignInResult) {
                return Ok ();
            }

            return BadRequest ("Email or password incorrect.");
        }

        [HttpPost ("Roles")]
        public async Task<ActionResult> CreateRole (string roleName) {
            if (string.IsNullOrWhiteSpace (roleName)) {
                return BadRequest ("Role name should be provide.");
            }

            var newRole = new Role {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync (newRole);

            if (roleResult.Succeeded) {
                return Ok ();
            }

            return Problem (roleResult.Errors.First ().Description, null, 500);
        }

        [HttpPost ("User/{userEmail}/Role")]
        public async Task<ActionResult> AddUserToRole (string userEmail, [FromBody] string role) {
            var user = _userManager.Users.SingleOrDefault (u => u.UserName == userEmail);

            var result = await _userManager.AddToRoleAsync(user, role);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Problem(result.Errors.First().Description, null, 500);
        }
    }
}