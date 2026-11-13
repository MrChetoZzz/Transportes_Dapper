using TransportOrders.Api.Models;

namespace TransportOrders.Api.Services;

public interface ITransportOrderService
{
    Task<IEnumerable<TransportOrder>> ListAsync(
        string? status = null,
        int? customerId = null,
        string? orderNumber = null);

    Task<TransportOrder?> FindAsync(int orderId);

    Task<bool> CreateAsync(TransportOrder order);

    Task<bool> UpdateAsync(TransportOrder order);

    Task<bool> DeleteAsync(int orderId);
}
