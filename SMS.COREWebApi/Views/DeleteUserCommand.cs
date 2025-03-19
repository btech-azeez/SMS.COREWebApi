using MediatR;

namespace SMS.COREWebApi.Views
{
    public record DeleteUserCommand(int Id) : IRequest<bool>;
}
