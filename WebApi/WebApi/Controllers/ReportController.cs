using Microsoft.AspNetCore.Mvc;
using WebApi.Data_base;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController
    {

        [HttpGet]
        [Route("ClientReport")]
        public dynamic ClientReport()
        {
            string json = ClientReportApp.Program.CreateJsonObject(@"..\..\WebApi\WebApi\Data Base\quote.json");
            ClientReportApp.Program.GeneratePdfReport(json);
            return true;
        }

        [HttpGet]
        [Route("LicReport")]
        public dynamic LicReport()
        {
            string json = LicReportApp.Program.CreateJsonObject(@"..\..\WebApi\WebApi\Data Base\quote.json");
            LicReportApp.Program.GeneratePdfReport(json);
            return true;
        }

    }
}
