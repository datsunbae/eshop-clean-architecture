using CleanArchitecture.Application.Common.Persistence;

namespace CleanArchitecture.Domain.AggregatesModels.Orders;

public interface IOrderRepository : IRepository<Order>
{
}
