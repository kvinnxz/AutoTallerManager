using Application.Common;
using MediatR;

namespace Application.UseCases.Ordenes.AgregarRepuesto;

public record AgregarRepuestoCommand(int OrdenId, int RepuestoId, int Cantidad) : IRequest<Result>;
