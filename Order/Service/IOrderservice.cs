using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ordermicroservice.Model;
using ordermicroservice.Entities;

namespace ordermicroservice.Service
{
    public interface IOrderservice
    {
        Task<IEnumerable<ordermicroservice.Model.Order>> GetOrders();
        Task<ordermicroservice.Model.Order> GetOrder(int id);
        Task UpdateOrder(int id, OrderUpdateRequestDto orderUpdateRequestDto);
        Task<ordermicroservice.Model.Order> PostOrder(OrderRequestDto orderRequestDto);
        Task<bool> OrderExists(int id);
        Task DeleteOrder(int id);
    }
}
