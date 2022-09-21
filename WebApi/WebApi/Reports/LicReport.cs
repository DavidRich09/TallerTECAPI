using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json;
using SautinSoft.Document;
using SautinSoft.Document.Drawing;
using SautinSoft.Document.MailMerging;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace LicReportApp
{
    
    /**
     * Estrucutra de la cita almacenada en la base de datos
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

    }
    
    /**
     * Estrucutra del reporte
     */
    class Appointment
    {
        public string LicencePlate { get; set; }
        public string Service { get; set; }
        public string Client { get; set; }
        public string Office { get; set; }
        public int Visitas { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Get json data
            string json = CreateJsonObject("asdf");

            // 2. Show json to Console.
            Console.WriteLine(json);

            // 3. Generate report based on .docx template and json.
            GeneratePdfReport(json);
        }
        
        
        /**
         * Metodo para generar el pdf en base a un json
         */
        public static void GeneratePdfReport(string json)
        {
            // Get data from json.
            JsonSerializerOptions options = new() { IncludeFields = true };
            var appointments = JsonSerializer.Deserialize<List<Appointment>>(json, options);

            // Load the template document.
            string templatePath = @"..\..\WebApi\WebApi\Reports\template.docx";

            DocumentCore dc = DocumentCore.Load(templatePath);

            // To be able to mail merge from your own data source, it must be wrapped into an object that implements the IMailMergeDataSource interface.
            CustomMailMergeDataSource customDataSource = new CustomMailMergeDataSource(appointments);

            // Execute the mail merge.
            dc.MailMerge.Execute(customDataSource);

            string resultPath = "HistorialCitas.pdf";

            // Save the output to file
            PdfSaveOptions so = new PdfSaveOptions()
            {
                Compliance = PdfCompliance.PDF_A1a
            };

            dc.Save(resultPath, so);

            // Open the result for demonstration purposes.
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(resultPath) { UseShellExecute = true });
        }

        
        static List<Appointment> appointments = new List<Appointment>();
        
        /**
         * Metodo para tomar la informacion de las citas y generar la infomacion del reporte
         */
        public static string CreateJsonObject(string path)
        {
            string content;

            using(var reader = new StreamReader(path))
            {
                content = reader.ReadToEnd();
            }
            
            List<Quote> jsonData = JsonConvert.DeserializeObject<List<Quote>>(content);
            
            Console.WriteLine(jsonData);

            Appointment nuevo = new Appointment() {LicencePlate = "NULL", Service = "NULL", Client = "NULL", Office = "NULL", Visitas = 0};
            appointments.Clear();

            int i = 0;
            while (jsonData.Count > 0)
            {
                i = 0;
                string plate = jsonData[i].LicensePlate;
                string services = jsonData[i].Service;
                string clients = jsonData[i].Client;
                string offices = jsonData[i].Office;
                i += 1;
                for (int j = 1; j < jsonData.Count; j++)
                {
                    if (plate == jsonData[j].LicensePlate)
                    {
                        services += "\n" + jsonData[j].Service;
                        clients += "\n" + jsonData[j].Client;
                        offices += "\n" + jsonData[j].Office;
                        jsonData.RemoveAt(j);
                        j -= 1;
                        i += 1;
                    }
                }

                nuevo = new Appointment()
                {
                    LicencePlate = plate,
                    Service = services,
                    Client = clients,
                    Office = offices,
                    Visitas = i
                };

                appointments.Add(nuevo);
                
                jsonData.RemoveAt(0);

            }
            
            List<Appointment> sorted = appointments.OrderBy(x => x.Visitas).ToList();

            sorted.Reverse();
            
            string json = String.Empty;
            // Make serialization to JSON format.
            JsonSerializerOptions options = new() {IncludeFields = true };
            json = JsonSerializer.Serialize(sorted, options);
            return json;
        }

        /// <summary>
        /// A custom mail merge data source that allows SautinSoft.Document to retrieve data from CatBeeds objects.
        /// </summary>
        public class CustomMailMergeDataSource : IMailMergeDataSource
        {
            private readonly List<Appointment> _appointments;
            private int _recordIndex;

            /// <summary>
            /// The name of the data source.
            /// </summary>
            public string Name
            {
                get { return "CatBreed"; }
            }

            /// <summary>
            /// SautinSoft.Document calls this method to get a value for every data field.
            /// </summary>
            public bool TryGetValue(string valueName, out object value)
            {
                switch (valueName)
                {
                    case "Title":
                        value = _appointments[_recordIndex].LicencePlate;
                        return true;
                    case "Description":
                        value = _appointments[_recordIndex].Service;
                        return true;
                    case "PictUrl":
                        value = _appointments[_recordIndex].Client;
                        return true;
                    case "WeightFrom":
                        value = _appointments[_recordIndex].Office;
                        return true;
                    case "WeightTo":
                        value = _appointments[_recordIndex].Visitas;
                        return true;
                    default:
                        // A field with this name was not found
                        value = null;
                        return false;
                }
            }

            /// <summary>
            /// A standard implementation for moving to a next record in a collection.
            /// </summary>
            public bool MoveNext()
            {
                return (++_recordIndex < _appointments.Count);
            }

            public IMailMergeDataSource GetChildDataSource(string sourceName)
            {
                return null;
            }
            public CustomMailMergeDataSource(List<Appointment> appointments)
            {
                _appointments = appointments;
                // When the data source is initialized, it must be positioned before the first record.
                _recordIndex = -1;
            }
        }
    }
}