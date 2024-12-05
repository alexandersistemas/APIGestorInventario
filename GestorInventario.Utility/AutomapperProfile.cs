using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using GestorInventario.DTO;
using GestorInventario.Model;


namespace GestorInventario.Utility
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<Menu, MenuDTO>().ReverseMap();
            CreateMap<EntradasInventario, EntradasInventarioDTO>().ReverseMap();
            CreateMap<SalidasInventario, SalidasInventarioDTO>().ReverseMap();
            CreateMap<EstadoInventario, EstadoInventarioDTO>().ReverseMap();
        }

    }
}
