using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smakoowa_Api.Data;
using System;
using System.Net.Http.Headers;
using System.Reflection;

namespace Smakoowa_Api.Tests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {

        public HttpClient _httpClient;

        public CustomWebApplicationFactory()
        {
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(host =>
                {
                    host.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                            typeof(DbContextOptions<DataContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<DataContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDB");
                        });
                    });
                });
            var httpClient = appFactory.CreateClient();
            _httpClient = httpClient;
        }











        //=================================
        //protected override IHost CreateHost(IHostBuilder builder)
        //{
        //    // shared extra set up goes here
        //    return base.CreateHost(builder);
        //}


        ///// <summary>
        ///// Get the application project path where the startup assembly lives
        ///// </summary>    
        //string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
        //{
        //    var projectName = startupAssembly.GetName().Name;

        //    var applicationBaseBath = AppContext.BaseDirectory;

        //    var directoryInfo = new DirectoryInfo(applicationBaseBath);

        //    do
        //    {
        //        directoryInfo = directoryInfo.Parent;
        //        var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));
        //        if (projectDirectoryInfo.Exists)
        //        {
        //            if (new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj")).Exists)
        //                return Path.Combine(projectDirectoryInfo.FullName, projectName);
        //        }
        //    } while (directoryInfo.Parent != null);

        //    throw new Exception($"Project root could not be located using application root {applicationBaseBath}");
        //}

        ///// <summary>
        ///// The temporary test server that will be used to host the controllers
        ///// </summary>
        //private TestServer _server;

        ///// <summary>
        ///// The client used to send information to the service host server
        ///// </summary>
        //public HttpClient HttpClient { get; }

        //public CustomWebApplicationFactory() : this(Path.Combine(""))
        //{ }

        //protected CustomWebApplicationFactory(string relativeTargetProjectParentDirectory)
        //{
        //    //var startupAssembly = typeof(TStatup).GetTypeInfo().Assembly;
        //    //var contentRoot = GetProjectPath(relativeTargetProjectParentDirectory, startupAssembly);

        //    //var configurationBuilder = new ConfigurationBuilder()
        //    //    .SetBasePath(contentRoot)
        //    //    .AddJsonFile("appsettings.json")
        //    //    .AddJsonFile("appsettings.Development.json");


        //    var webHostBuilder = new WebHostBuilder().Build();
        //        //.UseContentRoot(contentRoot)
        //        //.ConfigureServices(InitializeServices)
        //        //.UseConfiguration(configurationBuilder.Build())
        //        //.UseEnvironment("Development")
        //        //.UseStartup(typeof(TStatup));

        //    //create test instance of the server
        //    _server = new TestServer(webHostBuilder);

        //    //configure client
        //    HttpClient = _server.CreateClient();
        //    HttpClient.BaseAddress = new Uri("http://localhost:5005");
        //    HttpClient.DefaultRequestHeaders.Accept.Clear();
        //    HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //}

        ///// <summary>
        ///// Initialize the services so that it matches the services used in the main API project
        ///// </summary>
        //protected virtual void InitializeServices(IServiceCollection services)
        //{
        //    var startupAsembly = typeof(Program).GetTypeInfo().Assembly;
        //    var manager = new ApplicationPartManager
        //    {
        //        ApplicationParts = {
        //        new AssemblyPart(startupAsembly)
        //    },
        //        FeatureProviders = {
        //        new ControllerFeatureProvider()
        //    }
        //    };
        //    services.AddSingleton(manager);
        //}

        ///// <summary>
        ///// Dispose the Client and the Server
        ///// </summary>
        //public void Dispose()
        //{
        //    HttpClient.Dispose();
        //    _server.Dispose();
        //    _ctx.Dispose();
        //}

        //DataContext _ctx = null;
        //public void SeedDataToContext()
        //{
        //    if (_ctx == null)
        //    {
        //        _ctx = _server.Services.GetService<DataContext>();
        //        //if (_ctx != null)
        //        //    _ctx.SeedAppDbContext();
        //    }
        //}
    












    //===============================


    //protected IConfiguration Configuration { get; }

    //public CustomWebApplicationFactory()
    //{
    //    Configuration = new ConfigurationBuilder()
    //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    //        .AddJsonFile("local-appsettings.json", optional: true, reloadOnChange: false)
    //        .Build();
    //}

    //protected override void ConfigureWebHost(IWebHostBuilder builder)
    //{
    //    builder.ConfigureServices(services =>
    //    {
    //        //services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SmakoowaApiDBConnection")));

    //        services.AddDbContext<DataContext>(options => options = new DbContextOptionsBuilder<DataContext>()
    //            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Test;Trusted_Connection=True;")
    //            .UseInMemoryDatabase(databaseName: "TestDatabase"));

    //        builder.UseEnvironment("Development");

    //    });
    //    //CreateNewContextOptions();
    //}

    //public static DbContextOptions<DataContext> CreateNewContextOptions()
    //{
    //    // Create a fresh service provider, and therefore a fresh 
    //    // InMemory database instance.
    //    var serviceProvider = new ServiceCollection()
    //        .AddEntityFrameworkInMemoryDatabase()
    //        .BuildServiceProvider();

    //    // Create a new options instance telling the context to use an
    //    // InMemory database and the new service provider.
    //    var builder = new DbContextOptionsBuilder<DataContext>();
    //    builder.UseInMemoryDatabase(databaseName: "TestDatabase")
    //           .UseInternalServiceProvider(serviceProvider);

    //    return builder.Options;
    //}
}
}
