using CleanArchitecture.Application.Common.Persistence;
using CleanArchitecture.Domain.AggregatesModels.Orders;

namespace CleanArchitecture.Domain.AggregatesModels.Orders.Repository;

public interface IOrderRepository : IRepository<Order>
{
}
