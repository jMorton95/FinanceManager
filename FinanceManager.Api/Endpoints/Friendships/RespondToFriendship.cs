﻿using FinanceManager.Api.RouteHandlers;
using FinanceManager.Common.Contracts;
using FinanceManager.Common.Entities;
using FinanceManager.Data.Write.Friendships;

namespace FinanceManager.Api.Endpoints.Friendships;

public class RespondToFriendship : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("/respond", Handler)
        .WithDescription("Respond to a friendship request")
        .WithRequestValidation<Request>()
        .EnsureEntityExists<Common.Entities.User>(x => x.ResponderId)
        .EnsureEntityExists<Friendship>(x => x.FriendshipId)
        .SelfOrAdminResource<Common.Entities.User>(x => x.ResponderId);

    public record Request(Guid FriendshipId, Guid ResponderId, bool Accepted);

    public record Response(bool Success, string Message): IPostResponse;

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.FriendshipId)
                .NotEmpty()
                .WithMessage("Error occurred processing Friendship Request");
            
            RuleFor(x => x.ResponderId)
                .NotEmpty()
                .WithMessage("Error occurred accessing your User Id");
            
            RuleFor(x => x.Accepted)
                .NotNull()
                .WithMessage("Error occurred parsing your decision.");
        }
    }
    
    private static async Task<Results<Ok<Response>, ValidationError, BadRequest<Response>>> Handler(Request request, IWriteFriendships writeFriendships)
    {
        var result = await writeFriendships.EditAsync(request.FriendshipId, request.Accepted);
        
        var response = new Response(result, result ? "Success" : "Unable to confirm friendship decision.");
        
        return result ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}