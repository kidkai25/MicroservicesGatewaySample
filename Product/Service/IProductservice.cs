using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using productmicroservice.Model;
using productmicroservice.Entities;

namespace Productmicroservice.Service
{
    public interface IProductservice
    {
        Task<IEnumerable<productmicroservice.Model.Product>> GetProducts();
        Task<productmicroservice.Model.Product> GetProduct(int id);
        Task UpdateProduct(int id, ProductRequestDto productRequestDto);
        Task<productmicroservice.Model.Product> PostProduct(ProductRequestDto productRequestDto);
        Task<bool> ProductExists(int id);
        Task DeleteProduct(int id);
    }
}
