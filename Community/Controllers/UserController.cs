using Community.Repository.Entities;
using Community.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Community.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userRepo;
        public UserController(IUser userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Invalid user data.");

            var createdUser = _userRepo.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userRepo.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("GetUserById/{id:int}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepo.GetUserById(id);
            if (user == null)
                return NotFound($"No user found with ID {id}.");
            return Ok(user);
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] User user)
        {
            if (user == null || user.UserId <= 0)
                return BadRequest("Invalid user data.");

            var updatedUser = _userRepo.UpdateUser(user);
            if (updatedUser == null)
                return NotFound($"No user found with ID {user.UserId}.");
            return Ok(updatedUser);
        }

        [HttpDelete("DeleteUser/{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            var deletedUser = _userRepo.DeleteUser(id);
            if (deletedUser == null)
                return NotFound($"No user found with ID {id}.");
            return Ok(deletedUser);
        }

        [HttpPost("AuthenticateUser")]
        public IActionResult AuthenticateUser([FromBody] User loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
                return BadRequest("Username and password are required.");

            var authenticatedUser = _userRepo.AuthenticateUser(loginRequest.Username, loginRequest.Password);
            if (authenticatedUser == null)
                return Unauthorized("Invalid username or password.");

            return Ok(authenticatedUser);
        }
    }
}
