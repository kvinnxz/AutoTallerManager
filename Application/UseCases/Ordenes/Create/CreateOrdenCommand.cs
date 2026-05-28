using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Ordenes.Create;

public record CreateOrdenCommand(
    int VehiculoId, int MecanicoId,
    TipoServicio TipoServicio,
    string Descripcion, decimal CostoManoObra)
    : IRequest<Result<int>>;
