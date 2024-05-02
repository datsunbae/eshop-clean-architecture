using CleanArchitecture.Application.Common.ApplicationServices.Auth;
using CleanArchitecture.Domain.AggregatesModels.Baskets;
using CleanArchitecture.Persistence.Common;

namespace CleanArchitecture.Persistence.Repositories;

public sealed class BasketRepository : Repository<Basket>, IBasketRepository
{
    public BasketRepository(ApplicationDbContext dbContext, ICurrentUser currentUser) : base(dbContext, currentUser)
    {
    }
}
