using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using AutoMapper;
using Microsoft.EntityFrameworkCore;
using GestorInventario.BLL.Servicios.Contrato;
using GestorInventario.DAL.Repositorios.Contrato;
using GestorInventario.DTO;
using GestorInventario.Model;
    
namespace GestorInventario.BLL.Servicios
{
    public class EntradaService : IEntradaService
    {

        private readonly IGenericRepository<EntradasInventario> _entradaRepositorio;
        private readonly IMapper _mapper;

        public EntradaService(IGenericRepository<EntradasInventario> entradaRepositorio, IMapper mapper)
        {
            _entradaRepositorio = entradaRepositorio;
            _mapper = mapper;
        }

        public async Task<List<EntradasInventarioDTO>> lista()
        {
            try
            {
                var queryEntrada = await _entradaRepositorio.Consultar();
                var listaEntradas = queryEntrada
                    .ToList();
                return _mapper.Map<List<EntradasInventarioDTO>>(listaEntradas);
            }
            catch
            {
                throw;
            }
        }

        public async Task<EntradasInventarioDTO> Crear(EntradasInventarioDTO modelo)
        {
            try
            {
                var entradaCreada = await _entradaRepositorio.Crear(_mapper.Map<EntradasInventario>(modelo));

                if (entradaCreada.IdEntrada == 0)
                    throw new TaskCanceledException("No se pudo crear la entrada");

                return _mapper.Map<EntradasInventarioDTO>(entradaCreada);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(EntradasInventarioDTO modelo)
        {
            try
            {
                var entradaModelo = _mapper.Map<EntradasInventario>(modelo);
                var entradaEncontrada = await _entradaRepositorio.Obtener(u =>
                u.IdEntrada == entradaModelo.IdEntrada
                );

                if (entradaEncontrada == null)
                    throw new TaskCanceledException("Entrada no existe");

                entradaEncontrada.IdProducto = entradaModelo.IdProducto;
                entradaEncontrada.Cantidad = entradaModelo.Cantidad;
                entradaEncontrada.FechaCaducidad = entradaModelo.FechaCaducidad;
                entradaEncontrada.FechaEntrada = entradaModelo.FechaEntrada;

                bool respuesta = await _entradaRepositorio.Editar(entradaEncontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar la entrada");

                return respuesta;


            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var entradaEncontrada = await _entradaRepositorio.Obtener(u => u.IdEntrada == id);

                if (entradaEncontrada == null)
                    throw new TaskCanceledException("Entrada no existe");

                bool respuesta = await _entradaRepositorio.Eliminar(entradaEncontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo eliminar la entrada");

                return respuesta;

            }
            catch
            {
                throw;
            }
        }

       

        public async Task<List<EntradasInventarioDTO>> Obtener(int id)
        {
            try
            {
                var queryEntrada = await _entradaRepositorio.Consultar(u => u.IdEntrada == id);
                var listaEntrada = queryEntrada
                    .Include(Producto => Producto.IdProducto)
                    .ToList();
                return _mapper.Map<List<EntradasInventarioDTO>>(listaEntrada);
            }
            catch
            {
                throw;
            }
        }

    }
}
