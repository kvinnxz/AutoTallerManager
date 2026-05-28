using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Ordenes.Update;

public record UpdateOrdenCommand(
    int Id, EstadoOrden? Estado,
    string? TrabajoRealizado,
    decimal? CostoManoObra,
    DateTime? FechaEstimadaEntrega)
    : IRequest<Result>;
