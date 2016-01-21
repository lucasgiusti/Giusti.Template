﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giusti.Template.Model.Results
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public List<ServiceResultMessage> Messages { get; set; }

        public ServiceResult()
        {
            Success = true;
            Messages = new List<ServiceResultMessage>();
        }
    }
}
