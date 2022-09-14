using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Data_base;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class ReplacementController
    {

        private JsonManager jsonManager = new JsonManager();

        [HttpPost]
        [Route("saveReplacement")]

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

        public dynamic GetProvider(string ProvLegalID, string pname)
        {
            Replacement rep = jsonManager.RequestReplacement(ProvLegalID, pname);

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