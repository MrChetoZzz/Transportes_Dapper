namespace TransportOrders.Api.Models;

public sealed class TransportOrder
{
    public int OrderId { get; init; }
    public string OrderNumber { get; init; } = string.Empty;
    public int CustomerId { get; init; }
    public int? VehicleId { get; init; }
    public string OriginCity { get; init; } = string.Empty;
    public string DestinationCity { get; init; } = string.Empty;
    public string CargoDescription { get; init; } = string.Empty;
    public decimal WeightKg { get; init; }
    public decimal FreightAmount { get; init; }
    public string CurrencyCode { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public DateOnly PickupDate { get; init; }
    public DateOnly? DeliveryDate { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}