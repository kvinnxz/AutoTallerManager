using System.ComponentModel.DataAnnotations;
namespace Api.Dtos.Repuestos;
public record AjustarStockRequest([Required] int Cantidad, [Required] string Motivo);
