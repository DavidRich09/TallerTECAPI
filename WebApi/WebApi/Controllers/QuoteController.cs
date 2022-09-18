using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Data_base;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuoteController
    {
        private JsonManager jsonManager = new JsonManager();

        [HttpGet]
        [Route("requestService")]
        public dynamic RequestBills(int id)
        {

            List<Bills> list = jsonManager.GetBills(id);

            return list;
        }

        [HttpPost]
        [Route("saveQuote")]

        public dynamic SaveQuote(Quote c)
        {
            bool request = jsonManager.SaveQuote(c);
            if (request)
            {
                return new
                {
                    success = true,
                    message = "quote saved",
                    result = c
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "quote repeat",
                    result = c
                };
            }
        }

        [HttpGet]
        [Route("requestQuote")]
        public dynamic RequesteQuote(string LicensePlate, string Date)
        {

            Quote Quote = jsonManager.RequestQuote(LicensePlate, Date);

            if (Quote == null)
            {
                return new
                {
                    success = false,
                    message = "quote not registered"

                };
            }
            else
            {
                return new
                {
                    success = true,
                    message = Quote
                };
            }
        }

    }
}