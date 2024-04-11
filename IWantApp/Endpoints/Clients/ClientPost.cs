using IWantApp.Domain.Users;

namespace IWantApp.Endpoints.Clients
{
    public class ClientPost
    {
        public static string Template => "/clients";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        [AllowAnonymous]
        public static async Task<IResult> Action(ClientRequest clientRequest, UserCreator userCreator)
        {
            var userClaims = new List<Claim>
            {
                new Claim("Cpf", clientRequest.Cpf),
                new Claim("Name", clientRequest.Name)

            };
           (IdentityResult identity, string userId) result = await userCreator.Create(clientRequest.Email, clientRequest.Password, userClaims);
            if (!result.identity.Succeeded)
            {
                return Results.ValidationProblem(result.identity.Errors.ConvertToProblemDetails());
            }
            //var user = new IdentityUser { UserName = clientRequest.Email, Email = clientRequest.Email };
            //var result = await userManager.CreateAsync(user, clientRequest.Password);

            //if (!result.Succeeded)
            //{
            //    return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
            //}

            //var userClaims = new List<Claim>
            //{
            //    new Claim("Cpf", clientRequest.Cpf),
            //    new Claim("Name", clientRequest.Name)

            //};

            //var claimsResult = await userManager.AddClaimsAsync(user, userClaims);

            //if (!claimsResult.Succeeded)
            //{
            //    return Results.BadRequest(claimsResult.Errors.First());
            //}


            return Results.Created($"/clients/{result.userId}", result.userId);
        }
    }
}
