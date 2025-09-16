using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using Moq;
using Xunit;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ICategoryService _service;

    public CategoryServiceTests()
    {
        _repositoryMock = new Mock<ICategoryRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new CategoryService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Create_ShouldCreateCategoryAndReturnDto()
    {
        // Arrange
        string title = "Test Category";
        var createDto = new CategoryCreateDto(title);
        var dto = new CategoryDto(1, title);
        var category = new Category { Id = 1, Products = new List<Product>(), Title = title };

        _mapperMock.Setup(m => m.Map<Category>(createDto)).Returns(category);
        _repositoryMock.Setup(r => r.AddAsync(category)).ReturnsAsync(category);
        _mapperMock.Setup(m => m.Map<CategoryDto>(category)).Returns(dto);

        // Act
        var result = await _service.CreateAsync(createDto);
        
        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.Is<Category>(c => c.Title == title)), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(createDto.Title, result.Title);
    }
    
    [Fact]
    public async Task Create_GetById_ShouldCallRepositoryAndReturnDto()
    {
        // Arrange
        int id = 1;
        string title = "Test Category";
        var dto = new CategoryDto(id, title);
        var category = new Category { Id = id, Products = new List<Product>(), Title = title };

        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(category);
        _mapperMock.Setup(m => m.Map<CategoryDto>(category)).Returns(dto);

        // Act
        var result = await _service.GetByIdAsync(id);
        
        // Assert
        _repositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(dto.Title, result.Title);
        Assert.Equal(dto.Id, result.Id);
    }
}