using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorInventario.Model;

namespace GestorInventario.DAL.Repositorios.Contrato
{
    public interface IEntradaRepository : IGenericRepository<EntradasInventario>
    {
        Task<EntradasInventario> Registrar(EntradasInventario modelo);
    }
}
