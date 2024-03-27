using CleanArchitecture.Application.Common.Interfaces.Auth;
using CleanArchitecture.Domain.AggregatesModels.Orders;
using CleanArchitecture.Domain.AggregatesModels.Orders.Repository;
using CleanArchitecture.Persistence.Common;

namespace CleanArchitecture.Persistence.Repositories;

public sealed class OrderRepositoy : Repository<Order>, IOrderRepository
{
    public OrderRepositoy(ApplicationDbContext dbContext, ICurrentUser currentUser) : base(dbContext, currentUser)
    {
    }
}
