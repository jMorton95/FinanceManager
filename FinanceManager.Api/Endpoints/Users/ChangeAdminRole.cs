﻿using FinanceManager.Api.RouteHandlers;
using FinanceManager.Common.Constants;
using FinanceManager.Common.Contracts;
using FinanceManager.Data.Read.Users;
using FinanceManager.Data.Write.Users;

namespace FinanceManager.Api.Endpoints.Users;

public class ChangeAdminRole : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("change-admin-role", Handler)
        .RequireAuthorization(PolicyConstants.AdminRole)
        .WithDescription("Add or remove Administrator privileges to a specified User")
        .WithRequestValidation<Request>()
        .EnsureEntityExists<Common.Entities.User>(x => x.UserId);

    public record Request(Guid UserId, bool IsAdmin);

    public record Response(bool Success, string Message) : IPostResponse;

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.IsAdmin)
                .NotEmpty();
        }
    }

    private static async Task<Results<Ok<Response>, ValidationError, BadRequest<Response>>> Handler(
        Request request,
        IReadUsers readUsers,
        IWriteUsers writeUsers
    )
    {
        var user = await readUsers.GetByIdAsync(request.UserId)!;

        var roleChangeResult = await writeUsers.ManageUserAdministratorRole(user, request.IsAdmin);

        if (!roleChangeResult)
        {
            return TypedResults.BadRequest(new Response(roleChangeResult, $"Error occurred changing the role of {request.UserId}"));
        }

        var response = new Response(roleChangeResult, "Success");
        
        return TypedResults.Ok(response);
    }
}