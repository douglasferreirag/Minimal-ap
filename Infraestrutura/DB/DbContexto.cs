using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Entidades;

namespace minimal_api.Infraestrutura.DB
{
    public class DbContexto: DbContext
    {

            private readonly IConfiguration _configuraçãoAppSettings;

            public DbContexto(IConfiguration configuraçãoAppSettings){

                    _configuraçãoAppSettings = configuraçãoAppSettings;

            }

            public DbSet<Administrador> Administradores { get; set; } = default!;

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {

                if(!optionsBuilder.IsConfigured) {

                            var stringConexao = _configuraçãoAppSettings.GetConnectionString("mysql")?.ToString();

                            if(!string.IsNullOrEmpty(stringConexao)) {

                                optionsBuilder.UseMySql(    stringConexao, 
                                                            ServerVersion.AutoDetect(stringConexao)
                                                    );

                            }

                    
                }

              

                
            }

    }
}