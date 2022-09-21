using WebApi.Models;

namespace WebApi.Models
{
    public class Service
    {
        /*
         * Clase Service, estructura de la entidad Servicio
         * •	Name: Nombre del servicio
            •	Cost: Costo del servicio
            •	EstimateDuration: Tiempo estimado para realizar el servicio
            •	Price: Precio a cobrar al cliente
         */
        public string Name { get; set; }
        public string Cost { get; set; }
        public string EstimatedDuration { get; set; }
        public string Price { get; set; }

    }
}