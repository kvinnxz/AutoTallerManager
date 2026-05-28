using System.ComponentModel.DataAnnotations;
namespace Api.Dtos.Auth;
public record RegisterRequest(
    [Required][MaxLength(100)] string Nombre,
    [Required][EmailAddress]   string Correo,
    [Required][MinLength(8)]   string Password,
    [Required] string Rol);
