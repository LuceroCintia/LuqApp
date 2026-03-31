namespace Taller.Application.DTOs;

public sealed class CrearSiniestroRequest
{
    public DateTime FechaIngreso { get; set; } = DateTime.UtcNow;
    public string Descripcion { get; set; } = string.Empty;
    public string Tipo { get; set; } = "Particular";
    public string NroSiniestro { get; set; } = string.Empty;
    public string? Poliza { get; set; }
    public int ClienteId { get; set; }
    public int VehiculoId { get; set; }
}

public sealed class SiniestroDto
{
    public int Id { get; set; }
    public DateTime FechaIngreso { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string NroSiniestro { get; set; } = string.Empty;
    public string? Poliza { get; set; }
    public int ClienteId { get; set; }
    public int VehiculoId { get; set; }
}
