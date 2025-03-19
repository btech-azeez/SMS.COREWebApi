using MediatR;

namespace SMS.COREWebApi.Views
{
    public record UpdateUserCommand(int Id, string FirstName, string LastName, string Email, string Password, string Gender) : IRequest<bool>;
}
