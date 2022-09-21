using Microsoft.VisualBasic;
using WebApi.Models;

namespace WebApi.Models
{
    /*
     * Clase Office, estructura de la entidad sucursal
     * •	Name: Nombre de la sucursal
        •	Manager: Número de cédula del administrador
        •	StartManager: Fecha en la que empezó a trabajar el administrador
        •	StartOperation: Fecha en la que la sucursal empezó a funcionar
        •	Address: Dirección de la sucursal  
        •	Phone: Teléfonos de la sucursal
     */
    public class Office
    {
        public string Name { get; set; }
        public string Manager { get; set; }
        public string StartManagement { get; set; }
        public string StartOperation { get; set; }
        public Address Address { get; set; }
        public OfficePhones Phone { get; set; }

    }
    /*
     * Clase Address, relacion entre la direccion y la sucursal
     * •	OfficeName: Nombre de la sucursal
        •	Province: Provincia en la que se ubica la sucursal
        •	Canton: Cantón en el que se ubica la sucursal
        •	District: Distrito en el que se ubica la sucursal

     */
    public class Address
    {
        public string OfficeName { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Canton { get; set; }

    }
    /*
     * Clase OfficePhones, relacion entre telefonos y sucursal
     * •	OfficeName: Nombre de la sucursal
	        Phone: Teléfono de la sucursal  

     */
    public class OfficePhones
    {
        public string OfficeName { get; set; }

        public string Phone { get; set; }
    }
}