using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;
using System.Web.Http.Cors;
using System.Threading;

namespace WebAPIDemo.Controllers
{

    [EnableCorsAttribute("*","*","*")]
    //[RequireHttps]
    public class EmployeesController : ApiController
    {
        //can be applied to action, controller or in webapiconfig.cs class to apply it to whole application
        [BasicAuthentication]
        public IHttpActionResult Get(string gender="All")
        {
            //username is set in the BasicAuthenticationAttribute
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (var entities = new EmployeesDBEntities())
            {
                switch (username.ToLower())
                {
                    case "male":
                        return Ok(entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Ok(entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());                      
                    default:
                        return BadRequest("Not male or female");
                       
                }
                
            }
        }

        
        public IHttpActionResult Get(int id)
        {
            using (var entities = new EmployeesDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(x => x.Id == id);

                if (entity != null)
                {
                    return Ok(entity);
                }
                else
                {
                    return BadRequest($"Did not find an employee with an id of {id}");
                }
            }
        }


        //FromBody tells web api that data for employee will come from the requests body...
        public IHttpActionResult Post([FromBody] Employee employee)
        {
            try
            {
                using (var entities = new EmployeesDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    return Created(new Uri(Request.RequestUri + "/" + employee.Id.ToString()), employee);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (var entities = new EmployeesDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.Id == id);

                    if (entity == null)
                    {
                        return BadRequest($"Employee with and id of {id} not found");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();

                        return Ok($"Employee with and id of {id} was deleted");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        public IHttpActionResult Put(int id, Employee employee)
        {
            using (var entities = new EmployeesDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.Id == id);

                if(entity == null)
                {
                    return BadRequest($"Employee with an Id of {id} was not found");
                }
                else
                {
                    entity.FirstName = employee.FirstName;
                    entity.LastName = employee.LastName;
                    entity.Gender = employee.Gender;
                    entity.Salary = employee.Salary;

                    entities.SaveChanges();

                    return Ok(entity);
                }
            }
        }
    }
}
