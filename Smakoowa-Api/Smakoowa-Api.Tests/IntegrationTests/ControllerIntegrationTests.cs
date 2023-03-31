using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Smakoowa_Api.Data;
using Smakoowa_Api.Models.Interfaces;
using Xunit;

namespace Smakoowa_Api.Tests.IntegrationTests
{
    public class ControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        protected readonly HttpClient _HttpClient;
        protected readonly DataContext _context;
        protected readonly IConfiguration _configuration;

        public ControllerIntegrationTests(CustomWebApplicationFactory<Program> fixture)
        {
            _HttpClient = fixture._httpClient;
            _context = fixture._context;
            _configuration = fixture._configuration;
        }

        protected async Task<T> DeserializeResponse<T>(HttpResponseMessage response) where T : class
        {
            var resp = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(resp);
        }

        protected async Task AddToDatabase(IEnumerable<IDbModel> models)
        {
            foreach (var model in models) _context.Add(model);
            await _context.SaveChangesAsync();
        }
    }
}