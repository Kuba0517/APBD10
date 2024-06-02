using APBD10.Contexts;
using APBD10.DTOs;
using APBD10.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Services;

public interface IProductService
{
    Task<bool> CreateProduct(CreateProductDTO createProductDto);
}

public class ProductService(DatabaseContext db) : IProductService
{
    public async Task<bool> CreateProduct(CreateProductDTO createProductDto)
    {
        try
        {
            var product = new Product()
            {
                Name = createProductDto.ProductName,
                Weight = createProductDto.ProductWeight,
                Width = createProductDto.ProductWidth,
                Height = createProductDto.ProductHeight,
                Depth = createProductDto.ProductDepth,
                ProductsCategories = createProductDto.ProductCategories.Select(id =>
                    new ProductCategory()
                    {
                        CategoryId = id
                    }
                ).ToList()
            };

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}