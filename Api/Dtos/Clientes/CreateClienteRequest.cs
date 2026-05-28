using System.ComponentModel.DataAnnotations;
namespace Api.Dtos.Clientes;
public record CreateClienteRequest(
    [Required][MaxLength(100)] string Nombre,
    [Required][MaxLength(20)]  string Telefono,
    [Required][EmailAddress]   string Correo);
