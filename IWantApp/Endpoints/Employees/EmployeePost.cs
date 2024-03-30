namespace IWantApp.Endpoints.Employees
{
    public class EmployeePost
    {
        public static string Template => "/employees";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        [Authorize(Policy = "EmployeePolicy")]
        public static async Task<IResult> Action(EmployeeRequest employeeRequest, HttpContext http, UserManager<IdentityUser> userManager)
        {
            var created = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };
            var result = await userManager.CreateAsync(user, employeeRequest.Password);

            if (!result.Succeeded)
            {
                return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
            }

            var userClaims = new List<Claim>
            {
                new Claim("EmployeeCode", employeeRequest.EmployeeCode),
                new Claim("Name", employeeRequest.Name),
                new Claim("CreatedBy", created)

            };

            var claimsResult = await userManager.AddClaimsAsync(user, userClaims);

            if (!claimsResult.Succeeded)
            {
                return Results.BadRequest(claimsResult.Errors.First());
            }


            return Results.Created($"/employees/{user.Id}",user.Id);
        }
    }
}
