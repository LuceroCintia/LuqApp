using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taller.Application.DTOs;
using Taller.Application.Siniestros;

namespace Taller.API.Controllers;

[ApiController]
[Route("api/siniestros")]
[Authorize]
public sealed class SiniestrosController : ControllerBase
{
    private readonly CrearSiniestroUseCase _crearUseCase;
    private readonly ObtenerSiniestroUseCase _obtenerUseCase;

    public SiniestrosController(CrearSiniestroUseCase crearUseCase, ObtenerSiniestroUseCase obtenerUseCase)
    {
        _crearUseCase = crearUseCase;
        _obtenerUseCase = obtenerUseCase;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Recepcionista,JefeTaller")]
    public async Task<ActionResult<SiniestroDto>> Crear([FromBody] CrearSiniestroRequest request, CancellationToken cancellationToken)
    {
        var userId = 1;
        var creado = await _crearUseCase.ExecuteAsync(request, userId, cancellationToken);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Recepcionista,JefeTaller,Operario")]
    public async Task<ActionResult<SiniestroDto>> ObtenerPorId(int id, CancellationToken cancellationToken)
    {
        var siniestro = await _obtenerUseCase.ExecuteAsync(id, cancellationToken);
        if (siniestro is null) return NotFound();
        return Ok(siniestro);
    }
}
