using System.ComponentModel.DataAnnotations;

namespace SMS.COREWebApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string ?FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
        public string? Gender { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
