using System;
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

        [Route("{id:int}", Name = "GetStudentById")]
        public Student Get(int id)
        {
            return students.FirstOrDefault(s => s.Id == id);
        }

        public IHttpActionResult Post([FromBody] Student student)
        {
            students.Add(student);

            var location = new Uri(Url.Link("GetStudentById", new { id = student.Id }));

            return Created(location,student);
        }


    }
}
