using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using SMS.COREWebApi.Views;

namespace SMS.COREWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateUserCommand> _createUserValidator;
        private readonly IValidator<UpdateUserCommand> _updateUserValidator;

        public UserController(IMediator mediator, IValidator<CreateUserCommand> createUserValidator, IValidator<UpdateUserCommand> updateUserValidator)
        {
            _mediator = mediator;
            _createUserValidator = createUserValidator;
            _updateUserValidator = updateUserValidator;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return user != null ? Ok(user) : NotFound();
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var validationResult = await _createUserValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch.");

            var validationResult = await _updateUserValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var success = await _mediator.Send(command);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _mediator.Send(new DeleteUserCommand(id));
            return success ? NoContent() : NotFound();
        }
    }
}
