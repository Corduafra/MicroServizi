
using Guidici.ClientHttp.Abstraction;
using Guidici.Shared;
using System.Globalization;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace Guidici.ClientHttp
{
    public class ClientHttp(HttpClient httpClient) : IClientHttp
    {
        
        public async Task<string?> CreateGuidice(PersonaDto personaDto, CancellationToken c = default)
        {
            var response = await httpClient.PostAsync($"/Guidice/CreateGuidice", JsonContent.Create(personaDto), c);
            return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<string>(cancellationToken: c);
        }

        public async Task<PersonaDto?> GetGuidice(int id, CancellationToken c = default)
        {
            var queryString = QueryString.Create(new Dictionary<string, string?>(){
                {"id", id.ToString(CultureInfo.InvariantCulture) }
            });

            var response = await httpClient.GetAsync($"/Guidice/GetGuidice{queryString}", c);
            return await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<PersonaDto?>(cancellationToken: c);
        }
        


    }
}
