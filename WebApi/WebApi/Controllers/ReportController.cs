using Microsoft.AspNetCore.Mvc;
using WebApi.Data_base;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController
    {
        private JsonManager jsonManager = new JsonManager();

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

        [HttpGet]
        [Route("QuoteReport/{LicensePlate}/{date}/{service}")]
        public dynamic BillReport(string LicensePlate, string date, string service)
        {
            string json = QuoteReportApp.Program.CreateJsonObject(@"..\..\WebApi\WebApi\Data Base\quote.json", LicensePlate, date, service);
            QuoteReportApp.Program.GeneratePdfReport(json);
            return true;
        }
        

    }
}
