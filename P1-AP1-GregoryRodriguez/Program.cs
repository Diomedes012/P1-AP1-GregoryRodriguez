using P1_AP1_GregoryRodriguez.Components;
using P1_AP1_GregoryRodriguez.Data;
using Microsoft.EntityFrameworkCore;
using P1_AP1_GregoryRodriguez.Services;
using Blazored.Toast;

namespace P1_AP1_GregoryRodriguez;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddBlazorBootstrap();

        var ConnectionString = builder.Configuration.GetConnectionString("ConStr");

        builder.Services.AddDbContextFactory<Contexto>(option => option.UseSqlite(ConnectionString));
        builder.Services.AddScoped<EntradasGuacalesService>();
        builder.Services.AddScoped<TiposHuacalesService>();

        var app = builder.Build();

        

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
