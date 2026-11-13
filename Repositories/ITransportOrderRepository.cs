using TransportOrders.Api.Data;
using TransportOrders.Api.Models;

namespace TransportOrders.Api.Repositories;

public interface ITransportOrderRepository
{
    Task<DapperResponse> GetAllAsync(
        string? status = null,
        int? customerId = null,
        string? orderNumber = null);

    Task<DapperResponse> GetByIdAsync(int orderId);

    Task<DapperResponse> CreateAsync(TransportOrder order);

    Task<DapperResponse> UpdateAsync(TransportOrder order);

    Task<DapperResponse> DeleteAsync(int orderId);
}
