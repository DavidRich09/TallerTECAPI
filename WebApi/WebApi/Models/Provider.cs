namespace WebApi.Models;

public class Provider
{
    /*
     * Clase Provider, estructura de la entidad provider
     * •	LegalID: Número de cédula jurídico del proveedor
        •	Name: Nombre del proveedor
        •	Email: Correo electrónico del proveedor
        •	Contact: Contacto (Número de telefono) del proveedor
        •	Address: Dirección del proveedor  

     */
    public string LegalID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Contact { get; set; }
    public string Address { get; set; }
}