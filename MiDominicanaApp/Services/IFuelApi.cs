using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace MiDominicanaApp.Services
{
    public interface IFuelApi
    {
        // http://eladio37-001-site1.ftempurl.com/api/Fuels
        [Get("/api/Fuels")]
        Task<HttpResponseMessage> GetFuelsAsync();
    }
}
