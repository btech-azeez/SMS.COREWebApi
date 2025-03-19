using MediatR;
using SMS.COREWebApi.Models;

namespace SMS.COREWebApi.Views
{
    public record CreateUserCommand(string FirstName, string LastName, string Email, string Password, string Gender) : IRequest<UserModel>;
}
