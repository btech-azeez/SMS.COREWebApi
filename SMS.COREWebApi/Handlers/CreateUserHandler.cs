using Mapster;
using MediatR;
using SMS.COREWebApi.Models;
using SMS.COREWebApi.Repository;
using SMS.COREWebApi.Views;

namespace SMS.COREWebApi.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserModel>
    {
        private readonly SMSEntity _context;

        public CreateUserHandler(SMSEntity context)
        {
            _context = context;
        }

        public async Task<UserModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<TblUser>();
            user.CreatedOn = DateTime.Now;
            user.IsDeleted = false;

            _context.TblUsers.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Adapt<UserModel>();
        }
    }
}
