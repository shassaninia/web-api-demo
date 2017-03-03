using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace WebAPIDemo.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            using (var entities = new EmployeesDBEntities())
            {
                return entities.Employees.ToList();
            }
        }

        public Employee Get(int id)
        {
            using (var entities = new EmployeesDBEntities())
            {
                 return entities.Employees.FirstOrDefault(x => x.ID == id);
            }
        }


        //FromBody tells web api that data for employee will come from the requests body...
        public void Post([FromBody] Employee employee)
        {
            using (var entities = new EmployeesDBEntities())
            {
                entities.Employees.Add(employee);
                entities.SaveChanges();
            }
        }
    }
}
