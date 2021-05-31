using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ordermicroservice.Service;
using ordermicroservice.Database;
using ordermicroservice.Model;
using ordermicroservice.Entities;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderservice _orderservice;

        public OrdersController(IOrderservice orderservice)
        {
            _orderservice = orderservice;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ordermicroservice.Model.Order>>> GetOrders()
        {
            return Ok(await _orderservice.GetOrders());
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ordermicroservice.Model.Order>> GetOrder(int id)
        {
            var order = await _orderservice.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderUpdateRequestDto orderUpdateRequestDto)
        {
            await _orderservice.UpdateOrder(id, orderUpdateRequestDto);
            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ordermicroservice.Model.Order>> PostOrder(OrderRequestDto orderRequestDto)
        {
            var order = await _orderservice.PostOrder(orderRequestDto);

            return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderservice.DeleteOrder(id);

            return NoContent();
        }

    }
}
