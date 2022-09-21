using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Data_base;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    /**
    * Clase que controla los POST y los GET de la entidad repuesto
    */
    public class ReplacementController
    {

        private JsonManager jsonManager = new JsonManager();

        [HttpPost]
        [Route("saveReplacement")]
        /**
         * Metodo para guardar un repuesto
         * p: informacion del repuesto
         */
        public dynamic SaveReplacement(Replacement p)
        {
            bool request = jsonManager.SaveReplacement(p);
            if (request)
            {
                return new
                {
                    success = true,
                    message = "rep saved",
                    result = p
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "rep already saved",
                    result = p
                };
            }
        }

        [HttpGet]
        [Route("getReplacement")]
        /*
        * Metodo para solicitar informacion de un repuesto con la cedula juridica de su proveedor y el nombre del repuesto
        * ProvLegalID: cedula juridica del proveedor del repuesto
        * pname: nombre del repuesto 
        */
        public dynamic GetProvider(string ProvLegalID, string pname)
        {
            Replacement rep = jsonManager.RequestReplacement(pname, ProvLegalID);

            if (rep == null)
            {
                return new
                {
                    success = false,
                    message = "provider not recognized"

                };
            }
            else
            {
                return new
                {
                    success = true,
                    message = rep
                };
            }
        }
    }
}