using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorInventario.DTO;
using GestorInventario.Model;

namespace GestorInventario.BLL.Servicios.Contrato
{
    public interface ISalidaService
    {
        Task<List<SalidasInventarioDTO>> lista();
        Task<SalidasInventarioDTO> Crear(SalidasInventarioDTO modelo);
        Task<bool> Editar(SalidasInventarioDTO modelo);
        Task<bool> Eliminar(int id);
        Task<List<SalidasInventarioDTO>> Obtener(int id);

    }
}
