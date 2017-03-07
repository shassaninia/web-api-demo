using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "Tom" },
            new Student { Id = 2, Name = "Sam" },
            new Student { Id = 3, Name = "John" },
        };

        [Route("")]
        public IEnumerable<Student> Get()
        {
            return students;
        }

        // = /api/students/1
        [Route("{id:int}")]
        public Student Get(int id)
        {
            return students.FirstOrDefault(s => s.Id == id);
        }

        [Route("{name:alpha}")]
        public Student Get(string name)
        {
            return students.FirstOrDefault(s => s.Name.ToLower() == name.ToLower());
        }

        // = /api/students/1/courses
        [Route("{id}/courses")]
        public IEnumerable<string> GetStudentCourses(int id)
        {
            if (id == 1)
                return new List<string> { "C#", "ASP.NET", "SQL Server" };
            else if (id == 2)
                return new List<string> { "ASP.NET Web API", "C#", "SQL Server" };
            else
                return new List<string> { "Bootstrap", "jQuery", "AngularJs" };
        }

        //[Route("api/teachers")] - This will require you to put in /api/students/api/teachers because of the route prefix. To avoid this, use a Tilda symbol:
        [Route("~/api/teachers")]
        public IEnumerable<Teacher> GetTeachers()
        {
            List<Teacher> teachers = new List<Teacher>
            {
                new Teacher { Id = 1, Name = "Rob"},
                new Teacher { Id = 2, Name = "Mike" },
                new Teacher { Id = 3, Name = "Mary" }
            };

            return teachers;
        }
    }
}
