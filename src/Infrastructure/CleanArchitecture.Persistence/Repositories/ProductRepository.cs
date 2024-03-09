﻿using CleanArchitecture.Application.Common.Interfaces.Auth;
using CleanArchitecture.Application.Common.Persistence.Repositories;
using CleanArchitecture.Domain.Products;

namespace CleanArchitecture.Persistence.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext, ICurrentUser currentUser) : base(dbContext, currentUser)
    {
    }
}
