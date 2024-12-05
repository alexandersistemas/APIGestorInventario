using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GestorInventario.DAL.Repositorios.Contrato;
using GestorInventario.DAL.Repositorios;
using GestorInventario.DAL.DBContext;

using GestorInventario.Utility;
using GestorInventario.BLL.Servicios.Contrato;
using GestorInventario.BLL.Servicios;
using Microsoft.Data.SqlClient;



namespace GestorInventario.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {


            // se obtiene la cadena de conexión
            var cadenaSQL = configuration.GetConnectionString("CadenaSQL");

            services.AddDbContext<GestorInventarioContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CadenaSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IEntradaRepository, EntradaRepository>();


            services.AddAutoMapper(typeof(AutomapperProfile));

            services.AddScoped<IEntradaService, EntradaService>();
            services.AddScoped<ISalidaService, SalidaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IMenuService, MenuService>();


        }



    }
}
