using Api.Dtos.Clientes;
using Api.Helpers;
using Application.UseCases.Clientes.Create;
using Application.UseCases.Clientes.Delete;
using Application.UseCases.Clientes.GetAll;
using Application.UseCases.Clientes.GetById;
using Application.UseCases.Clientes.Update;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/clientes"), Authorize]
public class ClientesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] string? nombre = null, CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetClientesQuery(page, pageSize, nombre), ct);
        PaginationHelper.AppendHeaders(Response, result.Value!.Total, page, pageSize);
        return Ok(ApiResponse<IEnumerable<ClienteResult>>.Ok(result.Value.Items));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetClienteByIdQuery(id), ct);
        return result.IsSuccess ? Ok(ApiResponse<ClienteResult>.Ok(result.Value!))
                                : NotFound(ApiResponse.Fail(result.Error!));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClienteRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(req.Adapt<CreateClienteCommand>(), ct);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, ApiResponse<int>.Ok(result.Value))
            : BadRequest(ApiResponse.Fail(result.Error!));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateClienteRequest req, CancellationToken ct)
    {
        var cmd    = new UpdateClienteCommand(id, req.Nombre, req.Telefono, req.Correo);
        var result = await mediator.Send(cmd, ct);
        return result.IsSuccess ? NoContent() : BadRequest(ApiResponse.Fail(result.Error!));
    }

    [HttpDelete("{id:int}"), Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await mediator.Send(new DeleteClienteCommand(id), ct);
        return result.IsSuccess ? NoContent() : BadRequest(ApiResponse.Fail(result.Error!));
    }
}
