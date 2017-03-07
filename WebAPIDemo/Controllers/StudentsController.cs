using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class StudentsController : ApiController
    {
        static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "Tom" },
            new Student { Id = 2, Name = "Sam" },
            new Student { Id = 3, Name = "John" },
        };

        public IEnumerable<Student> Get()
        {
            return students;
        }

        // = /api/students/1
        public Student Get(int id)
        {
            return students.FirstOrDefault(s => s.Id == id);
        }

        // = /api/students/1/courses
        [Route("api/students/{id}/courses")]
        public IEnumerable<string> GetStudentCourses(int id)
        {
            if (id == 1)
                return new List<string> { "C#", "ASP.NET", "SQL Server" };
            else if (id == 2)
                return new List<string> { "ASP.NET Web API", "C#", "SQL Server" };
            else
                return new List<string> { "Bootstrap", "jQuery", "AngularJs" };
        }
    }
}
