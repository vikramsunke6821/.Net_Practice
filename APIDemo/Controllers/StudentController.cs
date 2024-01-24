using APIDemo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private ApplicationDBContext _dbContext;
        public StudentController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<StudentModel> GetAllStudents() 
        {
            return _dbContext.StudentTable.ToList();
        }

        [HttpGet ("GetStudentById")]
        public ActionResult<StudentModel> GetStudentDetails(int Id) 
        {
            var studentDetails= _dbContext.StudentTable.FirstOrDefault(x => x.Id == Id);
            return studentDetails;
        }

        [HttpPost]
        public ActionResult<StudentModel> AddStudent([FromBody]StudentModel studentDetails)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dbContext.StudentTable.Add(studentDetails);
            _dbContext.SaveChanges();
            return Ok(studentDetails);
        }


    }
}
