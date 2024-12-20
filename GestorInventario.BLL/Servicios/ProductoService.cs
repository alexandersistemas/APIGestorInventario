﻿using System;
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
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepositorio, IMapper mapper)
        {
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }


        public async Task<List<ProductoDTO>> lista()
        {
            try
            {
                var queryProducto = await _productoRepositorio.Consultar();
                var listaProductos = queryProducto
                    .ToList();
                return _mapper.Map<List<ProductoDTO>>(listaProductos);
            }
            catch
            {
                throw;
            }

        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var productoCreado = await _productoRepositorio.Crear(_mapper.Map<Producto>(modelo));

                if (productoCreado.IdProducto == 0)
                    throw new TaskCanceledException("No se pudo crear el producto");

                return _mapper.Map<ProductoDTO>(productoCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Producto>(modelo);
                var productoEncontrado = await _productoRepositorio.Obtener(u =>
                u.IdProducto == productoModelo.IdProducto
                );

                if (productoEncontrado == null)
                    throw new TaskCanceledException("El producto no existe");

                productoEncontrado.Nombre  = productoModelo.Nombre;
                productoEncontrado.Descripcion = productoModelo.Descripcion;
                productoEncontrado.FechaCreacion = productoModelo.FechaCreacion;


                bool respuesta = await _productoRepositorio.Editar(productoEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar el producto");

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
                var productoEncontrado = await _productoRepositorio.Obtener(u => u.IdProducto == id);

                if (productoEncontrado == null)
                    throw new TaskCanceledException("El producto no existe");

                bool respuesta = await _productoRepositorio.Eliminar(productoEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo eliminar el producto");

                return respuesta;

            }
            catch
            {
                throw;
            }

        }

        public async Task<List<ProductoDTO>> Obtener(int id)
        {
            try
            {
                var queryProducto = await _productoRepositorio.Consultar(u => u.IdProducto == id);
                var listaProducto = queryProducto                    
                    .ToList();
                return _mapper.Map<List<ProductoDTO>>(listaProducto);
            }
            catch
            {
                throw;
            }
        }
    }
}
