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
    public class ApiController : ControllerBase
    {

        private JsonManager jsonManager = new JsonManager();

        [HttpGet]
        [Route("requestWorkerR")]
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
