using Api.Helpers;
using Application.UseCases.Facturas.GetAll;
using Application.UseCases.Facturas.Pagar;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/facturas"), Authorize]
public class FacturasController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] int? clienteId = null, [FromQuery] int? ordenId = null,
        [FromQuery] DateTime? desde = null, [FromQuery] DateTime? hasta = null,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetFacturasQuery(page, pageSize, clienteId, ordenId, desde, hasta), ct);
        PaginationHelper.AppendHeaders(Response, result.Value!.Total, page, pageSize);
        return Ok(ApiResponse<IEnumerable<FacturaResult>>.Ok(result.Value.Items));
    }

    [HttpPatch("{id:int}/pagar"), Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Pagar(int id, CancellationToken ct)
    {
        var result = await mediator.Send(new PagarFacturaCommand(id), ct);
        return result.IsSuccess ? Ok(ApiResponse.Ok()) : BadRequest(ApiResponse.Fail(result.Error!));
    }
}
