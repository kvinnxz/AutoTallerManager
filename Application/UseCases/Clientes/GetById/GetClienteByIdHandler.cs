using Application.Abstractions;
using Application.Common;
using Application.UseCases.Clientes.GetAll;
using MediatR;

namespace Application.UseCases.Clientes.GetById;

public sealed class GetClienteByIdHandler(IUnitOfWork uow)
    : IRequestHandler<GetClienteByIdQuery, Result<ClienteResult>>
{
    public async Task<Result<ClienteResult>> Handle(GetClienteByIdQuery qry, CancellationToken ct)
    {
        var c = await uow.Clientes.FirstOrDefaultAsync(
            x => x.Id == qry.Id && x.IsActive, ct, "Vehiculos");
        if (c is null) return Result<ClienteResult>.Failure("Cliente no encontrado.");

        return Result<ClienteResult>.Success(new ClienteResult(
            c.Id, c.Nombre, c.TelefonoValue, c.CorreoValue, c.CreatedAt, c.Vehiculos.Count));
    }
}
