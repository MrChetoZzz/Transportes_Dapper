using TransportOrders.Api.Models;
using TransportOrders.Api.Repositories;

namespace TransportOrders.Api.Services;

public sealed class TransportOrderService : ITransportOrderService
{
    private readonly ITransportOrderRepository _repository;

    public TransportOrderService(ITransportOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TransportOrder>> ListAsync(
        string? status = null,
        int? customerId = null,
        string? orderNumber = null)
    {
        var response = await _repository.GetAllAsync(status, customerId, orderNumber);
        if (!response.Success)
        {
            return Array.Empty<TransportOrder>();
        }

        object? result = response.Result;
        return result as IEnumerable<TransportOrder> ?? Array.Empty<TransportOrder>();
    }

    public async Task<TransportOrder?> FindAsync(int orderId)
    {
        var response = await _repository.GetByIdAsync(orderId);
        if (!response.Success)
        {
            return null;
        }

        object? result = response.Result;
        return result as TransportOrder;
    }

    public async Task<bool> CreateAsync(TransportOrder order)
    {
        var response = await _repository.CreateAsync(order);
        return response.Success;
    }

    public async Task<bool> UpdateAsync(TransportOrder order)
    {
        var response = await _repository.UpdateAsync(order);
        return response.Success;
    }

    public async Task<bool> DeleteAsync(int orderId)
    {
        var response = await _repository.DeleteAsync(orderId);
        return response.Success;
    }
}