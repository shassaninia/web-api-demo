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

        public HttpResponseMessage Get(int id)
        {
            using (var entities = new EmployeesDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(x => x.Id == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id + " not found");
                }
            }
        }


        //FromBody tells web api that data for employee will come from the requests body...
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {


                using (var entities = new EmployeesDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    //return a 201 (item created) in the response
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    //return the location of the the new item
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + employee.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (var entities = new EmployeesDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.Id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Employee with id: {id} not found");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, $"Employee with id: {id} deleted");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
