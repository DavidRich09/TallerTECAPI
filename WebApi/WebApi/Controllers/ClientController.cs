using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Data_base;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

using System.IO;
using System.Net;
using System.Net.Mail;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    /**
     * Clase que controla los POST y los GET de la entidad cliente
     */
    public class ClientController
    {
        private JsonManager jsonManager = new JsonManager();
        
        [HttpPost]
        [Route("saveClientClient")]

        /**
         * Metodo para guardar un cliente para la vista cliente
         * c: informacion del cliente
         */
        public dynamic SaveClientClient(Client c)
        {
            bool request = jsonManager.SaveClient(c);
            if (request)
            {
                return new
                {
                    success = true,
                    message = "client saved",
                    result = c

                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "client repeated",
                    result = c
                };
            }
        }

        /*
         * Metodo para guardar un cliente en la vista taller
         * c: informacion del cliente
         */
        [HttpPost]
        [Route("saveClient")]

        public dynamic SaveClient(Client c)
        {
            bool request = jsonManager.SaveClient(c);
            if (request)
            {
                SendEmail(c);
                return new
                {
                    success = true,
                    message = "client saved",
                    result = c

                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "client repeat",
                    result = c
                };
            }
        }

        /*
         * Metodo para solicitar informacion de un cliente con su cedula
         * id: cedula del cliente
         */
        [HttpGet]
        [Route("requestClient/{id}")]
        public dynamic RequesteClient(string id)
        {

            Client client = jsonManager.RequestClient(id);

            if (client == null)
            {
                return new
                {
                    success = false,
                    message = "client not registered"

                };
            }
            else
            {
                return new
                {
                    success = true,
                    message = client
                };
            }
        }
        
        /*
         * Metodo para solicitar infromacion de un cliente con su usuario
         * user: usuario del cliente
         */
        [HttpGet]
        [Route("requestClientbyUser/{user}")]
        public dynamic RequesteClientbyUser(string user)
        {

            Client client = jsonManager.RequestClientbyUser(user);

            if (client == null)
            {
                return new
                {
                    success = false,
                    message = "client not registered"

                };
            }
            else
            {
                return new
                {
                    success = true,
                    message = client
                };
            }
        }

        /*
         * Metodo para enviar la contraseña del cliente por correo electronico
         */
        [HttpGet]
        [Route("SendMail")]
        public dynamic SendMail(Client c)
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("ingrid.vargas.badilla@gmail.com", "Kyocode", System.Text.Encoding.UTF8);//Correo de salida
            correo.To.Add(c.Email); //Correo destino?
            correo.Subject = "Bienvenido a TallerTEC"; //Asunto
            correo.Body = "Su contraseña es " + c.Password ; //Mensaje del correo
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
            smtp.Port = 25; //Puerto de salida
            smtp.Credentials = new System.Net.NetworkCredential("invabadilla@estudiantec.cr", "Ivb2020129621.");//Cuenta de correo
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtp.EnableSsl = true;//True si el servidor de correo permite ssl
            smtp.Send(correo);

            return new
            {
                success = true,
                message = c
            };
        }

        protected void SendEmail(Client c)
        {
            string txtEmail = "ingrid.vargas.badilla@gmail.com";

            using (MailMessage mm = new MailMessage(txtEmail, c.Email))
            {
                mm.Subject = "Creación de nuevo cliente en TallerTEC";
                mm.Body = "Le damos la bienvenida a nuestro taller " + c.Name + ".\n Este es su usuario: " + c.User + "\n Esta es su contraseña: " + c.Password + "\n No compartas el usuario ni la contraseña de tu cuenta";
                
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                NetworkCredential NetworkCred = new NetworkCredential(txtEmail, "mhbznvcsbsvjjrzq");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(mm);
                
            }
        }
        
        /**
         * Metodo para almacenar el usuario ingresado en la aplicacion
         */
        [HttpPost]
        [Route("saveactive")]
        public dynamic SaveActive(string id)
        {
            UserSingleton user = UserSingleton.GetInstance();
            user.id = id;

            return new
            {
                success = true,
                message = "client saved",
            };
        }
        
        /**
         * Metodo para solicitar el usuario activo
         */
        [HttpGet]
        [Route("requestactive")]
        public dynamic RequestActive()
        {
            UserSingleton user = UserSingleton.GetInstance();
            return new
            {
                success = true,
                message = user.id,
            };
        }
    }
}
