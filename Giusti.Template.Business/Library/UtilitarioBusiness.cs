using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;
using System.Net.Mail;

namespace Giusti.Template.Business.Library
{
    public class UtilitarioBusiness
    {
        public static void GravaArquivoTexto(string Path, bool Append, string Conteudo)
        {
            StreamWriter writer = new StreamWriter(Path, Append, System.Text.Encoding.UTF8);
            try
            {
                writer.Write(Conteudo);
                writer.Flush();
            }
            finally
            {
                writer.Close();
            }
        }

        public static void GravaLog(string Path, string Mensagem)
        {
            Path += "LOG" + " " + DateTime.Today.Date.ToShortDateString().Replace("/", "-").Replace(" ", "").Replace("00:00:00", "") + ".txt";
            GravaArquivoTexto(Path, true, Mensagem);
        }

        public static string RetornaChaveConfig(string Nome)
        {
            if (ConfigurationManager.AppSettings[Nome] != null)
                return ConfigurationManager.AppSettings[Nome].ToString();
            else
                return null;
        }

        public static string RetornaExceptionMessages(Exception e, string msgs = "")
        {
            if (e == null) return string.Empty;
            if (msgs == "") msgs = e.Message;
            if (e.InnerException != null)
                msgs += "\r\nInnerException: " + RetornaExceptionMessages(e.InnerException);
            return msgs;
        }
    }
}
