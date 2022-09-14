using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Data_base;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController
    {
        private JsonManager jsonManager = new JsonManager();

        [HttpPost]
        [Route("saveService")]

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