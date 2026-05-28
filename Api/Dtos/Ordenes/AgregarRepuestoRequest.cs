using System.ComponentModel.DataAnnotations;
namespace Api.Dtos.Ordenes;
public record AgregarRepuestoRequest(
    [Required] int RepuestoId,
    [Range(1, int.MaxValue)] int Cantidad);
