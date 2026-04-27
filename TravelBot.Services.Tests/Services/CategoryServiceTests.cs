using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using TravelBot.Common.Contracts;
using TravelBot.Context.Tests;
using TravelBot.Entities;
using TravelBot.Repositories.ReadRepositories;
using TravelBot.Repositories.WriteRepositories;
using TravelBot.Services.Contracts.Exceptions;
using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Services.Crud;
using TravelBot.Services.Infrastructure;
using TravelBot.Services.Services.Crud;

namespace TravelBot.Services.Tests.Services;

/// <summary>
///     Тесты для <see cref="CategoryService" />
/// </summary>
public class CategoryServiceTests : TravelBotContextInMemory
{
    private readonly ICategoryService categoryService;

    /// <summary>
    ///     ctor
    /// </summary>
    public CategoryServiceTests()
    {
        var config = new MapperConfiguration(options => { options.AddProfile<ServiceProfile>(); });

        var mapper = config.CreateMapper();
        categoryService = new CategoryService(
            new CategoryReadRepository(Context),
            new CategoryWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
            UnitOfWork,
            mapper);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllCategories()
    {
        // Arrange
        Context.Set<Category>().AddRange(
            new Category { Id = Guid.NewGuid(), Name = "Museum" },
            new Category { Id = Guid.NewGuid(), Name = "Park" });
        await Context.SaveChangesAsync();
        ClearTracking();

        // Act
        var result = await categoryService.GetAll(CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
        result.Select(x => x.Name).Should().BeEquivalentTo("Museum", "Park");
    }

    [Fact]
    public async Task GetById_ShouldReturnCategory_WhenCategoryExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        Context.Set<Category>().Add(new Category { Id = id, Name = "Cafe" });
        await Context.SaveChangesAsync();
        ClearTracking();

        // Act
        var result = await categoryService.GetById(id, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(id);
        result.Name.Should().Be("Cafe");
    }

    [Fact]
    public async Task GetById_ShouldThrowNotFound_WhenCategoryDoesNotExist()
    {
        // Act
        var act = () => categoryService.GetById(Guid.NewGuid(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<TravelBotNotFoundException>();
    }

    [Fact]
    public async Task Create_ShouldAddCategoryAndReturnCreatedModel()
    {
        // Arrange
        var model = new CategoryCreateModel
        {
            Name = "Hotel"
        };

        // Act
        var result = await categoryService.Create(model, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Hotel");

        var saved = await Context.Set<Category>().FirstOrDefaultAsync(x => x.Name == "Hotel");
        saved.Should().NotBeNull();
        saved.Name.Should().Be("Hotel");
    }

    [Fact]
    public async Task Edit_ShouldUpdateCategoryAndReturnUpdatedModel()
    {
        // Arrange
        var id = Guid.NewGuid();
        Context.Set<Category>().Add(new Category { Id = id, Name = "Old name" });
        await Context.SaveChangesAsync();
        ClearTracking();

        var model = new CategoryCreateModel
        {
            Name = "New name"
        };

        // Act
        var result = await categoryService.Edit(id, model, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(id);
        result.Name.Should().Be("New name");

        var updated = await Context.Set<Category>().FirstAsync(x => x.Id == id);
        updated.Name.Should().Be("New name");
    }

    [Fact]
    public async Task Edit_ShouldThrowNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        var model = new CategoryCreateModel
        {
            Name = "Anything"
        };

        // Act
        var act = () => categoryService.Edit(Guid.NewGuid(), model, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<TravelBotNotFoundException>();
    }

    [Fact]
    public async Task Delete_ShouldRemoveCategory_WhenCategoryExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        Context.Set<Category>().Add(new Category { Id = id, Name = "To delete" });
        await Context.SaveChangesAsync();
        ClearTracking();

        // Act
        await categoryService.Delete(id, CancellationToken.None);

        // Assert
        var deleted = await Context.Set<Category>().FirstOrDefaultAsync(x => x.Id == id);
        deleted!.DeletedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task Delete_ShouldThrowNotFound_WhenCategoryDoesNotExist()
    {
        // Act
        var act = () => categoryService.Delete(Guid.NewGuid(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<TravelBotNotFoundException>();
    }
}