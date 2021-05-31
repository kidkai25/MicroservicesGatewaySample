using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Productmicroservice.Utilities;
using productmicroservice.Database;
using productmicroservice.Entities;
using productmicroservice.Model;

namespace Productmicroservice.Service
{
    public class ProductService : IProductservice
    {
        private readonly DataBaseContext _context;
        private readonly IErrorHelper _errorHelper;
        public ProductService(DataBaseContext context, IErrorHelper errorHelper)
        {
            _context = context;
            _errorHelper = errorHelper;
        }

        public async Task<productmicroservice.Model.Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<productmicroservice.Model.Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<productmicroservice.Model.Product> PostProduct(ProductRequestDto productRequestDto)
        {
            productmicroservice.Model.Product product = new productmicroservice.Model.Product();
            product.ProductId = productRequestDto.Id;
            product.ProductName = productRequestDto.Name;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            //get Product by id
            return await GetLastestProduct();
        }

        public async Task UpdateProduct(int id, ProductRequestDto productRequestDto)
        {
            productmicroservice.Model.Product product = new productmicroservice.Model.Product();
            product.ProductId = productRequestDto.Id;
            product.ProductName = productRequestDto.Name;
            if (id != product.ProductId)
            {
                //return BadRequest();
                _errorHelper.HandleError("Bad Request");
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await ProductExists(id)))
                {
                    _errorHelper.HandleError("Product not found", System.Net.HttpStatusCode.NotFound);
                    //return NotFound();
                }
                else
                {
                    throw ;
                }
            }
        }

        public async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(e => e.ProductId == id);
        }
        public async Task<productmicroservice.Model.Product> GetLastestProduct()
        {
            return await _context.Products.OrderByDescending(x => x.ProductId).LastOrDefaultAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var Product = await _context.Products.FindAsync(id);
            if (Product == null)
            {
                //return NotFound();
                _errorHelper.HandleError("Product Not Found", System.Net.HttpStatusCode.NotFound);
            }

            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();
        }
    }
}
