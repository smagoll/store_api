using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    
    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ProductDto> Create(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        var created = await _repository.AddAsync(product);
        return _mapper.Map<ProductDto>(created);
    }

    public async Task Delete(DeleteProductDto dto)
    {
        var product = await _repository.GetByIdAsync(dto.Id);
        await _repository.DeleteAsync(product);
    }

    public async Task<ProductDto> Update(UpdateProductDto dto)
    {
        var product = await _repository.GetByIdAsync(dto.Id);
        
        product.Name = dto.Name;
        product.Price = dto.Price;
        product.CategoryId = dto.CategoryId;

        await _repository.UpdateAsync(product);

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto?> GetById(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<ProductDto>> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}