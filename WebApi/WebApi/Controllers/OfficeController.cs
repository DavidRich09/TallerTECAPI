using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Data_base;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    /**
     * Clase que controla los POST y los GET de la entidad sucursal
     */
    public class OfficeController
    {
        private JsonManager jsonManager = new JsonManager();

        [HttpPost]
        [Route("saveOffice")]
        /**
         * Metodo para guardar una sucursal
         * c: informacion del sucursal
         */
        public dynamic SaveOffice(Office c)
        {
            bool request = jsonManager.SaveOffice(c);
            if (request)
            {
                return new
                {
                    success = true,
                    message = "office saved",
                    result = c
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "office repeat",
                    result = c
                };
            }
        }

        [HttpGet]
        [Route("requestOffice")]
        /*
     * Metodo para solicitar informacion de una sucursal con su nombre
     * name: nombre de la sucursal
     */
        public dynamic RequesteOffice(string Name)
        {

            Office office = jsonManager.RequestOffice(Name);

            if (office == null)
            {
                return new
                {
                    success = false,
                    message = "office not registered"

                };
            }
            else
            {
                return new
                {
                    success = true,
                    message = office
                };
            }
        }
    }
}
