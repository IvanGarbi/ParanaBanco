using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ParanaBancoCase.Business.Interfaces;
using ParanaBancoCase.Business.Notificacoes;
using ParanaBancoCase.Business.Services;
using ParanaBancoCase.Data.Context;
using ParanaBancoCase.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API - Paraná Banco",
        Version = "Versão 1",
        Description = "Esta API faz parte do Case do Paraná Banco",
        Contact = new OpenApiContact() { Name = "Ivan Assumpção", Email = "ivan_garbi@hotmail.com" }
    });
});

builder.Services.AddDbContext<ParanaBancoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ParanaBancoDbContext>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<INotificador, Notificador>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddAutoMapper(typeof(Program));


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
