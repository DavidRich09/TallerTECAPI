using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Data_base;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OfficeController
    {
        private JsonManager jsonManager = new JsonManager();

        [HttpPost]
        [Route("saveOffice")]

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
