using WebApi.Models;

namespace WebApi.Models
{
    public class Quote
    {
        public string Responsible { get; set; }
        public string Assistant { get; set; }
        public string LicensePlate { get; set; }
        public string Service { get; set; }
        public string Client { get; set; }
        public string Office { get; set; }
        public string Date { get; set; }

    }

    public class Replacements
    {
        public string LicensePlate { get; set; }
        public string Replacement { get; set; }

    }

}