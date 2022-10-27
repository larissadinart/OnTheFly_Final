using Newtonsoft.Json;
using OnTheFly_Final.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnTheFly_Final.Services
{
    public class AddressServices
    {
        public async Task<Address> GetAdress(string cep)
        {

            Address address;
            using HttpClient _adressClient = new();
            HttpResponseMessage response = await _adressClient.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
            var addressJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return address = JsonConvert.DeserializeObject<Address>(addressJson);
            else
                return null;
        }
    }
}
