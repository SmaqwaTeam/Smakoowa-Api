using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using Smakoowa_Api.Data;
using Smakoowa_Api.Models.Interfaces;
using Smakoowa_Api.Models.RequestDtos;
using Smakoowa_Api.Models.Services;
using System.Linq.Expressions;
using System.Net.Http.Headers;
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

            string adminTestToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiUGxhY2Vob2xkZXJBZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InBsYWNlaG9sZGVyQWRtaW5AdGVzdC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzE4OC8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MTg4LyIsImlhdCI6MTY4MDUzMjMyN30.HZOE3I64RIKKyvev47oMvzyWeqyz4Aewc2VzndfdxQs";
            _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminTestToken);
        }

        protected async Task<T> DeserializeResponse<T>(HttpResponseMessage response) where T : class
        {
            var resp = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(resp);
        }

        protected async Task AddToDatabase(IEnumerable<IDbModel> models)
        {
            await _context.AddRangeAsync(models);
            await _context.SaveChangesAsync();
        }

        protected async Task<IDbModel> AddToDatabase(IDbModel model)
        {
            var addedModel = await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return addedModel.Entity;
        }

        protected void AssertResponseSuccess(HttpResponseMessage response, ServiceResponse responseContent)
        {
            response.EnsureSuccessStatusCode();
            Assert.True(responseContent.SuccessStatus);
        }

        protected void AssertResponseFailure(HttpResponseMessage response, ServiceResponse responseContent)
        {
            response.EnsureSuccessStatusCode();
            Assert.False(responseContent.SuccessStatus);
        }

        public virtual async Task<List<T>> FindInDatabaseByConditions<T>(Expression<Func<T, bool>> expresion) where T : class
        {
            return await _context.Set<T>().Where(expresion).ToListAsync();
        }

        public virtual async Task<T> FindInDatabaseByConditionsFirstOrDefault<T>(Expression<Func<T, bool>> expresion) where T : class
        {
            return await _context.Set<T>().Where(expresion).FirstOrDefaultAsync();
        }
    }
}