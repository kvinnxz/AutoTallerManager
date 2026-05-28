using Api.Dtos.Auth;
using Api.Helpers;
using Application.UseCases.Auth.Login;
using Application.UseCases.Auth.Register;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req, CancellationToken ct)
    {
        var cmd    = req.Adapt<LoginCommand>();
        var result = await mediator.Send(cmd, ct);
        return result.IsSuccess ? Ok(ApiResponse<LoginResult>.Ok(result.Value!))
                                : Unauthorized(ApiResponse.Fail(result.Error!));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req, CancellationToken ct)
    {
        var cmd    = req.Adapt<RegisterCommand>();
        var result = await mediator.Send(cmd, ct);
        return result.IsSuccess ? Ok(ApiResponse<int>.Ok(result.Value))
                                : BadRequest(ApiResponse.Fail(result.Error!));
    }
}
