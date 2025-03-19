using MediatR;
using SMS.COREWebApi.Models;

namespace SMS.COREWebApi.Views
{
    public record GetUserByIdQuery(int Id) : IRequest<UserModel>;
}
