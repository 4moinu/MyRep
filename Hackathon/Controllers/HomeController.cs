using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace Hackathon.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("test1")]
        public IHttpActionResult Index()
        {
            var obj = new MyClass();
            obj.Id = "1";
            obj.Name = "Hello World";
            obj.Value = "123";

            TestDBEntities testDBEntities = new TestDBEntities();
            var objStudent = testDBEntities.Students.FirstOrDefault();

            obj.Id = objStudent.StudentID.ToString();
            obj.Name = objStudent.StudentName;
            obj.Value = objStudent.Age.ToString();
            //string text = "Hello World !!";
            return Ok(obj);
        }


    }

    public class MyClass
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
