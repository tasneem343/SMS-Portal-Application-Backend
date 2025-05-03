using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SMSPortal.Application.Services.GenerateJwtToken;
using SMSPortal.Domain.Enitites.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class GenerateJwtToken : IGenerateJwtToken
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<GenerateJwtToken> _logger;

    public GenerateJwtToken(UserManager<ApplicationUser> userManager, IConfiguration configuration, ILogger<GenerateJwtToken> logger)
    {
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        _logger.LogInformation("JWT token generated for user {Email}.", user.Email);
        _logger.LogInformation("Token: {Token}", token.ValidTo);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
