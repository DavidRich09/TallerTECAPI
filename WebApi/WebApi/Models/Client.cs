using WebApi.Models;

namespace WebApi.Models
{
    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class Adress
    {
        public string ClientId { get; set; }
        public string Nstreet { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Canton { get; set; }

    }

    public class ClientPhones
    {
        public string ClientId { get; set; }

        public string Phone { get; set; }
    }
}

