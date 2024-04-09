namespace IWantApp.Endpoints.Employees
{
    public class ClientPost
    {
        public static string Template => "/clients";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        [AllowAnonymous]
        public static async Task<IResult> Action(ClientRequest clientRequest, HttpContext http, UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser { UserName = clientRequest.Email, Email = clientRequest.Email };
            var result = await userManager.CreateAsync(user, clientRequest.Password);

            if (!result.Succeeded)
            {
                return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
            }

            var userClaims = new List<Claim>
            {
                new Claim("Cpf", clientRequest.Cpf),
                new Claim("Name", clientRequest.Name)

            };

            var claimsResult = await userManager.AddClaimsAsync(user, userClaims);

            if (!claimsResult.Succeeded)
            {
                return Results.BadRequest(claimsResult.Errors.First());
            }


            return Results.Created($"/clients/{user.Id}",user.Id);
        }
    }
}
