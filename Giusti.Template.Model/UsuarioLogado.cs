﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Giusti.Template.Model
{
    public class UsuarioLogado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string WorkstationId { get; set; }
        public DateTime UltimoAcesso { get; set; }
        public DateTime DataHoraAcesso { get; set; }
        public string Token { get; set; }
    }
}
