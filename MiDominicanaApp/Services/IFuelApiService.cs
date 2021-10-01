using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiDominicanaApp.Services
{
    public interface IFuelApiService
    {
        Task<HttpResponseMessage> GetFuelsAsync();
    }
}
