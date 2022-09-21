using Microsoft.VisualBasic;
using WebApi.Models;

namespace WebApi.Models
{
    /*
     * Clase Office, estructura de la entidad sucursal
     * �	Name: Nombre de la sucursal
        �	Manager: N�mero de c�dula del administrador
        �	StartManager: Fecha en la que empez� a trabajar el administrador
        �	StartOperation: Fecha en la que la sucursal empez� a funcionar
        �	Address: Direcci�n de la sucursal  
        �	Phone: Tel�fonos de la sucursal
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
     * �	OfficeName: Nombre de la sucursal
        �	Province: Provincia en la que se ubica la sucursal
        �	Canton: Cant�n en el que se ubica la sucursal
        �	District: Distrito en el que se ubica la sucursal

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
     * �	OfficeName: Nombre de la sucursal
	        Phone: Tel�fono de la sucursal  

     */
    public class OfficePhones
    {
        public string OfficeName { get; set; }

        public string Phone { get; set; }
    }
}