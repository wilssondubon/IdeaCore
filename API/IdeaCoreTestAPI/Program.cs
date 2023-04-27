using IdeaCoreApplication.Contracts;
using IdeaCoreApplication;
using Microsoft.AspNetCore.Mvc;
using Services;
using IdeaCoreInterfaces.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Infraestructure.UnitOfWork;
using Infraestructure.DBContext;
using IdeaCoreTestAPI.MapperProfile;
using IdeaCoreTestAPI.hateoas;
using IdeaCoreHateoas.DI;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

var configoutput = (MvcOptions config) => {
    //var formaters = config.OutputFormatters;

    var jsonoutput = config.OutputFormatters.OfType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter>()?.FirstOrDefault();

    if (jsonoutput != null)
    {
        jsonoutput.SupportedMediaTypes.Add(new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/api.genesis.hateoas+json"));
    }
    config.RespectBrowserAcceptHeader = true;
};

builder.Services.AddControllers().AddMvcOptions(configoutput).AddXmlSerializerFormatters();

builder.Services.Configure<JsonOptions>(o =>
{
    o.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<TestDBContext>(options => options.UseSqlServer(""));
builder.Services.AddTransient<IUnitOfWork, TestUnitOfWork>();
builder.Services.AddScoped<ITipoService, TipoService>();
builder.Services.AddScoped<IElectrodomesticoService, ElectrodomesticoService>();
builder.Services.AddScoped<IAppServices, AppServices>();
builder.Services.AddHateoasMapper<HateoasEntitiesProfile, HateoasAPI>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
