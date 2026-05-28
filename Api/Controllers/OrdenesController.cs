using Api.Dtos.Ordenes;
using Api.Helpers;
using Application.UseCases.Ordenes.AgregarRepuesto;
using Application.UseCases.Ordenes.Cancelar;
using Application.UseCases.Ordenes.Cerrar;
using Application.UseCases.Ordenes.Create;
using Application.UseCases.Ordenes.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/ordenes"), Authorize]
public class OrdenesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrdenRequest req, CancellationToken ct)
    {
        var cmd = new CreateOrdenCommand(req.VehiculoId, req.MecanicoId, req.TipoServicio, req.Descripcion, req.CostoManoObra);
        var result = await mediator.Send(cmd, ct);
        return result.IsSuccess ? Ok(ApiResponse<int>.Ok(result.Value))
                                : BadRequest(ApiResponse.Fail(result.Error!));
    }

    [HttpPut("{id:int}"), Authorize(Policy = "MecanicoOAdmin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrdenRequest req, CancellationToken ct)
    {
        var cmd = new UpdateOrdenCommand(id, req.Estado, req.TrabajoRealizado, req.CostoManoObra, req.FechaEstimadaEntrega);
        var result = await mediator.Send(cmd, ct);
        return result.IsSuccess ? NoContent() : BadRequest(ApiResponse.Fail(result.Error!));
    }

    [HttpPost("{id:int}/repuestos"), Authorize(Policy = "MecanicoOAdmin")]
    public async Task<IActionResult> AgregarRepuesto(int id, [FromBody] AgregarRepuestoRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new AgregarRepuestoCommand(id, req.RepuestoId, req.Cantidad), ct);
        return result.IsSuccess ? Ok(ApiResponse.Ok()) : BadRequest(ApiResponse.Fail(result.Error!));
    }

    [HttpPost("{id:int}/cerrar"), Authorize(Policy = "MecanicoOAdmin")]
    public async Task<IActionResult> Cerrar(int id, CancellationToken ct)
    {
        var result = await mediator.Send(new CerrarOrdenCommand(id), ct);
        return result.IsSuccess ? Ok(ApiResponse<FacturaCreadaResult>.Ok(result.Value!))
                                : BadRequest(ApiResponse.Fail(result.Error!));
    }

    [HttpPost("{id:int}/cancelar"), Authorize(Policy = "MecanicoOAdmin")]
    public async Task<IActionResult> Cancelar(int id, CancellationToken ct)
    {
        var result = await mediator.Send(new CancelarOrdenCommand(id), ct);
        return result.IsSuccess ? NoContent() : BadRequest(ApiResponse.Fail(result.Error!));
    }
}
