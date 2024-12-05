using AutoMapper;
using GestorInventario.BLL.Servicios.Contrato;
using GestorInventario.DAL.Repositorios.Contrato;
using GestorInventario.DTO;
using GestorInventario.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorInventario.BLL.Servicios
{
    public class MenuService : IMenuService
    {

        private readonly IGenericRepository<Menu> _menuRepositorio;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Menu> menuRepositorio, IMapper mapper)
        {
            _menuRepositorio = menuRepositorio;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Lista()
        {
            try
            {
                var queryProducto = await _menuRepositorio.Consultar();
                var listaMenu = queryProducto
                    .ToList();
                return _mapper.Map<List<MenuDTO>>(listaMenu);
            }
            catch
            {
                throw;
            }
        }
    }
}
