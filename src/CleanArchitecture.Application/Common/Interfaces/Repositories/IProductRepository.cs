using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Product;

namespace CleanArchitecture.Application.Common.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
}
