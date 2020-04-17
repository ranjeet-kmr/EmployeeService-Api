using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {

        // get  method without id
        public IEnumerable<Employee> Get()
        {
                using(var entities = new EmployeeDBEntities())
            {
                return entities.Employees.ToList();
            }
        }

        //get method id
        public HttpResponseMessage Get(int id)
        {
            try
            {

                using (var entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with ID = " + id.ToString() + " Not Found");
                    }
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try 
            {
                using (var entities = new EmployeeDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }

            }
            catch(Exception ex)
            {
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (var entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity != null)
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Employee with ID = " + id + "is removed Sucessfully");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with ID = " + id + "couldn't be found to delete");
                    }
                }

                
            }

            catch(Exception ex)

            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
                
            
        }
    }
}
