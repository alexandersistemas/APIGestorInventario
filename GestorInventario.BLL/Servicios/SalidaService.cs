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
    public class SalidaService : ISalidaService
    {

        private readonly IGenericRepository<SalidasInventario> _salidaRepositorio;
        private readonly IMapper _mapper;

        public SalidaService(IGenericRepository<SalidasInventario> salidaRepositorio, IMapper mapper)
        {
            _salidaRepositorio = salidaRepositorio;
            _mapper = mapper;
        }


        public async Task<List<SalidasInventarioDTO>> lista()
        {
            try
            {
                var querySalida = await _salidaRepositorio.Consultar();
                var listaSalidas = querySalida
                    .ToList();
                return _mapper.Map<List<SalidasInventarioDTO>>(listaSalidas);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SalidasInventarioDTO> Crear(SalidasInventarioDTO modelo)
        {
            try
            {
                var salidaCreada = await _salidaRepositorio.Crear(_mapper.Map<SalidasInventario>(modelo));

                if (salidaCreada.IdEntrada == 0)
                    throw new TaskCanceledException("No se pudo crear la salida");

                return _mapper.Map<SalidasInventarioDTO>(salidaCreada);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(SalidasInventarioDTO modelo)
        {
            try
            {
                var salidaModelo = _mapper.Map<SalidasInventarioDTO>(modelo);
                var salidaEncontrada = await _salidaRepositorio.Obtener(u =>
                u.IdSalida  == salidaModelo.IdSalida
                );

                if (salidaEncontrada == null)
                    throw new TaskCanceledException("Salida no existe");

                salidaEncontrada.IdProducto = salidaModelo.IdProducto;
                salidaEncontrada.IdEntrada = salidaModelo.IdEntrada;
                salidaEncontrada.Cantidad = salidaModelo.Cantidad;
                salidaEncontrada.FechaSalida = salidaModelo.FechaSalida;

                bool respuesta = await _salidaRepositorio.Editar(salidaEncontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar la salida");

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
                var salidaEncontrada = await _salidaRepositorio.Obtener(u => u.IdSalida == id);

                if (salidaEncontrada == null)
                    throw new TaskCanceledException("Salida no existe");

                bool respuesta = await _salidaRepositorio.Eliminar(salidaEncontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo eliminar la salida");

                return respuesta;

            }
            catch
            {
                throw;
            }
        }       

        public async Task<List<SalidasInventarioDTO>> Obtener(int id)
        {
            try
            {
                var querySalida = await _salidaRepositorio.Consultar(u => u.IdSalida == id);
                var listaSalida = querySalida
                    .Include(Producto => Producto.IdProducto)
                    .Include(Entrada => Entrada.IdEntrada)
                    .ToList();
                return _mapper.Map<List<SalidasInventarioDTO>>(listaSalida);
            }
            catch
            {
                throw;
            }
        }
    }
}
