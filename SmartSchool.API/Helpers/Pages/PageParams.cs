using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Helpers
{
    public class PageParams
    {
        public const int MaxPageTamanho = 50;
        public int PegaNumero { get; set; } = 1;

        private int tamanhoPage = 10;
        public int TamanhoPage
        { 
            get { return tamanhoPage; }
            set { tamanhoPage = (value > MaxPageTamanho) ? MaxPageTamanho : value; } 
        }

        public int? Matricula { get; set; } = null;
        public string Nome { get; set; } = string.Empty;
        public int? Ativo { get; set; } = null;

    }
}
