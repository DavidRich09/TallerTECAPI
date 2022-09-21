using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Data_base;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    /**
     * Clase que controla los POST y los GET de la entidad servicio
     */
    public class ServiceController
    {
        private JsonManager jsonManager = new JsonManager();

        [HttpPost]
        [Route("saveService")]
        /**
        * Metodo para guardar un servicio 
        * c: informacion del servicio
        */
        public dynamic SaveService(Service c)
        {
            bool request = jsonManager.SaveService(c);
            if (request)
            {
                return new
                {
                    success = true,
                    message = "service saved",
                    result = c
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "service repeat",
                    result = c
                };
            }
        }

        [HttpGet]
        [Route("requestService")]
        /*
       * Metodo para solicitar informacion de un servicio con su nombre
       * nombre: nombre del servicio
       */
        public dynamic RequesteService(string Name)
        {

            Service service = jsonManager.RequestService(Name);

            if (service == null)
            {
                return new
                {
                    success = false,
                    message = "service not registered"

                };
            }
            else
            {
                return new
                {
                    success = true,
                    message = service
                };
            }
        }
    }
}