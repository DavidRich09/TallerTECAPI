using WebApi.Models;

namespace WebApi.Models
{
    /**
     * Clase Quote es la estrucutra de la entidad cita
     * Responsible: Número de cédula del trabajador responsable de la cita
     * Assistant: Número de cédula del trabajador asistente de la cita
     * LicensePlate:  Placa del vehículo al cual se le reserva la cita
     * Service: Servicio a realizar en la cita
     * Client: Número de cédula del cliente
     * Office: Sucursal en la cual se realizará la cita
     * Date: Fecha en la cual la cita es asignada
     * Replacements: Repuestos necesarios para la cita 
     */
    public class Quote
    {
        public string Responsible { get; set; }
        public string Assistant { get; set; }
        public string LicensePlate { get; set; }
        public string Service { get; set; }
        public string Client { get; set; }
        public string Office { get; set; }
        public string Date { get; set; }
        public List<Replacements> Replacements{ get; set; }

    }

    /**
     * Clase Replacement para definir la estructura de la entidad repuestos
     * LicensePlate: Placa del carro al cual se le asignan los repuestos y la cita
     * Replacement: Repuesto necesario para la cita 
     */
    public class Replacements
    {
        public string LicensePlate { get; set; }
        public string Replacement { get; set; }

    }

}