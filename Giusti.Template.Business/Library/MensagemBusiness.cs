using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Giusti.Template.Business.Library;
using System.Resources;
using System.Reflection;

namespace Giusti.Template.Business
{
    public static class MensagemBusiness
    {
        public static string RetornaMensagens(string CodMensagem)
        {
            ResourceManager Mensagem = new ResourceManager("Giusti.Template.Business.Resource.Mensagem", Assembly.GetExecutingAssembly());
            return Mensagem.GetString(CodMensagem, System.Threading.Thread.CurrentThread.CurrentUICulture);
        }

        public static string RetornaMensagens(string CodMensagem, string[] Complementos)
        {
            return String.Format(RetornaMensagens(CodMensagem), Complementos);
        }
    }
}