﻿using FinanceManager.Api.RouteHandlers.Filters;
using FinanceManager.Common.Entities;

namespace FinanceManager.Api.RouteHandlers;

public class ValidationFilterBuilder<TRequest>(RouteHandlerBuilder builder)
{
    public ValidationFilterBuilder<TRequest> EnsureEntityExists<TEntity>(Func<TRequest, Guid?> entityId) where TEntity : class, IEntity
    {
        builder
            .AddEndpointFilter(new EnsureEntityExistsFilter<TRequest, TEntity>(entityId))
            .ProducesProblem(StatusCodes.Status404NotFound);

        return this;
    }

    public ValidationFilterBuilder<TRequest> SelfOrAdminResource<TEntity>(Func<TRequest, Guid?> requestedUserId) where TEntity : class, IEntity
    {
        builder
            .AddEndpointFilter(new SelfOrAdminResourceFilter<TRequest, TEntity>(requestedUserId))
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden);

        return this;
    }
    
}

public static class ValidationFilterExtension
{
    public static ValidationFilterBuilder<TRequest> WithRequestValidation<TRequest>(this RouteHandlerBuilder builder)
    {
        builder
            .AddEndpointFilter<RequestValidationFilter<TRequest>>()
            .ProducesValidationProblem();

        return new ValidationFilterBuilder<TRequest>(builder);
    }
}