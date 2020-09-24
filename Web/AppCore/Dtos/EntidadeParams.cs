using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Dtos
{
    public class EntidadeParams
    {
        public int id { get; } = 0;
        public string texto { get; }
        public int valor { get; } = 0;
        public DateTime? data { get; }
        public bool ativo { get; } = true;
        public int limit { get; } = 50;
        public int page { get;} = 1;
    }
}
