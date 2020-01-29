using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.CRUD.Controllers
{
    public class EmployeeController : ApiController
    {
        ExampleEntities db = new ExampleEntities();

        public HttpResponseMessage Get(string gender = "All", int? top = 0)
        {
            IQueryable<Employee> query = db.Employees;

            gender = gender.ToLower();

            switch (gender)
            {
                case "all":
                    break;
                case "erkek":
                case "kadın":
                    query = query.Where(q => q.Gender.ToLower() == gender);
                    break;
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"{gender} bulunamadı. Lütfen All, Erkek yada Kadın parametrelerini kullanın.");
            }

            if (top > 0)
            {
                query = query.Take(top.Value);
            }

            return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
        }

        public HttpResponseMessage Get(int id)
        {
            Employee emp = db.Employees.FirstOrDefault(x => x.Id == id);

            if (emp == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Id'si {id} olan çalışan bulunamadı.");
            else
                return Request.CreateResponse(HttpStatusCode.OK, emp);
        }

        public HttpResponseMessage Post(Employee emp)
        {
            try
            {
                db.Employees.Add(emp);

                if (db.SaveChanges() > 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, emp);
                    response.Headers.Location = new Uri(Request.RequestUri + "/" + emp.Id);

                    return response;
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Veri ekleme işlemi yapılamadı.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(Employee emp)
        {
            try
            {
                Employee e = db.Employees.FirstOrDefault(x => x.Id == emp.Id);

                if (e == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee Id: {emp.Id}");
                else
                {
                    e.Name = emp.Name;
                    e.Surname = emp.Surname;
                    e.Salary = emp.Salary;
                    e.Gender = emp.Gender;

                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, emp);
                    }
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Güncelleme yapılamadı.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //FromUri, FromBody Örneği -- İki FormBody yapmak için ComplexType kullanmalıyız. 
        public HttpResponseMessage Put([FromUri]int id, [FromBody]Employee emp)
        {
            try
            {
                Employee e = db.Employees.FirstOrDefault(x => x.Id == id);

                if (e == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee Id: {id}");
                else
                {
                    e.Name = emp.Name;
                    e.Surname = emp.Surname;
                    e.Salary = emp.Salary;
                    e.Gender = emp.Gender;

                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, emp);
                    }
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Güncelleme yapılamadı.");
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
                var e = db.Employees.FirstOrDefault(x => x.Id == id);

                if (e == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee Id: {id}");
                else
                {
                    db.Employees.Remove(e);

                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, $"Employee Id: {id}");
                    }
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Kayıt Silinemedi. Employee ID: {id}");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
