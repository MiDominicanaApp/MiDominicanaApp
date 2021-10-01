using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace MiDominicanaApp.Services
{
    public class FuelApiService : IFuelApiService
    {
        public async Task<HttpResponseMessage> GetFuelsAsync()
        {
            return await RestService.For<IFuelApi>("http://eladio37-001-site1.ftempurl.com").GetFuelsAsync();
        }
    }
}
