using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Helpers
{
    public class PageList<T> : List<T>
    {
        public int TotalContador { get; set; }
        public int TamanhoPage { get; set; }
        public int PageAtual { get; set; }
        public int TotalPage { get; set; }

        public PageList(List<T> items, int contador, int numeroPage, int tamanhoPage)
        {
            TotalContador = contador;
            TamanhoPage = tamanhoPage;
            PageAtual = numeroPage;
            TotalPage = (int)Math.Ceiling(contador / (double)tamanhoPage);
            this.AddRange(items);
        }

        public static async Task<PageList<T>> CreateAsync(
            IQueryable<T> fonte, int pegaNumero, int tamanhoPage) 
        {
            var contador = await fonte.CountAsync();
            var items = await fonte.Skip((pegaNumero - 1) * tamanhoPage)
                .Take(tamanhoPage)
                .ToListAsync();
            return new PageList<T>(items, contador, pegaNumero, tamanhoPage);
        }
    }
}

