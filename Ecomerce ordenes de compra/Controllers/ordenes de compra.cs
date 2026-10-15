using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecomerce_ordenes_de_compra.Controllers
{
    // Modelos de datos para la Orden y sus Productos
    public class Producto
    {
        public string ProductoId { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class Orden
    {
        public int Id { get; set; }
        public string ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<Producto> Productos { get; set; }
    }

    [Route("api/ordenes")]
    [ApiController]
    public class ordenes_de_compra : ControllerBase
    {
        // Simulación de una base de datos en memoria para el ejercicio
        private static List<Orden> listaOrdenes = new List<Orden>
        {
            new Orden
            {
                Id = 1,
                ClienteId = "CUST-1024",
                Fecha = DateTime.UtcNow,
                Total = 150.50m,
                Productos = new List<Producto>
                {
                    new Producto { ProductoId = "P001", Nombre = "Teclado Mecánico", Cantidad = 1, PrecioUnitario = 120.00m },
                    new Producto { ProductoId = "P002", Nombre = "Mousepad RGB", Cantidad = 1, PrecioUnitario = 30.50m }
                }
            }
        };

        // a) Consultar todas las órdenes registradas en el sistema (GET: /api/ordenes)
        [HttpGet]
        public IActionResult ObtenerOrdenes()
        {
            return Ok(listaOrdenes);
        }

        // b) Crear una nueva orden de compra (POST: /api/ordenes)
        [HttpPost]
        public IActionResult CrearOrden([FromBody] Orden nuevaOrden)
        {
            if (nuevaOrden == null)
            {
                return BadRequest("La orden no puede estar vacía.");
            }

            listaOrdenes.Add(nuevaOrden);
            return CreatedAtAction(nameof(ObtenerOrdenes), new { id = nuevaOrden.Id }, nuevaOrden);
        }

        // c) Cancelar/Eliminar la orden con el ID 5 (DELETE: /api/ordenes/5)
        [HttpDelete("{id}")]
        public IActionResult EliminarOrden(int id)
        {
            var orden = listaOrdenes.FirstOrDefault(o => o.Id == id);
            if (orden == null)
            {
                return NotFound($"No se encontró la orden con el ID {id}.");
            }

            listaOrdenes.Remove(orden);
            return Ok(new { mensaje = $"La orden con ID {id} ha sido eliminada/cancelada exitosamente." });
        }
    }
}