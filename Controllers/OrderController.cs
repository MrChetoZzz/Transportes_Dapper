using Microsoft.AspNetCore.Mvc;
using TransportOrders.Api.Models;
using TransportOrders.Api.Services;

namespace TransportOrders.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ITransportOrderService _orderService;

    public OrderController(ITransportOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderService.ListAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _orderService.FindAsync(id);
        if (order == null)
            return NotFound();
            
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TransportOrder order)
    {
        var success = await _orderService.CreateAsync(order);
        if (!success)
            return BadRequest("No se pudo crear la orden.");
            
        return Ok("Orden creada exitosamente.");
    }

[HttpPut("{id}")]
public async Task<IActionResult> Update(int id, [FromBody] TransportOrder order)
{
    // Si tu modelo usa 'init', puedes verificar que coincidan los IDs de otra forma o actualizar el modelo a { get; set; }
    var success = await _orderService.UpdateAsync(order);
    if (!success)
        return BadRequest("No se pudo actualizar la orden.");
        
    return Ok("Orden actualizada exitosamente.");
}

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _orderService.DeleteAsync(id);
        if (!success)
            return BadRequest("No se pudo eliminar la orden.");
            
        return Ok("Orden eliminada exitosamente.");
    }
}