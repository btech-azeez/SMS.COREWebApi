using Microsoft.AspNetCore.Mvc;
using SMS.COREWebApi.Models;
using SMS.COREWebApi.Repo;
using SMS.COREWebApi.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMS.COREWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Retrieves all students.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return Ok(students);
        }

        /// <summary>
        /// Retrieves a student by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            return student != null ? Ok(student) : NotFound();
        }

        /// <summary>
        /// Creates a new student.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] UserModel student)
        {
            if (student == null)
                return BadRequest("Invalid student data.");

            var studentId = await _studentRepository.CreateStudentAsync(student);
            return CreatedAtAction(nameof(GetStudent), new { id = studentId }, student);
        }

        /// <summary>
        /// Updates an existing student.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UserModel student)
        {
            if (id != student.Id)
                return BadRequest("ID mismatch.");

            var success = await _studentRepository.UpdateStudentAsync(student);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Deletes a student.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var success = await _studentRepository.DeleteStudentAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
