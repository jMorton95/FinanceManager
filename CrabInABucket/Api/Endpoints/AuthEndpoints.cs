using CrabInABucket.Api.Mappers;
using CrabInABucket.Api.Requests;
using CrabInABucket.Api.Responses;
using CrabInABucket.Core.Services;
using CrabInABucket.Core.Services.Interfaces;
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
        app.MapPost("/api/auth/login", async Task<Results<Ok<string>, ValidationProblem, UnauthorizedHttpResult>>
            ([FromBody] LoginRequest req, [FromServices] DataContext db, [FromServices] ITokenService tokens, IValidator<LoginRequest> validator) =>
        {
            var validationResult = await validator.ValidateAsync(req);

            if (!validationResult.IsValid)
            {
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }
            
            var user = await db.User.FirstOrDefaultAsync(x => x.Username == req.Username && x.Password == req.Password);

            return user != null ? TypedResults.Ok(await tokens.CreateToken(user)) : TypedResults.Unauthorized();
        });
    }
}