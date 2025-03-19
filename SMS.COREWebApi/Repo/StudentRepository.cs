using Dapper;
using SMS.COREWebApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SMS.COREWebApi.Repo
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DapperDbContext _context;

        public StudentRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserModel>> GetAllStudentsAsync()
        {
            using var connection = _context.CreateConnection();
            string sql = "SELECT * FROM TblUser WHERE IsDeleted = 0";
            return await connection.QueryAsync<UserModel>(sql);
        }

        public async Task<UserModel> GetStudentByIdAsync(int id)
        {
            using var connection = _context.CreateConnection();
            string sql = "SELECT * FROM TblUser WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<UserModel>(sql, new { Id = id });
        }

        public async Task<int> CreateStudentAsync(UserModel student)
        {
            using var connection = _context.CreateConnection();
            string sql = @"
                INSERT INTO TblUser (FirstName, LastName, Email, CreatedOn, IsDeleted) 
                VALUES (@FirstName, @LastName, @Email, @CreatedOn, 0);
                SELECT CAST(SCOPE_IDENTITY() as int);";
            return await connection.ExecuteScalarAsync<int>(sql, student);
        }

        public async Task<bool> UpdateStudentAsync(UserModel student)
        {
            using var connection = _context.CreateConnection();
            string sql = @"
                UPDATE TblUser 
                SET FirstName = @FirstName, LastName = @LastName, Email = @Email
                WHERE Id = @Id";
            int rowsAffected = await connection.ExecuteAsync(sql, student);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            using var connection = _context.CreateConnection();
            string sql = "UPDATE TblUser SET IsDeleted = 1 WHERE Id = @Id";
            int rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
