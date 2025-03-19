using Mapster;
using MediatR;
using SMS.COREWebApi.Models;
using SMS.COREWebApi.Repository;
using SMS.COREWebApi.Views;
using Microsoft.EntityFrameworkCore;

namespace SMS.COREWebApi.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserModel?>
    {
        private readonly SMSEntity _context;

        public GetUserByIdHandler(SMSEntity context)
        {
            _context = context;
        }

        public async Task<UserModel?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.TblUsers
                .AsNoTracking() // ✅ Improve performance for read-only queries
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken); // ✅ Use FirstOrDefaultAsync instead of FindAsync

            return user?.Adapt<UserModel>(); // ✅ Avoid returning null explicitly
        }
    }
}
