using System.ComponentModel.DataAnnotations;
namespace Api.Dtos.Auth;
public record LoginRequest(
    [Required][EmailAddress] string Correo,
    [Required][MinLength(6)] string Password);
