using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FitWallet.Api.Configuration;
using FitWallet.Core.Dtos.Users;
using FitWallet.Core.Models;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FitWallet.Api.Controllers;

public class IdentityController : ApplicationControllerBase
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityController(
        //ILogger logger,
        IMapper mapper,
        JwtSettings jwtSettings, 
        UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager) : base(null, mapper)
    {
        _jwtSettings = jwtSettings;
        _userManager = userManager;
        _roleManager = roleManager;
    }


    [HttpPost("login")]
    public async Task<Results<Ok<AccessTokenResponse>, UnauthorizedHttpResult>> Login([FromBody] LoginRequestDto request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user is null || 
            await _userManager.CheckPasswordAsync(user, request.Password) == false)
        {
            return TypedResults.Unauthorized();
        }

        var token = await GenerateJwtToken(user);

        return TypedResults.Ok(new AccessTokenResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresIn = (long)token.ValidTo.Subtract(DateTime.Now).TotalSeconds,
            RefreshToken = "c"// TODO: Add token
        });
    }

    [HttpPost("register")]
    public async Task<Results<Ok<AccessTokenResponse>, ValidationProblem>> Register([FromBody] RegisterRequestDto request)
    {
        var user = Mapper.Map<RegisterRequestDto, User>(request);

        var createResult = await _userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            return CreateValidationProblem(createResult);
        }

        // Add to roles
        
        var token = await GenerateJwtToken(user);

        return TypedResults.Ok(new AccessTokenResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresIn = (long)token.ValidTo.Subtract(DateTime.Now).TotalSeconds,
            RefreshToken = "c"// TODO: Add token
        });
    }

    // TODO: Add token refresh


    private static ValidationProblem CreateValidationProblem(IdentityResult result)
    {
        var errorsDictionary = result.Errors
            .GroupBy(error => error.Code)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.Description).ToArray());

        return TypedResults.ValidationProblem(errorsDictionary);
    }


    // TODO: Jwt token service can be used

    private async Task<List<Claim>> GetUserClaims(User user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
            
        var userRolesClaims = userRoles.Select(userRole =>
            new Claim(ClaimTypes.Role, userRole));

        return new List<Claim>(userRolesClaims)
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
    }
        
    private async Task<JwtSecurityToken> GenerateJwtToken(User user)
    {
        var userClaims = await GetUserClaims(user);

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: userClaims,
            expires: DateTime.Now.AddSeconds(_jwtSettings.ExpirationSeconds),
            signingCredentials: signingCredentials
        );

        return token;
    }
}