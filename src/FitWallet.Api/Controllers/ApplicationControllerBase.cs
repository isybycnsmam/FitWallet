using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitWallet.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class ApplicationControllerBase : ControllerBase
{
    protected readonly ILogger Logger;
    protected readonly IMapper Mapper;

    protected ApplicationControllerBase(ILogger logger, IMapper mapper)
    {
        Logger = logger;
        Mapper = mapper;
    }

    protected string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}