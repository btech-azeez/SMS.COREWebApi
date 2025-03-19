using SMS.COREWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMS.COREWebApi.Repo
{
    public interface IStudentRepository
    {
        Task<IEnumerable<UserModel>> GetAllStudentsAsync();
        Task<UserModel> GetStudentByIdAsync(int id);
        Task<int> CreateStudentAsync(UserModel student);
        Task<bool> UpdateStudentAsync(UserModel student);
        Task<bool> DeleteStudentAsync(int id);
    }
}
