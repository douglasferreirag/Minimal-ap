using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.DB;
using Microsoft.AspNetCore.Mvc;
using minimal_api.Dominio.ModelViews;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministradorServico, AdministradorServiço>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options => {
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});

var app = builder.Build();

app.MapGet("/", () => Results.Json(new Home()));

app.MapPost("/login",  ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) => {

    if(administradorServico.Login(loginDTO) != null ) 

        return Results.Ok("Login com sucesso.");
    
    else

        return Results.Unauthorized();


});

app.UseSwagger();

app.UseSwaggerUI();

app.Run();


