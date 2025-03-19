using Mapster;
using MediatR;
using SMS.COREWebApi.Models;
using SMS.COREWebApi.Repository;
using SMS.COREWebApi.Views;
using Microsoft.EntityFrameworkCore;

namespace SMS.COREWebApi.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserModel>>
    {
        private readonly SMSEntity _context;

        public GetAllUsersHandler(SMSEntity context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.TblUsers
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName ?? null,  // ✅ Ensure NULL values are handled
                    LastName = u.LastName ?? null,
                    Email = u.Email ?? null,
                    Password = u.Password ?? null,
                    UserName = u.UserName ?? null,
                    Gender = u.Gender ?? null,
                    CreatedOn = u.CreatedOn,
                    UpdatedOn = u.UpdatedOn,
                    IsDeleted = u.IsDeleted ?? false
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return users;
        }

    }
}
