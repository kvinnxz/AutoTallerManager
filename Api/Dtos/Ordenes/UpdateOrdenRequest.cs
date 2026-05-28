using Domain.Enums;
namespace Api.Dtos.Ordenes;
public record UpdateOrdenRequest(
    EstadoOrden? Estado, string? TrabajoRealizado,
    decimal? CostoManoObra, DateTime? FechaEstimadaEntrega);
