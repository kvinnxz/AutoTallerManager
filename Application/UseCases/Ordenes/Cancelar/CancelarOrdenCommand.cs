using Application.Common;
using MediatR;
namespace Application.UseCases.Ordenes.Cancelar;
public record CancelarOrdenCommand(int OrdenId) : IRequest<Result>;
