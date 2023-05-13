using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smakoowa_Api.Data;
using Smakoowa_Api.Services.Interfaces;
using Smakoowa_Api.Services.Tests;

namespace Smakoowa_Api.Tests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public HttpClient _httpClient;
        public DataContext _context;
        public IConfiguration _configuration;

        public CustomWebApplicationFactory()
        {
            var appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(host =>
                {
                    host.ConfigureServices(services =>
                    {
                        services.Remove(services.SingleOrDefault(
                            d => d.ServiceType ==
                            typeof(DbContextOptions<DataContext>)));

                        services.Remove(services.SingleOrDefault(
                            d => d.ServiceType ==
                            typeof(DbContextOptions<BackgroundDataContext>)));

                        services.Remove(services.SingleOrDefault(
                            d => d.ServiceType ==
                            typeof(IApiUserService)));

                        services.AddScoped(typeof(IApiUserService), typeof(TestApiUserService));

                        services.AddDbContext<DataContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDB");
                        }, ServiceLifetime.Scoped);

                        services.AddDbContext<BackgroundDataContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryBackgroundDB");
                        }, ServiceLifetime.Singleton);
                    });
                });

            _configuration = appFactory.Services.GetService<IConfiguration>();
            _context = appFactory.Services.CreateScope().ServiceProvider.GetService<DataContext>();
            _httpClient = appFactory.CreateClient();
        }
    }
}
