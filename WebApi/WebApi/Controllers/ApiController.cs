using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Data_base;
using LicReportApp;
using SautinSoft.Document;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    /**
     * Clase que controla los POST y los GET de la entidad trabajador
     */
    public class ApiController : ControllerBase
    {

        private JsonManager jsonManager = new JsonManager();

        [HttpGet]
        [Route("requestWorkerR")]
        /**
         * Metodo para guardar un trabajador aleatorio
         */
        public dynamic RequesteWorkerR()
        {
            Worker worker = jsonManager.RequestWorkerR();
            if (worker == null)
            {
                return new
                {
                    success = false,
                    message = "worker not exist"

                };
            }
            else
            {
                return new
                {
                    success = true,
                    message = worker
                };
            }
        }
        [HttpGet]
        [Route("requestWorker/{id}")]
        /**
         * Metodo para solicitar la informacion de un trabajador con su cedula
         * id: cedula del trabajador
         */
        public dynamic RequesteWorker(int id)
        {

            Worker worker = jsonManager.RequestWorker(id);

            if (worker == null)
            {
                return new
                {
                    success = false,
                    message = "worker not exist"
                    
                };
            }
            else
            {
                return new
                {
                    success = true,
                    message = worker
                };
            }
        }


        [HttpPost]
        [Route("saveWorker")]
        /**
         * Metodo para guardar un trabajador
         * worker: informacion del trabajador
         */
        public dynamic SaveWorker(Worker worker)
        {
            

            bool requestSuccees = jsonManager.SaveWorker (worker);

            if (requestSuccees)
            {
                return new
                {
                    success = true,
                    message = "worker saved",
                    result = worker
                };
            } else
            {
                return new
                {
                    success = false,
                    message = "worker repeat",
                    result = worker
                };
            }

        
        }


    }
}
