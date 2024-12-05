using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorInventario.DTO;
using GestorInventario.Model;

namespace GestorInventario.BLL.Servicios.Contrato
{
    public interface IEntradaService
    {
        Task<List<EntradasInventarioDTO>> lista();
        Task<EntradasInventarioDTO> Crear(EntradasInventarioDTO modelo);
        Task<bool> Editar(EntradasInventarioDTO modelo);
        Task<bool> Eliminar(int id);
        Task<List<EntradasInventarioDTO>> Obtener(int id);

    }
}
