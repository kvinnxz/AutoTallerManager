using System.ComponentModel.DataAnnotations;
using Domain.Enums;
namespace Api.Dtos.Ordenes;
public record CreateOrdenRequest(
    [Required] int VehiculoId,
    [Required] int MecanicoId,
    [Required] TipoServicio TipoServicio,
    [Required][MaxLength(500)] string Descripcion,
    [Range(0, double.MaxValue)] decimal CostoManoObra);
