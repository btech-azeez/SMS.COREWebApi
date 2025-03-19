using MediatR;
using SMS.COREWebApi.Repository;
using SMS.COREWebApi.Views;

namespace SMS.COREWebApi.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly SMSEntity _context;

        public DeleteUserHandler(SMSEntity context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.TblUsers.FindAsync(request.Id);
            if (user == null) return false;

            _context.TblUsers.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
