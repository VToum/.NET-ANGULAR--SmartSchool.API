using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Helpers.Pages
{
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse response,
            int PageAtual, int TamanhoPage, int TotalContador, int TotalPage)
        {
            var paginationHeadar = new PaginationHeader(PageAtual, TamanhoPage, TotalContador, TotalPage);

            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeadar));
            response.Headers.Add("Access-Control-Expose-Header", "Pagination");
        }
    }
}
