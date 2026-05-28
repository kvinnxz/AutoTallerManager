using System.ComponentModel.DataAnnotations;
namespace Api.Dtos.Repuestos;
public record CreateRepuestoRequest(
    [Required][MaxLength(30)]  string Codigo,
    [Required][MaxLength(200)] string Descripcion,
    [Required][MaxLength(80)]  string Categoria,
    [Range(0, int.MaxValue)]   int CantidadStock,
    [Range(0, int.MaxValue)]   int StockMinimo,
    [Range(0.01, double.MaxValue)] decimal PrecioUnitario);
