﻿using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Features.V1.Categories;

public sealed class CategoryResponse : IAuditResponse
{
    public Guid Id { get; init; }
    public Guid CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
    public Guid LastModifiedBy { get; init; }
    public DateTime? LastModifiedOn { get; init; }
    public DateTime? DeletedOn { get; init; }
    public Guid? DeletedBy { get; init; }
    public string Name { get; init; }
}
