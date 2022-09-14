namespace WebApi.Models
{
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