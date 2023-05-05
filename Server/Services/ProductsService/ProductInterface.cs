using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace NorthwindBlazor.Services.NorthwindService
{
    public interface ProductInterface
    {
        Task<ServiceResponse<ProductDTO>> GetProduct(int id);

        Task<ServiceResponse<List<Product>>> GetProducts();

        Task<ServiceResponse<ProductDTO>> PostProduct(ProductDTO prod);

        Task<ServiceResponse<Product>> PutProduct(Product prod);

        Task<ServiceResponse<ProductDTO>> DeleteProduct(int id);

    }
}
