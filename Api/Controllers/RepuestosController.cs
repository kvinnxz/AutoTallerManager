using Api.Dtos.Repuestos;
using Api.Helpers;
using Application.UseCases.Clientes.GetAll;
using Application.UseCases.Repuestos.AjustarStock;
using Application.UseCases.Repuestos.Create;
using Application.UseCases.Repuestos.GetAll;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/repuestos"), Authorize]
public class RepuestosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] string? categoria = null, [FromQuery] string? descripcion = null,
        [FromQuery] bool? bajoStock = null, CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetRepuestosQuery(page, pageSize, categoria, descripcion, bajoStock), ct);
        PaginationHelper.AppendHeaders(Response, result.Value!.Total, page, pageSize);
        return Ok(ApiResponse<IEnumerable<RepuestoResult>>.Ok(result.Value.Items));
    }

    [HttpPost, Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Create([FromBody] CreateRepuestoRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(req.Adapt<CreateRepuestoCommand>(), ct);
        return result.IsSuccess ? Ok(ApiResponse<int>.Ok(result.Value))
                                : BadRequest(ApiResponse.Fail(result.Error!));
    }

    [HttpPatch("{id:int}/stock"), Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> AjustarStock(int id, [FromBody] AjustarStockRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(new AjustarStockCommand(id, req.Cantidad, req.Motivo), ct);
        return result.IsSuccess ? Ok(ApiResponse.Ok()) : BadRequest(ApiResponse.Fail(result.Error!));
    }
}
