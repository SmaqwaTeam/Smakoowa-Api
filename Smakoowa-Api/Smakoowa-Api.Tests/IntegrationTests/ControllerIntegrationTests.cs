using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Smakoowa_Api.Data;
using Smakoowa_Api.Models.Interfaces;
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

            string adminTestToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiUGxhY2Vob2xkZXJBZG1pbiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MTg4LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcxODgvIiwiaWF0IjoxNjgwNTMyMzI3fQ.oqBGnpkH2w6e6DuZDtiwFU-Z6CwfcdgAlVVkatyY700";
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

        protected void AssertResponseSuccess(ServiceResponse responseContent)
        {
            Assert.True(responseContent.SuccessStatus, responseContent.Message);
        }

        protected void AssertResponseFailure(ServiceResponse responseContent)
        {
            Assert.False(responseContent.SuccessStatus, responseContent.Message);
        }

        protected virtual async Task<List<T>> FindInDatabaseByConditions<T>(Expression<Func<T, bool>> expresion) where T : class
        {
            return await _context.Set<T>().Where(expresion).ToListAsync();
        }

        protected virtual async Task<T> FindInDatabaseByConditionsFirstOrDefault<T>(Expression<Func<T, bool>> expresion) where T : class
        {
            return await _context.Set<T>().Where(expresion).FirstOrDefaultAsync();
        }
    }
}