using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Helpers
{
    public static class DateTimeExtensions
    {
        public static int PegaAnoAtual(this DateTime dateTime)
        {
            var dataAtual = DateTime.UtcNow;
            int ano = dataAtual.Year - dateTime.Year;

            if (dataAtual < dateTime.AddYears(ano))
                ano--;
        
            return ano;
        }
    }
}
