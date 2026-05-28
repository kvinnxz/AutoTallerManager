using Api.Dtos.Vehiculos;
using Api.Helpers;
using Application.UseCases.Vehiculos.Create;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/vehiculos"), Authorize]
public class VehiculosController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVehiculoRequest req, CancellationToken ct)
    {
        var result = await mediator.Send(req.Adapt<CreateVehiculoCommand>(), ct);
        return result.IsSuccess ? Ok(ApiResponse<int>.Ok(result.Value))
                                : BadRequest(ApiResponse.Fail(result.Error!));
    }
}
