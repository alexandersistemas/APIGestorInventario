using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorInventario.DAL.Repositorios.Contrato;
using GestorInventario.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
//using GestorInventario.DTO;
using GestorInventario.Model;
//using AutoMapper;
//using GestorInventario.Utility;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;


namespace GestorInventario.DAL.Repositorios
{
    public class GenericRepository<TModelo> : IGenericRepository<TModelo> where TModelo : class
    {
        private readonly GestorInventarioContext _dbContext;

        public GenericRepository(GestorInventarioContext dbContext)
        {
            _dbContext = dbContext;            
        }

        public async Task<TModelo> Obtener(Expression<Func<TModelo, bool>> filtro)
        {
            try
            {
                TModelo modelo = await _dbContext.Set<TModelo>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch
            {
                throw;
            }
        }        

        public async Task<TModelo> Crear(TModelo modelo)
        {
            try
            {
                _dbContext.Set<TModelo>().Add(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(TModelo modelo)
        {
            try
            {
                _dbContext.Set<TModelo>().Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(TModelo modelo)
        {
            try
            {
                _dbContext.Set<TModelo>().Remove(modelo);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<TModelo>> Consultar(Expression<Func<TModelo, bool>> filtro = null)
        {
            try
            {

                // Verificar si el DbContext está inicializado
                Console.WriteLine(_dbContext == null ? "DbContext es null" : "DbContext está inicializado");

                if (_dbContext == null)
                {       
                    throw new NullReferenceException("El contexto de base de datos (_dbContext) no está inicializado.");
                }
              
                var dbSet = _dbContext.Set<TModelo>();
                if (dbSet == null)
                {
                    throw new InvalidOperationException($"El DbSet para el tipo {typeof(TModelo).Name} no está configurado en el contexto.");
                }


                IQueryable<TModelo> queryModelo = filtro == null ? _dbContext.Set<TModelo>() : _dbContext.Set<TModelo>().Where(filtro);
                return queryModelo;
            }
            catch
            {
                throw;
            }
        }


    }
}
