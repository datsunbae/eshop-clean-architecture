using CleanArchitecture.Application.Common.Interfaces.Persistence.Repositories;
using CleanArchitecture.Domain.Product;
using CleanArchitecture.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
