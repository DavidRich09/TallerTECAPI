﻿using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Data_base;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {

        private JsonManager jsonManager = new JsonManager();

        [HttpGet]
        [Route("requestWorker")]
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
