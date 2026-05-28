using Api.Dtos.Clientes;
using Api.Dtos.Ordenes;
using Api.Dtos.Repuestos;
using Api.Dtos.Vehiculos;
using Application.UseCases.Clientes.Create;
using Application.UseCases.Clientes.Update;
using Application.UseCases.Ordenes.AgregarRepuesto;
using Application.UseCases.Ordenes.Create;
using Application.UseCases.Ordenes.Update;
using Application.UseCases.Repuestos.AjustarStock;
using Application.UseCases.Repuestos.Create;
using Application.UseCases.Vehiculos.Create;
using Mapster;

namespace Api.Mappings;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        // Clientes
        TypeAdapterConfig<CreateClienteRequest, CreateClienteCommand>.NewConfig();
        TypeAdapterConfig<UpdateClienteRequest, UpdateClienteCommand>
            .NewConfig()
            .ConstructUsing(src => new UpdateClienteCommand(0, src.Nombre, src.Telefono, src.Correo));

        // Vehiculos
        TypeAdapterConfig<CreateVehiculoRequest, CreateVehiculoCommand>.NewConfig();

        // Ordenes
        TypeAdapterConfig<CreateOrdenRequest, CreateOrdenCommand>.NewConfig();
        TypeAdapterConfig<UpdateOrdenRequest, UpdateOrdenCommand>
            .NewConfig()
            .ConstructUsing(src => new UpdateOrdenCommand(0, src.Estado, src.TrabajoRealizado, src.CostoManoObra, src.FechaEstimadaEntrega));
        TypeAdapterConfig<AgregarRepuestoRequest, AgregarRepuestoCommand>
            .NewConfig()
            .ConstructUsing(src => new AgregarRepuestoCommand(0, src.RepuestoId, src.Cantidad));

        // Repuestos
        TypeAdapterConfig<CreateRepuestoRequest, CreateRepuestoCommand>.NewConfig();
        TypeAdapterConfig<AjustarStockRequest, AjustarStockCommand>
            .NewConfig()
            .ConstructUsing(src => new AjustarStockCommand(0, src.Cantidad, src.Motivo));
    }
}
