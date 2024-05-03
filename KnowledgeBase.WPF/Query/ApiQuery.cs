using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using KnowledgeBase.WPF.Models;

namespace KnowledgeBase.WPF.Query
{
    public class ApiQuery
    {
        private readonly string _url = "http://localhost:5111";
        private readonly HttpClient _httpClient;

        public ApiQuery(HttpClient client)
        {
            _httpClient = client;
        }



        #region Commands

        public async Task<string> CreateCommandAsync(Command c, Descriptor d, Description? desc)
        {
            if (desc == null) desc = new Description() { DescriptionText = "", Version = "1.0.0" };
            using StringContent jsonContent = new(JsonSerializer.Serialize(new CreateCSD_DTO
            {
                 Command = c.CommandText, System = d.System, Tech = d.Tech, Lang = d.Lang, Description = desc.DescriptionText, Version = desc.Version  
            }),Encoding.UTF8,"application/json");

            
            var response = await _httpClient.PostAsync($"{_url}/api/command", jsonContent);
            if(response != null)
            {
                var ID = await response.Content.ReadAsStringAsync();
                return ID;
            }

            return string.Empty;
        }
        public async Task<string> DeleteCommandAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_url}/api/command/{id}");
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> UpdateCommandAsync(UpdateCSD_DTO update)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(update),Encoding.UTF8,"application/json");

            var response = await _httpClient.PutAsync($"{_url}/api/command", jsonContent);
            if (response != null)
            {
                return response.Content.ToString();
            }
            return string.Empty;
        }
        public async Task<IEnumerable<ReadCSD_DTO>> GetAllCommandsAsync()
        {
            IEnumerable<ReadCSD_DTO>? response = await _httpClient.GetFromJsonAsync<IEnumerable<ReadCSD_DTO>>($"{_url}/api/command");
            if (response == null) throw new Exception();

            return response;
        }
        public async Task<ReadCSD_DTO> GetCommandByIdAsync(Guid id)
        {
            IEnumerable<ReadCSD_DTO>? response = await _httpClient.GetFromJsonAsync<IEnumerable<ReadCSD_DTO>>($"{_url}/api/command?id={id.ToString()}");
            if (response is null) throw new Exception();

            var command = response.FirstOrDefault() ?? new ReadCSD_DTO();
            return command;
        }
        public async Task<IEnumerable<ReadCSD_DTO>> GetCommandsBySystemAsync(string system)
        {
            IEnumerable<ReadCSD_DTO>? response = await _httpClient.GetFromJsonAsync<IEnumerable<ReadCSD_DTO>>($"{_url}/api/command/system/{system}");
            if(response == null) throw new Exception();
            return response;
        }
        public async Task<IEnumerable<ReadCSD_DTO>> GetCommandsByTechAsync(string tech)
        {
            IEnumerable<ReadCSD_DTO>? response = await _httpClient.GetFromJsonAsync<IEnumerable<ReadCSD_DTO>>($"{_url}/api/command/tech/{tech}");
            if (response == null) throw new Exception();
            return response;
        }
        public async Task<IEnumerable<ReadCSD_DTO>> GetCommandsByLangAsync(string lang)
        {
            IEnumerable<ReadCSD_DTO>? response = await _httpClient.GetFromJsonAsync<IEnumerable<ReadCSD_DTO>>($"{_url}/api/command/lang/{lang}");
            if (response == null) throw new Exception();
            return response;
        }

        #endregion

        #region Snippets
        public async Task<IEnumerable<ReadCSD_DTO>> GetAllSnippetsAsync()
        {
            IEnumerable<ReadCSD_DTO>? response = await _httpClient.GetFromJsonAsync<IEnumerable<ReadCSD_DTO>>($"{_url}/api/snippet");
            if (response == null) throw new Exception();

            return response;
        }
        public async Task<ReadCSD_DTO> GetSnippetByIdAsync(Guid id)
        {
            IEnumerable<ReadCSD_DTO>? response = await _httpClient.GetFromJsonAsync<IEnumerable<ReadCSD_DTO>>($"{_url}/api/snippet?id={id.ToString()}");
            if (response == null) throw new Exception();

            var snippet = response.First();
            return snippet;
        }



        #endregion

        #region Documentations

        public async Task<IEnumerable<ReadCSD_DTO>> GetAllDocumentationsAsync()
        {
            IEnumerable<ReadCSD_DTO>? response = await _httpClient.GetFromJsonAsync<IEnumerable<ReadCSD_DTO>>($"{_url}/api/documentation");
            if (response == null) throw new Exception();

            return response;
        }



        #endregion

        #region Infos
        public async Task<IEnumerable<string>> GetAllSystemsAsync()
        {
            IEnumerable<string>? response = await _httpClient.GetFromJsonAsync<IEnumerable<string>>($"{_url}/systemlist");
                if (response == null) throw new Exception();

                return from data in response select data;
        }

        public async Task<IEnumerable<string>> GetAllTechsAsync()
        {
            IEnumerable<string>? response = await _httpClient.GetFromJsonAsync<IEnumerable<string>>($"{_url}/techlist");
            if (response == null) throw new Exception();

            return from data in response select data;
        }
        public async Task<IEnumerable<string>> GetAllLangsAsync()
        {
            IEnumerable<string>? response = await _httpClient.GetFromJsonAsync<IEnumerable<string>>($"{_url}/langlist");
            if (response == null) throw new Exception();

            return from data in response select data;
        }

        #endregion
    }




}
