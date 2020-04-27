using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AEP_WebApi.Models
{
    public class Comuni
    {
        public int IdComune { get; set; }
        public string Comune { get; set; }

        [MinLength(5, ErrorMessage="Il cap deve avere 5 cifre")]
        [MaxLength(5, ErrorMessage="Il cap non può essere più lungo di 5 caratteri")]
        public string Cap { get; set; }

        [MinLength(2, ErrorMessage="La provincia deve avere 2 caratteri")]
        [MaxLength(2, ErrorMessage="La provincia non può essere più lungo di 2 caratteri")]
        public string Provincia { get; set; }
        public string Regione { get; set; }
    }
}
