using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saveetha_Demo_Project.Data;
using Saveetha_Demo_Project.Model;

namespace Saveetha_Demo_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext appDb;

        public StudentController(AppDbContext dbContext)
        {
            appDb = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var std = await appDb.students.ToListAsync();

            return Ok(std);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetStudentsByID(int id)
        {
            var stud = await appDb.students.FindAsync(id);
            
            if(stud == null)
            {
                return NotFound($"Students not found on {id}");
            }

            return Ok(stud);



        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student stds)
        {
            appDb.students.Add(stds);

            await appDb.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentsByID), new { id = stds.id }, stds);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Updatestudent(int id, Student studs)
        {
            var idexists = await appDb.students.FindAsync(id);

            if (idexists == null)
                return NotFound($"ID : {id} not available in database");

            idexists.Name = studs.Name;
            idexists.email = studs.email;
            idexists.email = studs.dept;
            idexists.marks = studs.marks;

            await appDb.SaveChangesAsync();

            return Ok(idexists);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletesstudent(int id)
        {
            var deleteid = await appDb.students.FindAsync(id);
            if (deleteid == null)
                return NotFound($"ID : {id} not available in database");

            appDb.students.Remove(deleteid);
            await appDb.SaveChangesAsync();

            return Ok($"ID : {id}  data has been deleted");

        }

    }
}
