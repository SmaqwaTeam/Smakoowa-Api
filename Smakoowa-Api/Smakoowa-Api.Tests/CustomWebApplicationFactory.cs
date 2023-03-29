
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smakoowa_Api.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smakoowa_Api.Tests
{
    public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected IConfiguration Configuration { get; }

        public CustomWebApplicationFactory()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile("local-appsettings.json", optional: true, reloadOnChange: false)
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //    var dbContextDescriptor = services.SingleOrDefault(
                //        d => d.ServiceType ==
                //            typeof(DbContextOptions<DataContext>));

                //    services.Remove(dbContextDescriptor);

                //    var dbConnectionDescriptor = services.SingleOrDefault(
                //        d => d.ServiceType ==
                //            typeof(DbConnection));

                //    services.Remove(dbConnectionDescriptor);

                //    // Create open SqliteConnection so EF won't automatically close it.
                //    services.AddSingleton<DbConnection>(container =>
                //    {
                //        var connection = new SqliteConnection("DataSource=:memory:");
                //        //var connection = new SqliteConnection("Server=(localdb)\\mssqllocaldb;Database=SmakoowaApiDB;Trusted_Connection=True;MultipleActiveResultSets=true");
                //        connection.Open();

                //        return connection;
                //    });

                //    services.AddDbContext<DataContext>((container, options) =>
                //    {
                //        var connection = container.GetRequiredService<DbConnection>();
                //        options.UseSqlServer(connection);
                //    });
                //});
                services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SmakoowaApiDBConnection")));

                builder.UseEnvironment("Development");
            });
        }
    }
}
