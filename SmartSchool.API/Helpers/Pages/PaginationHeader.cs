using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Helpers.Pages
{
    public class PaginationHeader
    {
        public int PageAtual { get; set; }
        public int TamanhoPage { get; set; }
        public int TotalContador { get; set; }
        public int TotalPage { get; set; }
        public PaginationHeader(int pageAtual, int tamanhoPage, int totalContador, int totalPage)
        {
            this.PageAtual = pageAtual;
            this.TamanhoPage = tamanhoPage;
            this.TotalContador = totalContador;
            this.TotalPage = totalPage;
        }
 
    }
}
