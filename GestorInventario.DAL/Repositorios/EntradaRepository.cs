using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorInventario.DAL.DBContext;
using GestorInventario.DAL.Repositorios.Contrato;
using GestorInventario.Model;
using Microsoft.EntityFrameworkCore;



namespace GestorInventario.DAL.Repositorios
{
    public class EntradaRepository : GenericRepository<EntradasInventario>, IEntradaRepository
    {
        private readonly GestorInventarioContext _dbcontext;

        public EntradaRepository(GestorInventarioContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<EntradasInventario> Registrar(EntradasInventario modelo)
        {
            EntradasInventario entradaGenerada = new EntradasInventario();

            using (var transaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {

                    await _dbcontext.SaveChangesAsync();
                    entradaGenerada = modelo;
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                return entradaGenerada;

            }
        }
    }
}
