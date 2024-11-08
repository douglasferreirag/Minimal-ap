using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.DB;
using Microsoft.AspNetCore.Mvc;
using minimal_api.Dominio.ModelViews;
using minimal_api.Dominio.Entidades;

var builder = WebApplication.CreateBuilder(args);

#region Builder
    

builder.Services.AddScoped<IAdministradorServico, AdministradorServiço>();

builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options => {
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});

var app = builder.Build();

#endregion


#region  Home

app.MapGet("/", () => Results.Json(new Home()));

#endregion

#region Administradores

app.MapPost("/administradores/login",  ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) => {

    if(administradorServico.Login(loginDTO) != null ) 

        return Results.Ok("Login com sucesso.");
    
    else

        return Results.Unauthorized();


});

#endregion

#region  Veiculos

app.MapPost("/veiculos",  ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) => {

   var veiculo = new Veiculo{

        Nome = veiculoDTO.Nome,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano
   };

   veiculoServico.Incluir(veiculo);

   return Results.Created($"/veiculo/{veiculo.Id}", veiculo);


});

#endregion

#region App

app.UseSwagger();

app.UseSwaggerUI();

app.Run();

#endregion 

