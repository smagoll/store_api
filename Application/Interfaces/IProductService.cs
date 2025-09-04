using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IProductService
{
    Task<ProductDto> Create(CreateProductDto dto);
    Task<ProductDto> Delete(DeleteProductDto dto);
    Task<ProductDto> Update(UpdateProductDto dto);
    Task<ProductDto?> GetById(int id);
    Task<IEnumerable<ProductDto>> GetAll();
}