using System;
using System.Net.Mail;
using System.Net;
using Application.Utilidades.EnvioCorreo.Parameter;
using Application.Utilidades.EnvioCorreo.Interface;
using System.IO;

namespace Application.Utilidades.EnvioCorreo.Services
{
    public class SendEmailsService : ISendEmailsService
    {
        public string SendEmails(ParameterSendEmail parameter, ConfigCredentialsEmail config)
        {
            try
            {
                MailAddress fromAddress = new MailAddress(config.CorreoRemitente);

                SmtpClient smtpClient = new SmtpClient
                {
                    Host = config.Dominio,
                    Port = config.Port,
                    EnableSsl = config.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, config.Password)
                };

                int count = 0;
                foreach (string recipientEmail in parameter.Recipients)
                {

                    MailAddress toAddress = new MailAddress(recipientEmail);
                    MailMessage message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = parameter.Subject,
                        IsBodyHtml = true,
                        Body = parameter.Body
                    };

                    foreach (Adjuntos adjunto in parameter.ListadoAdjuntos)
                    {
                        byte[] fileBytes = Convert.FromBase64String(adjunto.base64);
                        MemoryStream memoryStream = new MemoryStream(fileBytes);

                        Attachment attachment = new Attachment(memoryStream, adjunto.nombreArchivo, adjunto.tipoArchivo);
                        message.Attachments.Add(attachment);
                    }

                    try
                    {
                        smtpClient.Send(message);
                        count++;
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        ex.ToString();
                    }
                }
                //Contar cuantos correos se enviaron
                string result = $"Se enviaron {count} correos exitosamente de {parameter.Recipients.Count}";

                return result;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return ex.ToString();
            }
        }
    }
}
