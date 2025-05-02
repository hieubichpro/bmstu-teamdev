using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.BL;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/v1/users/")]
    public class UserController : Controller
    {
        private readonly UserService userService;
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Retrieves all users
        /// </summary>
        /// <returns>
        /// A list of all users in the system
        /// </returns>
        /// <response code="200">Returns the list of users</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If there was a server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getAllUsers()
        {
            var users = userService.getAllUsers();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a specific user by unique id
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>
        /// The requested user details
        /// </returns>
        /// <response code="200">Returns the requested user</response>
        /// <response code="404">If the user is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult getUser(int id)
        {
            try
            {
                User user = userService.GetUser(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Logs out the current user
        /// </summary>
        /// <returns>
        /// Confirmation of successful logout
        /// </returns>
        /// <response code="200">User successfully logged out</response>
        /// <response code="500">If there was a server error during logout</response>
        [HttpGet("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult logout()
        {
            return Ok();
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="user">User data to register</param>
        /// <returns>
        /// The newly created user
        /// </returns>
        /// <response code="201">User successfully created</response>
        /// <response code="409">If user already exists or data conflict occurs</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddUser([FromBody] User user)
        {
            try
            {
                userService.AddUser(user);
                return StatusCode(StatusCodes.Status201Created, user);
            }
            catch
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
        }

        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <param name="username">User's username</param>
        /// <param name="password">User's password</param>
        /// <returns>
        /// Authenticated user details
        /// </returns>
        /// <response code="200">Returns authenticated user details</response>
        /// <response code="404">If authentication fails (invalid credentials)</response>
        [HttpGet("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult login(string username, string password)
        {
            try
            {
                User user = userService.LogIn(username, password);
                return Ok(user);
            }
            catch
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Updates all user information
        /// </summary>
        /// <param name="id">User id to update</param>
        /// <param name="user">Complete user data for update</param>
        /// <returns>
        /// Confirmation of successful update
        /// </returns>
        /// <response code="200">User successfully updated</response>
        /// <response code="404">If user is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult updateUser(int id, [FromBody] User user)
        {
            try
            {
                userService.UpdateUser(id, user);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Changes user's password
        /// </summary>
        /// <param name="username">User's username</param>
        /// <param name="newpassword">New password to set</param>
        /// <returns>
        /// Updated user details
        /// </returns>
        /// <response code="200">Returns user details with updated password</response>
        /// <response code="404">If user is not found</response>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult changePassword(string username, string newpassword)
        {
            try
            {
                User user = userService.ChangePassword(username, newpassword);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">User id to delete</param>
        /// <returns>
        /// Confirmation of successful deletion
        /// </returns>
        /// <response code="200">User successfully deleted</response>
        /// <response code="404">If user is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DelUser(int id)
        {
            try
            {
                userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}