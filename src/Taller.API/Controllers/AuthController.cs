using Microsoft.AspNetCore.Mvc;
using Taller.API.Models;
using Taller.Domain.Entities;
using Taller.Domain.Enums;
using Taller.Infrastructure.Security;

namespace Taller.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(JwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Demo bootstrap: reemplazar por validación real con usuario persistido + bcrypt.
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Credenciales inválidas.");

        var role = request.Username.ToLowerInvariant() switch
        {
            "admin" => RolUsuario.Admin,
            "jefe" => RolUsuario.JefeTaller,
            "operario" => RolUsuario.Operario,
            _ => RolUsuario.Recepcionista
        };

        var user = new Usuario
        {
            Id = 1,
            Username = request.Username,
            Rol = role,
            SectorId = role == RolUsuario.Operario ? 1 : null
        };

        var token = _jwtTokenService.CreateToken(user);
        return Ok(new { access_token = token, role = role.ToString() });
    }
}
