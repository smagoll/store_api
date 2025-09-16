using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace Tests.Unit.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new ProductService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Create_ShouldCallRepositoryAndReturnDto()
    {
        // Arrange
        var createDto = new CreateProductDto("Test", 100, 1);
        var product = new Product { Name = "Test", CategoryId = 1, Price = 100, Id = 1 };
        var productDto = new ProductDto(1, "Test", 100, 1);

        _mapperMock.Setup(m => m.Map<Product>(createDto)).Returns(product);
        _repositoryMock.Setup(r => r.AddAsync(product)).ReturnsAsync(product);
        _mapperMock.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);

        // Act
        var result = await _service.Create(createDto);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(product), Times.Once);
        Assert.Equal(productDto.Id, result.Id);
        Assert.Equal(productDto.Name, result.Name);
    }

    [Fact]
    public async Task Delete_ShouldCallDeleteAsync()
    {
        // Arrange
        var product = new Product { Id = 1 };
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);
        
        // Act
        await _service.Delete(new DeleteProductDto(1));

        // Assert
        _repositoryMock.Verify(r => r.GetByIdAsync(1), Times.Once);
    }
    
    [Fact]
    public async Task Update_ShouldUpdateProductAndReturnDto()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Old", Price = 50, CategoryId = 2 };
        var dto = new UpdateProductDto(1, "New", 200, 3);
        var updatedDto = new ProductDto(1, "New", 200, 3);
        
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);
        _mapperMock.Setup(m => m.Map<ProductDto>(product)).Returns(updatedDto);
        
        // Act
        var result = await _service.Update(dto);

        // Assert
        _repositoryMock.Verify(r => r.UpdateAsync(product), Times.Once);
        Assert.Equal("New", result.Name);
        Assert.Equal(200, result.Price);
    }
    
    [Fact]
    public async Task GetById_ShouldReturnDto()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Old", Price = 50, CategoryId = 2 };
        var dto = new ProductDto(1, "New", 200, 3);
        
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);
        _mapperMock.Setup(m => m.Map<ProductDto>(product)).Returns(dto);
        
        // Act
        var result = await _service.GetById(1);

        // Assert
        _repositoryMock.Verify(r => r.GetByIdAsync(1), Times.Once);
        Assert.Equal(dto.Id, result.Id);
        Assert.Equal(dto.Name, result.Name);
    }
    
    [Fact]
    public async Task GetById_ShouldReturnNull_WhenNotFound()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Product?)null);
        
        // Act
        var result = await _service.GetById(1);

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetAll_ShouldReturnDtos()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "A" },
            new Product { Id = 2, Name = "B" }
        };
        
        var dtos = new List<ProductDto>
        {
            new ProductDto(1, "A", 100, 1),
            new ProductDto(2, "B", 100, 1),
        };
        
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(products);
        _mapperMock.Setup(m => m.Map<IEnumerable<ProductDto>>(products)).Returns(dtos);
        
        // Act
        var result = await _service.GetAll();

        // Assert
        Assert.Equal(2, result.Count());
    }
}