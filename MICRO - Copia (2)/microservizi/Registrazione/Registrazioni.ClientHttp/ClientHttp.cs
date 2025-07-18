using Registrazioni.ClientHttp.Abstraction;
using Registrazioni.Shared;
using System.Globalization;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace Registrazioni.ClientHttp
{
    public class ClientHttp(HttpClient httpClient) : IClientHttp
    {

        public async Task <string?> CreateCaneAsync(CaneDto caneDto, CancellationToken c = default)
        {
            var response = await httpClient.PostAsync($"/Cane/CreateCane", JsonContent.Create(caneDto), c);
            return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<string>(cancellationToken: c);
        }

        public async Task<CaneDto?> GetCaneDtoAsync(int id, CancellationToken c = default)
        {
            var queryString = QueryString.Create(new Dictionary<string, string?>()
            {
                {"id",id.ToString(CultureInfo.InvariantCulture) }
            });
            var response = await httpClient.GetAsync($"/Cane/GetCane{queryString}", c);
            return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<CaneDto?>(cancellationToken: c);
        }




    }
}
