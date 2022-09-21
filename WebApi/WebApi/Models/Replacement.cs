namespace WebApi.Models
{
    /*
     * Clase Replacement, estructura de la entidad Repuesto
     * •	name: Nombre del repuesto
        •	price: Precio del repuesto
        •	brand: Marca del repuesto
        •	ProvLegalID: Cédula Jurídica del proveedor 
        •	cost: Costo del repuesto
        •	models: Modelos de vehículos en los cuales se puede utilizar el repuesto
     */
    public class Replacement
    {
        public string name { get; set; }
        public string price { get; set; }
        public string brand { get; set; }
        public string ProvLegalID { get; set; }
        public string cost { get; set; }
        public string[] models { get; set; }
    }
}