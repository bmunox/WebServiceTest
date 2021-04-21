using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using WebServiceTest.Models;

namespace WebServiceTest.Controllers
{
    public class PersonaController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Add(Models.Request.PersonaRequest model)
        {
            persona oPersona = new persona();
            using (Models.pruebillaEntities db = new Models.pruebillaEntities())
            {
                oPersona.nombre = model.nombre;
                oPersona.edad = model.edad;
                db.persona.Add(oPersona);
                db.SaveChanges();
            }
            return Ok(JsonConvert.SerializeObject(oPersona));
        }
    }
}
