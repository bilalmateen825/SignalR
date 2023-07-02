// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Common;

Console.WriteLine("Hello, World!");


CreateWebHostBuilder().Build().Run();

//IWebHostBuilder CreateWebHostBuilder() => WebHost.CreateDefaultBuilder().UseStartup<Startup>();
IWebHostBuilder CreateWebHostBuilder()
{
    IWebHostBuilder builder = WebHost.CreateDefaultBuilder();
    builder.UseStartup<Startup>();
    
    //Define Custom Urls
    //builder.UseUrls("http://localhost:5001"); // Specify custom port here

    return builder;
}

class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR();        
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ChatHub>("/ChatHub");
        });
        //.UseUrls("http://localhost:5001") // Specify custom port here
    }
}