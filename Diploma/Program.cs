using DevExpress.AspNetCore;
using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Json;
using Diploma.Models;
using Microsoft.Extensions.FileProviders;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

IFileProvider? fileProvider = builder.Environment.ContentRootFileProvider;
IConfiguration? configuration = builder.Configuration;

IFileLoader loader = new DefaultFileLoader();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IFileLoader, DefaultFileLoader>();
builder.Services.AddSingleton<ExcelToJsonParser>();

builder.Services.AddDevExpressControls();
builder.Services.AddScoped<DashboardConfigurator>((IServiceProvider serviceProvider) =>
{

    DashboardConfigurator configurator = new DashboardConfigurator();
    foreach (var jsonFile in loader.GetFileList())
    {
        configurator.SetDashboardStorage(new DashboardFileStorage($"{Directory.GetCurrentDirectory()}\\AppData\\Dashboards"));
        if (Path.GetExtension(jsonFile) == ".json")
        {
            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
            DashboardJsonDataSource jsonDataSourceString = new DashboardJsonDataSource($"JSON Data Source ({jsonFile})");
            Elem elem = new Elem("qwe", "qwe", "qwe");
            System.Xml.Linq.XElement xElement = new("asd", 23);
            string json = loader.GetFile(jsonFile);
            jsonDataSourceString.JsonSource = new CustomJsonSource(json);
            jsonDataSourceString.RootElement = "Value";
            //dataSourceStorage.RegisterDataSource(jsonFile, jsonDataSourceString.SaveToXml());
            dataSourceStorage.RegisterDataSource(xElement);
            configurator.SetDataSourceStorage(dataSourceStorage);
            Console.WriteLine(jsonFile);
            configurator.SetConnectionStringsProvider(new DashboardConnectionStringsProvider(configuration));
        }
    }
    
    return configurator;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

Console.WriteLine(loader.GetFile("asdasd.json"));

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseDevExpressControls();

app.UseRouting();

app.UseAuthorization();

app.MapDashboardRoute("api/dashboard", "DefaultDashboard");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FileLoader}/{action=Dashboard}/{id?}");

app.Run();