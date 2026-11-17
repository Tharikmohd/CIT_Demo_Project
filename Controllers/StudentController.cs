using CIt_Demo_Project.Data;
using CIt_Demo_Project.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CIt_Demo_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _appdb;

        public StudentController(AppDbContext dbContext)
        {
            _appdb = dbContext;

        }

        [HttpGet]
        public async Task<IActionResult> GetallStudent()
        {
            var std = await _appdb.students.ToListAsync();

            return Ok(std);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentbyId(int id)
        {
            var idstd = await _appdb.students.FindAsync(id);

            if(idstd == null)
            {
                return NotFound("ID not found in Database");
            }

            return Ok(idstd);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student std)
        {
            _appdb.students.Add(std);

            await _appdb.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentbyId),
                new { id = std.id }, std);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Updatestudent(int id, Student stud)
        {
            var idexist = await _appdb.students.FindAsync(id);
            if (idexist == null)
                return NotFound($"ID : {id} not available for update");

            idexist.name = stud.name;
            idexist.email = stud.email;
            idexist.dept = stud.dept;
            idexist.marks = stud.marks;

            return Ok(idexist);

        }


    }
}
