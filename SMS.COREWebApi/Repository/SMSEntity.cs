using Microsoft.EntityFrameworkCore;
using SMS.COREWebApi.Models;

namespace SMS.COREWebApi.Repository
{
    public class SMSEntity : DbContext
    {
        public SMSEntity(DbContextOptions<SMSEntity> options)
            : base(options)
        {
        }

        public virtual DbSet<TblUser> TblUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
