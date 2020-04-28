using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AEP_WebApi.Models
{
    public class Comuni
    {
        public int IdComune { get; set; }
        public string Comune { get; set; }
        public string Cap { get; set; }
        public string Provincia { get; set; }
        public string Regione { get; set; }
    }
}
