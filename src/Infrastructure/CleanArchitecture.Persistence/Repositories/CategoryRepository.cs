using CleanArchitecture.Application.Common.Interfaces.Auth;
using CleanArchitecture.Application.Common.Persistence.Repositories;
using CleanArchitecture.Domain.Categories;

namespace CleanArchitecture.Persistence.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext, ICurrentUser currentUser) : base(dbContext, currentUser)
    {
    }
}
