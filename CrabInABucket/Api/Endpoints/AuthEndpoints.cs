using CrabInABucket.Api.Mappers;
using CrabInABucket.Api.Requests;
using CrabInABucket.Api.Responses;
using CrabInABucket.Core.Services;
using CrabInABucket.Core.Services.Interfaces;
using CrabInABucket.Core.Workers;
using CrabInABucket.Core.Workers.Interfaces;
using CrabInABucket.Data;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrabInABucket.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var authGroup = app.MapGroup("/api/auth/").WithTags("Auth").AllowAnonymous();
        
        authGroup.MapPost("/login", async Task<Results<Ok<LoginResponse>, ValidationProblem, BadRequest>>
        (
            [FromBody] LoginRequest req,
            [FromServices] IValidator<LoginRequest> validator,
            [FromServices] ILoginWorker worker
        )
            =>
        {
            var validationResult = await validator.ValidateAsync(req);

            if (!validationResult.IsValid)
            {
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }

            var res = await worker.Login(req);

            return res != null ? TypedResults.Ok(res) : TypedResults.BadRequest();
        });
        
        authGroup.MapPost("/register", async Task<Results<Ok<CreateUserResponse>, ValidationProblem>>
        (
            [FromBody] CreateUserRequest req,
            [FromServices] IValidator<CreateUserRequest> validator,
            [FromServices] ICreateUserWorker createUserWorker
        ) 
            =>
        {
            var validationResult = await validator.ValidateAsync(req);

            if (!validationResult.IsValid)
            {
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }

            var result = await createUserWorker.CreateUser(req);

            return TypedResults.Ok(result);
        });
    }
}