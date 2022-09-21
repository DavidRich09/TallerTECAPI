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
using System.Globalization;

namespace OfficeReportApp
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

        public string totalSells { get; set; }
        public string office { get; set; }


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
            string templatePath = @"..\..\WebApi\WebApi\Reports\officeReport.docx";

            DocumentCore dc = DocumentCore.Load(templatePath);

            // To be able to mail merge from your own data source, it must be wrapped into an object that implements the IMailMergeDataSource interface.
            CustomMailMergeDataSource customDataSource = new CustomMailMergeDataSource(appointments);

            // Execute the mail merge.
            dc.MailMerge.Execute(customDataSource);

            string resultPath = "InformeVentas.pdf";

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
        public static string CreateJsonObject(string path, string dateStart, string dateEnd)
        {
            string content;

            dateStart = dateStart.Replace("%2F", "/");
            dateEnd = dateEnd.Replace("%2F", "/");

            string[] validformats = new[] { "dd/MM/yyyy", "dd-MM-yyyy","dd:MM:yyyy","d/M/yyyy", "d-M-yyyy","d:M:yyyy" };
            
            DateTime timeStart = DateTime.ParseExact(dateStart, validformats, CultureInfo.InvariantCulture);
            DateTime timeEnd = DateTime.ParseExact(dateEnd, validformats, CultureInfo.InvariantCulture);

            using (var reader = new StreamReader(path))
            {
                content = reader.ReadToEnd();
            }

            List<Quote> jsonData = JsonConvert.DeserializeObject<List<Quote>>(content);

            Bills nuevo = new Bills()
                { totalSells = "NULL", office = "NULL"};
            appointments.Clear();

            int totalCartago = 0;
            int totalOreamuno = 0;
            int totalCurridabat = 0;
            int totalSantaAna = 0;
            int totalPanama = 0;
            
            string quoteDate = "";

            int i = 0;
            while (i < jsonData.Count)
            {

                DateTime timeQoute = DateTime.ParseExact(jsonData[i].Date, validformats, CultureInfo.InvariantCulture);
                
                if (timeQoute >= timeStart && timeQoute <= timeEnd)
                {

                    if (jsonData[i].Office == "Sucursal Cartago Centro")
                    {
                        totalCartago++;
                    }
                    else if (jsonData[i].Office == "Oreamuno de Cartago")
                    {
                        totalOreamuno++;
                    }
                    else if (jsonData[i].Office == "Pinares Curridabat")
                    {
                        totalCurridabat++;
                    }
                    else if (jsonData[i].Office == "Rio Oro Santa Ana")
                    {
                        totalSantaAna++;
                    }
                    else if (jsonData[i].Office == "Ciudad de Panamá")
                    {
                        totalPanama++;
                    }
                }
                
                i++;

            }

            nuevo = new Bills()
            {
                totalSells = totalCartago.ToString(),
                office = "Sucursal Cartago Centro"
            };
            appointments.Add(nuevo);
            nuevo = new Bills()
            {
                totalSells = totalOreamuno.ToString(),
                office = "Oreamuno de Cartago"
            };
            appointments.Add(nuevo);
            nuevo = new Bills()
            {
                totalSells = totalCurridabat.ToString(),
                office = "Pinares Curridabat"
            };
            appointments.Add(nuevo);
            nuevo = new Bills()
            {
                totalSells = totalSantaAna.ToString(),
                office = "Rio Oro Santa Ana"
            };
            appointments.Add(nuevo);
            nuevo = new Bills()
            {
                totalSells = totalPanama.ToString(),
                office = "Ciudad de Panamá"
            };
            appointments.Add(nuevo);
            
            List<Bills> sorted = appointments.OrderBy(x => x.totalSells).ToList();

            sorted.Reverse();

            string json = String.Empty;
            // Make serialization to JSON format.
            JsonSerializerOptions options = new() { IncludeFields = true };
            json = JsonSerializer.Serialize(sorted, options);
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
                        value = _appointments[_recordIndex].office;
                        return true;
                        /*
                    case "Description":
                        value = _appointments[_recordIndex].service;
                        return true;
                    case "PictUrl":
                        value = _appointments[_recordIndex].mecanic;
                        return true;
                    case "WeightFrom":
                        value = _appointments[_recordIndex].date;
                        return true;
                        */
                    case "WeightTo":
                        value = _appointments[_recordIndex].totalSells;
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
