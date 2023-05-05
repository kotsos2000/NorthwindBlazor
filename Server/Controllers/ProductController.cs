using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NorthwindBlazor.Server.Controllers

{
    [ApiController]
    // initialize DbContext and Mapper
    public class ProductsController : ControllerBase
    {
        private readonly NorthwindContext _context;

        private readonly IMapper _mapper;

        private readonly ProductInterface productInterface;


        public ProductsController(NorthwindContext context,IMapper mapper, ProductInterface productInterface)
        {
            _context = context;
            _mapper = mapper;
            this.productInterface = productInterface;
        }

        [HttpGet("/api/products/productid/{id}")] // GET A Single Product using KEY id
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var response = await productInterface.GetProduct(id);

            if (response.Success)
            {
                return Ok(response.Data);
            }

            return NotFound(response.Message);
        }

        [HttpGet("/api/products")] // GET All Products
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var response = await productInterface.GetProducts();
            return response.Data;
        }

        [HttpPost("/api/products")] // POST Product

        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO prod)
        {
            try
            {
                var response = await productInterface.PostProduct(prod);
                return Ok(response.Data);
            }
            catch
            {
                var response = new ServiceResponse<ProductDTO>();
                response.Message = "Failed to create product";
                response.Success = false;
                return BadRequest(response);
            }
        }
            
        [HttpPut("/api/products")] // PUT Product using the PUT DTO that has the ProductId field
        public async Task<ActionResult<Product>> PutProduct(Product prod)
        {
            var result = await productInterface.PutProduct(prod);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("/api/products/productid/{id}")] // DELETE A Single Product using KEY id
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var response = await productInterface.DeleteProduct(id);

            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }

    }

}