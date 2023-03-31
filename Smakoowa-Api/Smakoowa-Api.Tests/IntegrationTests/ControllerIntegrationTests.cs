﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Smakoowa_Api.Data;
using Smakoowa_Api.Models.Interfaces;
using Smakoowa_Api.Models.ResponseDtos;
using Smakoowa_Api.Models.Services;
using System.Linq.Expressions;
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
            await _context.AddRangeAsync(models);
            await _context.SaveChangesAsync();
        }

        protected async Task AddToDatabase(IDbModel model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
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