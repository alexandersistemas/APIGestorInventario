using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestorInventario.BLL.Servicios.Contrato;
using GestorInventario.DTO;
using GestorInventario.API.Utilidad;
using GestorInventario.DAL.DBContext;

namespace GestorInventario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly GestorInventarioContext _dbContext; 
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(GestorInventarioContext dbContext, ILogger<ProductosController> logger)
        {
            //_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            //_logger = logger;

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;

            _logger.LogInformation("DbContext ha sido inyectado correctamente.");


        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {

            try
            {
                // Verifica la conexión a la base de datos primero
                if (!_dbContext.Database.CanConnect())
                {
                    return StatusCode(500, "No se puede conectar a la base de datos.");
                }

                // Ahora accede a las propiedades
                var model = _dbContext.Model;  // Esto debería funcionar si la conexión es exitosa

                Console.WriteLine("Modelo accesible");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error general: {ex.Message}");
            }

            return Ok("Conexión exitosa.");
        }


        //[HttpGet("test-connection")]
        //public IActionResult TestConnection()
        //{


        //    try
        //    {
        //        Console.WriteLine("Intentando acceder al modelo...");
        //        var model = _dbContext.Model;  // O el acceso al ChangeTracker
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return StatusCode(500, $"Error al acceder al modelo del DbContext: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error general: {ex.Message}");
        //    }



        //    if (_dbContext == null)
        //    {
        //        return StatusCode(500, "DbContext no está inyectado correctamente.");
        //    }

        //    try
        //    {
        //        Console.WriteLine("Intentando conectar a la base de datos...");
        //        var canConnect = _dbContext.Database.CanConnect();
        //        return Ok(canConnect ? "Conexión exitosa a la base de datos." : "No se puede conectar a la base de datos.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al probar la conexión: {ex.Message}");
        //    }
        //}
    }
}
