using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Data_base;

namespace WebApi.Controllers
{
    /*
     * Clase que controla los POST y los GET de la entidad cita
     */
    [ApiController]
    [Route("[controller]")]
    public class QuoteController
    {
        private JsonManager jsonManager = new JsonManager();

        [HttpGet]
        [Route("requestBills/{id}")]
        public dynamic RequestBills(int id)
        {

            List<Bills> list = jsonManager.GetBills(id);

            return list;
        }

        /*
         * Metodo para guardar una cita
         * c: informacion de la cita
         */
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

        /*
         * Metodo para solicitar infomacion de una cita
         * LicensePlate: Placa del vehiculo
         * Date: fecha de la cita
         * Service: servicio a realizar en la cita
         */
        [HttpGet]
        [Route("requestQuote/{LicensePlate}/{Date}/{Service}")]
        public dynamic RequesteQuote(string LicensePlate, string Date, string Service)
        {
            Date = Date.Replace("2%F", "/");
            Quote Quote = jsonManager.RequestQuote(LicensePlate, Date, Service);

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