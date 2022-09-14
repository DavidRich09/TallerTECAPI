using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Data_base;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController
    {
        private JsonManager jsonManager = new JsonManager();

        [HttpPost]
        [Route("saveClient")]

        public dynamic SaveClient(Client c)
        {
            bool request = jsonManager.SaveClient(c);
            if (request)
            {
                return new
                {
                    success = true,
                    message = "client saved",
                    result = c
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "client repeat",
                    result = c
                };
            }
        }

        [HttpGet]
        [Route("requestClient")]
        public dynamic RequesteClient(string id)
        {

            Client client = jsonManager.RequestClient(id);

            if (client == null)
            {
                return new
                {
                    success = false,
                    message = "client not registered"

                };
            }
            else
            {
                return new
                {
                    success = true,
                    message = client
                };
            }
        }
    }
}
