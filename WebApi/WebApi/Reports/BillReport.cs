using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json;
using SautinSoft.Document;
using SautinSoft.Document.Drawing;
using SautinSoft.Document.MailMerging;
using WebApi.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace QuoteReportApp
{
    /**
     * Estructura de la cita almacenada en la base de datos
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
        public List<Replacements> Replacements { get; set; }

    }

    public class Replacements
    {
        public string LicensePlate { get; set; }
        public string Replacement { get; set; }

    }
    /**
     * Estructura del reporte
     */
    public class Bills
    {

        public string service { get; set; }
        public string cost { get; set; }
        public string mecanic { get; set; }
        public string date { get; set; }

        public string licensePlate { get; set; }


    }

    class Program
    {
        /**
         * Metodo para generar el pdf en base a un json
         */
        public static void GeneratePdfReport(string json)
        {
            // Get data from json.
            JsonSerializerOptions options = new() { IncludeFields = true };
            var appointments = JsonSerializer.Deserialize<List<Bills>>(json, options);

            // Load the template document.
            string templatePath = @"..\..\WebApi\WebApi\Reports\billtemp.docx";

            DocumentCore dc = DocumentCore.Load(templatePath);

            // To be able to mail merge from your own data source, it must be wrapped into an object that implements the IMailMergeDataSource interface.
            CustomMailMergeDataSource customDataSource = new CustomMailMergeDataSource(appointments);

            // Execute the mail merge.
            dc.MailMerge.Execute(customDataSource);

            string resultPath = "Factura.pdf";

            // Save the output to file
            PdfSaveOptions so = new PdfSaveOptions()
            {
                Compliance = PdfCompliance.PDF_A1a
            };

            dc.Save(resultPath, so);

            // Open the result for demonstration purposes.
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(resultPath)
                { UseShellExecute = true });
        }


        static List<Bills> appointments = new List<Bills>();
        /**
       * Metodo para tomar la informacion de las citas y generar la infomacion del reporte
       */
        public static string CreateJsonObject(string path, string licensePlate, string date, string service)
        {
            string content;

            date = date.Replace("%2F", "/");

            using (var reader = new StreamReader(path))
            {
                content = reader.ReadToEnd();
            }

            List<Quote> jsonData = JsonConvert.DeserializeObject<List<Quote>>(content);

            Bills nuevo = new Bills()
                { licensePlate = "NULL", cost = "NULL", service = "NULL", mecanic = "NULL", date = "NULL" };
            appointments.Clear();

            int i = 0;
            while (i < jsonData.Count)
            {
        

                nuevo = new Bills()
                {
                    licensePlate = jsonData[i].LicensePlate,
                    cost = jsonData[i].Office,
                    service = jsonData[i].Service,
                    date = jsonData[i].Date,
                    mecanic = jsonData[i].Responsible
                };
                
                if (licensePlate == jsonData[i].LicensePlate && service == jsonData[i].Service && date == jsonData[i].Date)
                {
                    if (service == "Cambio de aceite")
                    {
                        nuevo.cost = "90000";
                    } else if (service == "Cambio de llantas"){
                        nuevo.cost = "75000";
                    } else if (service == "Alineado y tramado")
                    {
                        nuevo.cost = "120000";
                    } else if (service == "Revision 5k km")
                    {
                        nuevo.cost = "100000";
                    } else if (service == "Revision 10k km")
                    {
                        nuevo.cost = "150000";
                    } else if (service == "Revision 15k km")
                    {
                        nuevo.cost = "200000";
                    } else
                    {
                        nuevo.cost = "250000";
                    }

                    Console.WriteLine("SIGUERA");
                    appointments.Add(nuevo);
                }

                i++;
               
            }

            if (appointments.Count == 0)
            {
                nuevo = new Bills()
                {
                    licensePlate = "No encontrado",
                    cost = "No encontrado",
                    service = "No encontrado",
                    date = "No encontrado",
                    mecanic = "No encontrado"
                };
                appointments.Add(nuevo);
            }
            

            string json = String.Empty;
            // Make serialization to JSON format.
            JsonSerializerOptions options = new() { IncludeFields = true };
            json = JsonSerializer.Serialize(appointments, options);
            return json;
        }

        /// <summary>
        /// A custom mail merge data source that allows SautinSoft.Document to retrieve data from CatBeeds objects.
        /// </summary>
        public class CustomMailMergeDataSource : IMailMergeDataSource
        {
            private readonly List<Bills> _appointments;
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
                        value = _appointments[_recordIndex].licensePlate;
                        return true;
                    case "Description":
                        value = _appointments[_recordIndex].service;
                        return true;
                    case "PictUrl":
                        value = _appointments[_recordIndex].mecanic;
                        return true;
                    case "WeightFrom":
                        value = _appointments[_recordIndex].date;
                        return true;
                    case "WeightTo":
                        value = _appointments[_recordIndex].cost;
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

            public CustomMailMergeDataSource(List<Bills> appointments)
            {
                _appointments = appointments;
                // When the data source is initialized, it must be positioned before the first record.
                _recordIndex = -1;
            }
        }
    }
}
