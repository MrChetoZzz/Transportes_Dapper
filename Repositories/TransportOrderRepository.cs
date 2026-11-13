using System.Data;
using Dapper;
using TransportOrders.Api.Data;
using TransportOrders.Api.Models;

namespace TransportOrders.Api.Repositories;

public sealed class TransportOrderRepository : ITransportOrderRepository
{
    private const string ConnectionId = "DefaultConnection";
    private const string SchemaName = "dbo";

    private readonly IDapperService _dapperService;

    public TransportOrderRepository(IDapperService dapperService)
    {
        _dapperService = dapperService;
    }

    public Task<DapperResponse> GetAllAsync(
        string? status = null,
        int? customerId = null,
        string? orderNumber = null)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Status", status, DbType.String);
        parameters.Add("@CustomerId", customerId, DbType.Int32);
        parameters.Add("@OrderNumber", orderNumber, DbType.String);

        return _dapperService.ExecuteStoredProcedureAsync<TransportOrder>(
            CreateProcedure("usp_TransportOrders_GetAll"),
            parameters,
            hasArray: true);
    }

    public Task<DapperResponse> GetByIdAsync(int orderId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@OrderId", orderId, DbType.Int32);

        return _dapperService.ExecuteStoredProcedureAsync<TransportOrder>(
            CreateProcedure("usp_TransportOrders_GetById"),
            parameters,
            hasArray: false);
    }

    public Task<DapperResponse> CreateAsync(TransportOrder order)
    {
        var parameters = CreateOrderParameters(order);
        parameters.Add(
            "@OrderId",
            dbType: DbType.Int32,
            direction: ParameterDirection.Output);

        return _dapperService.ExecuteStoredProcedureAsync<TransportOrder>(
            CreateProcedure("usp_TransportOrders_Create"),
            parameters,
            hasArray: false);
    }

    public Task<DapperResponse> UpdateAsync(TransportOrder order)
    {
        var parameters = CreateOrderParameters(order);
        parameters.Add("@OrderId", order.OrderId, DbType.Int32);

        return _dapperService.ExecuteStoredProcedureAsync<TransportOrder>(
            CreateProcedure("usp_TransportOrders_Update"),
            parameters,
            hasArray: false);
    }

    public Task<DapperResponse> DeleteAsync(int orderId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@OrderId", orderId, DbType.Int32);

        return _dapperService.ExecuteStoredProcedureAsync<TransportOrder>(
            CreateProcedure("usp_TransportOrders_Delete"),
            parameters,
            hasArray: false);
    }

    private static StoredProcedureInf CreateProcedure(string name) => new()
    {
        IdConnectionString = ConnectionId,
        SchemaName = SchemaName,
        Name = name
    };

    private static DynamicParameters CreateOrderParameters(TransportOrder order)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@OrderNumber", order.OrderNumber, DbType.String);
        parameters.Add("@CustomerId", order.CustomerId, DbType.Int32);
        parameters.Add("@VehicleId", order.VehicleId, DbType.Int32);
        parameters.Add("@OriginCity", order.OriginCity, DbType.String);
        parameters.Add("@DestinationCity", order.DestinationCity, DbType.String);
        parameters.Add("@CargoDescription", order.CargoDescription, DbType.String);
        parameters.Add("@WeightKg", order.WeightKg, DbType.Decimal);
        parameters.Add("@FreightAmount", order.FreightAmount, DbType.Decimal);
        parameters.Add("@CurrencyCode", order.CurrencyCode, DbType.AnsiStringFixedLength);
        parameters.Add("@Status", order.Status, DbType.String);
        parameters.Add("@PickupDate", order.PickupDate, DbType.Date);
        parameters.Add("@DeliveryDate", order.DeliveryDate, DbType.Date);
        parameters.Add("@Notes", order.Notes, DbType.String);
        return parameters;
    }
}
