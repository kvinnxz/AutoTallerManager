using Application.Common;
using MediatR;
namespace Application.UseCases.Vehiculos.Create;
public record CreateVehiculoCommand(
    int ClienteId, string Marca, string Modelo,
    int Anio, string VIN, int Kilometraje) : IRequest<Result<int>>;
