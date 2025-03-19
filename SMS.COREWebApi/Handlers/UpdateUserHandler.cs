using MediatR;
using SMS.COREWebApi.Repository;
using SMS.COREWebApi.Views;

namespace SMS.COREWebApi.Handlers
{

    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly SMSEntity _context;

        public UpdateUserHandler(SMSEntity context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.TblUsers.FindAsync(request.Id);
            if (user == null) return false;

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Password = request.Password;
            user.Gender = request.Gender;
            user.UpdatedOn = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
