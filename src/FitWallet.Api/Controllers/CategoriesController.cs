using AutoMapper;
using FitWallet.Core.Dtos;
using FitWallet.Core.Models;
using FitWallet.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitWallet.Api.Controllers;

[Authorize]
public class CategoriesController : ApplicationControllerBase
{
    private readonly ApplicationDatabaseContext _dbContext;

    public CategoriesController(
        ILogger<CategoriesController> logger, 
        IMapper mapper,
        ApplicationDatabaseContext dbContext) : base(logger, mapper)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<Ok<IEnumerable<CategoryDto>>> GetCategories()
    {
        var userId = GetUserId();

        var categories = await _dbContext.Categories
            .AsNoTracking()
            .Where(e => e.UserId == userId)
            .ToListAsync();

        var categoriesDtos = Mapper.Map<List<Category>, IEnumerable<CategoryDto>>(categories);

        return TypedResults.Ok(categoriesDtos);
    }

    [HttpGet("{id}")]
    public async Task<Results<NotFound, Ok<CategoryDto>>> GetCategory(string id)
    {
        var userId = GetUserId();

        var category = await _dbContext.Categories
            .AsNoTracking()
            .Where(e => e.UserId == userId && e.Id == id)
            .FirstOrDefaultAsync();

        if (category is null)
        {
            return TypedResults.NotFound();
        }

        var categoryDto = Mapper.Map<Category, CategoryDto>(category);

        return TypedResults.Ok(categoryDto);
    }

    [HttpPost]
    public async Task<Results<Conflict<string>,Ok<string>>> AddCategory([FromBody] CategoryDto request)
    {
        var userId = GetUserId();

        if (await _dbContext.Categories.AnyAsync(e => e.Name == request.Name && e.UserId == userId))
        {
            return TypedResults.Conflict("There is already category with this name");
        }

        var category = new Category
        {
            UserId = userId
        };

        Mapper.Map(request, category);

        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync();

        return TypedResults.Ok(category.Id);
    }
    [HttpPut("{id}")]
    public async Task<Results<NotFound, NoContent, Conflict<string>>> UpdateCategory(string id, [FromBody] CategoryDto request)
    {
        var userId = GetUserId();

        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(e => 
                e.Id == id && 
                e.UserId == userId);

        if (category is null)
        {
            return TypedResults.NotFound();
        }

        if (await _dbContext.Wallets.AnyAsync(e => e.Name == request.Name && e.UserId == userId))
        {
            return TypedResults.Conflict("There is already category with this name");
        }

        Mapper.Map(request, category);

        await _dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<Results<NotFound, NoContent>> DeleteCategory(string id)
    {
        var userId = GetUserId();

        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(e => 
                e.Id == id && 
                e.UserId == userId);

        if (category is null)
        {
            return TypedResults.NotFound();
        }

        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }

}