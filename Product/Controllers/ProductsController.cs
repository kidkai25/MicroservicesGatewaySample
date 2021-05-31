using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productmicroservice.Service;
using productmicroservice.Entities;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductservice _productservice;

        public ProductsController(IProductservice productservice)
        {
            _productservice = productservice;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<productmicroservice.Model.Product>>> GetProducts()
        {
            return Ok(await _productservice.GetProducts());
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<productmicroservice.Model.Product>> GetProduct(int id)
        {
            var product = await _productservice.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductRequestDto productRequestDto)
        {
            await _productservice.UpdateProduct(id, productRequestDto);
            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<productmicroservice.Model.Product>> PostProduct(ProductRequestDto productRequestDto)
        {
           var product = await _productservice.PostProduct(productRequestDto);

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productservice.DeleteProduct(id);

            return NoContent();
        }
    }
}
