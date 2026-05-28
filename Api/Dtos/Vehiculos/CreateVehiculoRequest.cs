using System.ComponentModel.DataAnnotations;
namespace Api.Dtos.Vehiculos;
public record CreateVehiculoRequest(
    [Required] int ClienteId,
    [Required][MaxLength(50)] string Marca,
    [Required][MaxLength(50)] string Modelo,
    [Range(1900, 2100)] int Anio,
    [Required][MaxLength(17)] string VIN,
    [Range(0, int.MaxValue)] int Kilometraje);
