using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Models
{
    public class Entidade : BaseEntity
    {
        public string texto { get; set; }
        public int valor { get; set; }
        public DateTime? data {get;set;}
        public bool ativo { get; set; }
    }
}
