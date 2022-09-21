using WebApi.Models;

namespace WebApi.Models
{

    /**
     * Clase Client que define la estructura de la entidad Cliente
     * Id: Número de cédula del cliente
     * Name: Nombre completo del cliente
     * User: Usuario para inicio de sesión del cliente
     * Email: Correo electrónico del cliente
     * Password: Contraseña para inicio de sesión del cliente
     * Address: Direcciones del cliente
     * Phone: Teléfonos del cliente 
     */
    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<ClientAddress> Address { get; set; }
        public List<ClientPhones> Phone { get; set; }

    }

    /**
     * Clase que define la estrucutura de la entidad direccion del cliente
     * ClientId: Número de cédula del cliente
     * Province: Provincia de la dirección del cliente
     * Canton: Cantón de la dirección del cliente
     * District: Distrito de la dirección del cliente
     * Nstreet: Número de calle de la dirección del cliente 
     */
    public class ClientAddress
    {
        public string ClientId { get; set; }
        public string Nstreet { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Canton { get; set; }

    }

    /**
     * Clase que define la estrucutura de la entidad telefono del cliente
     * ClientId: Número de cédula del cliente
     * Phone: Teléfono del cliente 
     */
    public class ClientPhones
    {
        public string ClientId { get; set; }

        public string Phone { get; set; }
    }
}

