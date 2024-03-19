using AutoMapper;
using FitWallet.Database;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitWallet.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class ApplicationControllerBase(
	ILogger logger, 
	IMapper mapper, 
	ApplicationDatabaseContext dbContext) : ControllerBase
{
	protected readonly ILogger _logger = logger;
	protected readonly IMapper _mapper = mapper;
	protected readonly ApplicationDatabaseContext _dbContext = dbContext;

	protected string GetUserId()
	{
		return User.FindFirstValue(ClaimTypes.NameIdentifier);
	}
}