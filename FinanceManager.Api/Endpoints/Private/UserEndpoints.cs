﻿namespace FinanceManager.Api.Endpoints.Private;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var usersGroup = app.MapGroup("/api/user/").WithTags("User").RequireAuthorization();
        
        // usersGroup.MapGet("/getAll", async Task<Results<Ok<IEnumerable<UserResponse>>, NoContent>> ([FromServices] DataContext db) =>
        // {
        //     var users = await db.User.Include(x => x.Roles).Include(x => x.Accounts).ToListAsync();
        //
        //     var res = users.Select(x => x.ToUserResponse());
        //     
        //     return users.Count > 0 ? TypedResults.Ok(res) : TypedResults.NoContent();
        // }).WithDisplayName("GetAll");
        //
        // usersGroup.MapGet("/get", async Task<Results<Ok<UserResponse>, ValidationProblem, NoContent>>
        //     (string username, [FromServices] DataContext db, IValidator<GetUserRequest> validator) =>
        // {
        //     var validationResult = await validator.ValidateAsync(new GetUserRequest(username));
        //     
        //     if (!validationResult.IsValid)
        //     {
        //         return TypedResults.ValidationProblem(validationResult.ToDictionary());
        //     }
        //     
        //     var user = await db.User.Include(x => x.Roles).Include(x => x.Accounts).FirstOrDefaultAsync(x => x.Username == username);
        //     
        //     if (user == null)
        //     {
        //         return TypedResults.NoContent();
        //     }
        //     
        //     var res = user.ToUserResponse();
        //         
        //     return TypedResults.Ok(res);
        // });
    }
}