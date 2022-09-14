using Microsoft.VisualBasic;
using WebApi.Models;

namespace WebApi.Models
{
    public class Office
    {
        public string Name { get; set; }
        public string Manager { get; set; }
        public string StartManagement { get; set; }
        public string StartOperation { get; set; }

    }

    public class Address
    {
        public string OfficeName { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Canton { get; set; }

    }

    public class OfficePhones
    {
        public string OfficeName { get; set; }

        public string Phone { get; set; }
    }
}