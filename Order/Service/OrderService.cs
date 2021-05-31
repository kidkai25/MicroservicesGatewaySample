using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ordermicroservice.Database;
using ordermicroservice.Model;
using ordermicroservice.Utilities;
using ordermicroservice.Entities;

namespace ordermicroservice.Service
{
    public class OrderService : IOrderservice
    {
        private readonly DataBaseContext _context;
        private readonly IErrorHelper _errorHelper;
        public OrderService(DataBaseContext context, IErrorHelper errorHelper)
        {
            _context = context;
            _errorHelper = errorHelper;
        }

        public async Task<ordermicroservice.Model.Order> GetOrder(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<ordermicroservice.Model.Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<ordermicroservice.Model.Order> PostOrder(OrderRequestDto orderRequestDto)
        {
            var order = new ordermicroservice.Model.Order();
            order.ProductID = orderRequestDto.ProductID;
            order.UserID = orderRequestDto.UserID;
            order.OccuredAt = orderRequestDto.OccuredAt;
            order.PriceAtPointInTime = orderRequestDto.PriceAtPointInTime;
            order.Quantity = orderRequestDto.Quantity;
            order.Total = orderRequestDto.Total;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            //get user by id
            return await GetLastestOrder();
        }

        public async Task UpdateOrder(int id, OrderUpdateRequestDto orderUpdateRequestDto)
        {
            var order = new ordermicroservice.Model.Order();
            order.OrderID = orderUpdateRequestDto.Id;
            order.ProductID = orderUpdateRequestDto.ProductID;
            order.UserID = orderUpdateRequestDto.UserID;
            order.OccuredAt = orderUpdateRequestDto.OccuredAt;
            order.PriceAtPointInTime = orderUpdateRequestDto.PriceAtPointInTime;
            order.Quantity = orderUpdateRequestDto.Quantity;
            order.Total = orderUpdateRequestDto.Total;
            if (id != orderUpdateRequestDto.Id)
            {
                //return BadRequest();
                _errorHelper.HandleError("Bad Request");
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await OrderExists(id)))
                {
                    _errorHelper.HandleError("Order not found", System.Net.HttpStatusCode.NotFound);
                    //return NotFound();
                }
                else
                {
                    throw ;
                }
            }
        }

        public async Task<bool> OrderExists(int id)
        {
            return await _context.Orders.AnyAsync(e => e.OrderID == id);
        }
        public async Task<ordermicroservice.Model.Order> GetLastestOrder()
        {
            return await _context.Orders.OrderByDescending(x => x.OrderID).LastOrDefaultAsync();
        }

        public async Task DeleteOrder(int id)
        {
            var user = await _context.Orders.FindAsync(id);
            if (user == null)
            {
                //return NotFound();
                _errorHelper.HandleError("Order Not Found", System.Net.HttpStatusCode.NotFound);
            }

            _context.Orders.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
