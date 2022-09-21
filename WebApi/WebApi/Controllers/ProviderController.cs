using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Data_base;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    /**
     * Clase que controla los POST y los GET de la entidad proveedor
     */
    public class ProviderController
    {
        private JsonManager jsonManager = new JsonManager();

        [HttpPost]
        [Route("saveProvider")]
        /*
         * Metodo para guardar un proveedor
         * p: informacion del proveedor
         */
        public dynamic SaveProvider(Provider p)
        {
            bool request = jsonManager.SaveProvider(p);
            if (request)
            {
                return new
                {
                    success = true,
                    message = "provider saved",
                    result = p
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "provider already saved",
                    result = p
                };
            }
        }

        [HttpGet]
        [Route("getProvider")]
        /*
         * Metodo para solicitar informacion de un proveedor con su cedula juridica
         * LegalID: cedula juridica del proveedor
         */
        public dynamic GetProvider(string LegalID)
        {
            Provider provider = jsonManager.RequestProvider(LegalID);
            
            if (provider == null)
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
                    message = provider
                };
            }
        }
    }
}