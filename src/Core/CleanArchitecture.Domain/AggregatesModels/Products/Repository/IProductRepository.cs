using CleanArchitecture.Application.Common.Persistence;
using CleanArchitecture.Domain.AggregatesModels.Products;

namespace CleanArchitecture.Domain.AggregatesModels.Products.Repository;

public interface IProductRepository : IRepository<Product>
{
}
