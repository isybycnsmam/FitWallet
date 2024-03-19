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
public class CategoriesController(
		ILogger<CategoriesController> logger,
		IMapper mapper,
		ApplicationDatabaseContext dbContext) : ApplicationControllerBase(logger, mapper, dbContext)
{
	/// <summary>
	/// Gets all categories for the current user.
	/// </summary>
	/// <returns>A list of category DTOs.</returns>
	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
	public async Task<Ok<IEnumerable<CategoryDto>>> GetAllCategories()
	{
		var userId = GetUserId();

		var categories = await _dbContext.Categories
			.AsNoTracking()
			.Where(e => e.UserId == userId)
			.OrderBy(e => e.Name)
			.ToListAsync();

		var categoriesDtos = _mapper.Map<List<Category>, IEnumerable<CategoryDto>>(categories);

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
			return TypedResults.NotFound();

		var categoryDto = _mapper.Map<Category, CategoryDto>(category);

		return TypedResults.Ok(categoryDto);
	}

	[HttpPost]
	public async Task<Results<Conflict<string>, Ok<string>>> AddCategory([FromBody] CategoryDto request)
	{
		var userId = GetUserId();

		if (await IsCategoryNameAlreadyTaken(request.Name, userId))
			return TypedResults.Conflict("There is already category with this name");

		var category = new Category { UserId = userId };

		_mapper.Map(request, category);

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
			return TypedResults.NotFound();

		if (category.Name != request.Name &&
			await IsCategoryNameAlreadyTaken(request.Name, userId))
		{
			return TypedResults.Conflict("There is already category with this name");
		}

		_mapper.Map(request, category);

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
			return TypedResults.NotFound();

		_dbContext.Categories.Remove(category);
		await _dbContext.SaveChangesAsync();

		return TypedResults.NoContent();
	}

	private async Task<bool> IsCategoryNameAlreadyTaken(string name, string userId)
	{
		var exists = await _dbContext.Categories
			.AsNoTracking()
			.Where(e => e.UserId == userId && e.Name == name)
			.AnyAsync();

		return exists;
	}
}