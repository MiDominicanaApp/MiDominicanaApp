using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace MiDominicanaApp.Services
{
    public class MiDominicanaApiService : IMiDominicanaApiService
    {

        IMiDominicanaApi _miDominicanaApi;

        public MiDominicanaApiService()
        {
            Task.Run(() =>
            {
                _miDominicanaApi = RestService.For<IMiDominicanaApi>(Config.MiDominicanaApiBaseURL);
            });
        }

        public async Task<HttpResponseMessage> GetCurrenciesAsync()
        {
            return await _miDominicanaApi.GetCurrenciesAsync();
        }

        public async Task<HttpResponseMessage> GetFuelsAsync()
        {
            return await _miDominicanaApi.GetFuelsAsync();
        }

        public async Task<HttpResponseMessage> GetProvinceDetailAsync(int provinceId)
        {
            return await _miDominicanaApi.GetProvinceDetailAsync(provinceId);
        }

        public async Task<HttpResponseMessage> GetProvincesAsync()
        {
            return await _miDominicanaApi.GetProvincesAsync();
        }
    }
}
