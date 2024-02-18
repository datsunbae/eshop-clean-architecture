using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Domain.Products;
using CleanArchitecture.Infrastructure.Persistence.DataAccess.Repositories.Common;

namespace CleanArchitecture.Infrastructure.Persistence.DataAccess.Repositories;

public sealed class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
