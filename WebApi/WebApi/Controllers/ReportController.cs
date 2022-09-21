using Microsoft.AspNetCore.Mvc;
using WebApi.Data_base;
using WebApi.Models;

namespace WebApi.Controllers
{
    /*
     * Clase que controla los reportes 
     */
    [ApiController]
    [Route("[controller]")]
    public class ReportController
    {
        private JsonManager jsonManager = new JsonManager();

        /*
         * Metodo para realizar el reporte de los clientes con mas visitas
         */
        [HttpGet]
        [Route("ClientReport")]
        public dynamic ClientReport()
        {
            string json = ClientReportApp.Program.CreateJsonObject(@"..\..\WebApi\WebApi\Data Base\quote.json");
            ClientReportApp.Program.GeneratePdfReport(json);
            return true;
        }

        /*
         * Metodo para realizar el reporte de los vehiculos con mas visitas
         */
        [HttpGet]
        [Route("LicReport")]
        public dynamic LicReport()
        {
            string json = LicReportApp.Program.CreateJsonObject(@"..\..\WebApi\WebApi\Data Base\quote.json");
            LicReportApp.Program.GeneratePdfReport(json);
            return true;
        }
        /*
         * Metodo para realizar la factura de una cita
         */
        [HttpGet]
        [Route("QuoteReport/{LicensePlate}/{date}/{service}")]
        public dynamic BillReport(string LicensePlate, string date, string service)
        {
            string json = QuoteReportApp.Program.CreateJsonObject(@"..\..\WebApi\WebApi\Data Base\quote.json", LicensePlate, date, service);
            QuoteReportApp.Program.GeneratePdfReport(json);
            return true;
        }
        /*
         * Metodo para realizar el reporte de las sucursales con mas visitas
         */

        [HttpGet]
        [Route("OfiiceReport/{dateStart}/{dateEnd}")]
        public dynamic BillReport(string dateStart, string dateEnd)
        {
            string json = OfficeReportApp.Program.CreateJsonObject(@"..\..\WebApi\WebApi\Data Base\quote.json", dateStart, dateEnd);
            OfficeReportApp.Program.GeneratePdfReport(json);
            return true;
        }



    }
}
