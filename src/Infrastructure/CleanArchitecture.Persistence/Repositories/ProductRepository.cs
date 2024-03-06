using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Persistence.Repositories;
using CleanArchitecture.Domain.Products;
using CleanArchitecture.Infrastructure.Persistence;

namespace CleanArchitecture.Persistence.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext, ICurrentUser currentUser) : base(dbContext, currentUser)
    {
    }
}
