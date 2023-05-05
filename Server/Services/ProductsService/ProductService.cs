using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace NorthwindBlazor.Services.NorthwindService
{
    public class ProductService : ProductInterface
    {
        private readonly NorthwindContext _context;

        private readonly IMapper _mapper;

        public ProductService(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ProductDTO>> DeleteProduct(int id)
        {
            var response = new ServiceResponse<ProductDTO>();

            var result = await _context.Products.FindAsync(id);

            if (result is not null)
            {
                _context.Products.Remove(result);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<ProductDTO>(result);
                return response;
            }
            else
            {
                response.Success = false;
                response.Message = $"Product with id {id} not found.";
                return response;
            }
        }

        public async Task<ServiceResponse<ProductDTO>> GetProduct(int id)
        {
            var response = new ServiceResponse<ProductDTO>();

            var result = await _context.Products.FindAsync(id);

            if (result is not null)
            {
                response.Data = _mapper.Map<ProductDTO>(result);
                return response;
            }
            else
            {
                response.Success = false;
                response.Message = $"Product with id {id} not found.";
                return response;
            }
        }

        public async Task<ServiceResponse<List<Product>>> GetProducts()
        {
            var response = new ServiceResponse<List<Product>>();

            response.Data = await _context.Products.ToListAsync();
            //response.Data = await _context.Products.Select(p => _mapper.Map<ProductDTO>(p)).ToListAsync();

            return response;
        }

        public async Task<ServiceResponse<ProductDTO>> PostProduct(ProductDTO prod)
        {
            var response = new ServiceResponse<ProductDTO>();

            // we will do a try catch in the controller
            _context.Products.Add(_mapper.Map<Product>(prod));
            await _context.SaveChangesAsync();

            response.Data = prod;
            return response;
        }

        public async Task<ServiceResponse<Product>> PutProduct(Product prod)
        {
            var response = new ServiceResponse<Product>();

            var result = await _context.Products.FindAsync(prod.ProductId);
            if (result != null)
            {
                result.ProductName = prod.ProductName;
                result.SupplierId = prod.SupplierId;
                result.CategoryId = prod.CategoryId;
                result.QuantityPerUnit = prod.QuantityPerUnit;
                result.UnitPrice = prod.UnitPrice;
                result.UnitsInStock = prod.UnitsInStock;
                result.UnitsOnOrder = prod.UnitsOnOrder;
                result.ReorderLevel = prod.ReorderLevel;
                result.Discontinued = prod.Discontinued;

                await _context.SaveChangesAsync();
                response.Data = result;
                return response;
            }
            else
            {
                response.Success = false;
                response.Message = $"Product with id {prod.ProductId} not found.";
                return response;
            }
        }
    }
}
