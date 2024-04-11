using IWantApp.Domain.Users;
using static System.Net.WebRequestMethods;

namespace IWantApp.Endpoints.Employees
{
    public class EmployeePost
    {
        public static string Template => "/employees";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        [Authorize(Policy = "EmployeePolicy")]
        public static async Task<IResult> Action(EmployeeRequest employeeRequest, HttpContext http,UserCreator userCreator)
        {
            var created = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userClaims = new List<Claim>
            {
                new Claim("EmployeeCode", employeeRequest.EmployeeCode),
                new Claim("Name", employeeRequest.Name),
                new Claim("CreatedBy", created)

            };
            (IdentityResult identity, string userId) result = await userCreator.Create(employeeRequest.Email, employeeRequest.Password, userClaims);
            if (!result.identity.Succeeded)
            {
                return Results.ValidationProblem(result.identity.Errors.ConvertToProblemDetails());
            }

            //var created = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            //var user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };
            //var result = await userManager.CreateAsync(user, employeeRequest.Password);

            //if (!result.Succeeded)
            //{
            //    return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
            //}

            //var userClaims = new List<Claim>
            //{
            //    new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            //    new Claim("Name", employeeRequest.Name),
            //    new Claim("CreatedBy", created)

            //};

            //var claimsResult = await userManager.AddClaimsAsync(user, userClaims);

            //if (!claimsResult.Succeeded)
            //{
            //    return Results.BadRequest(claimsResult.Errors.First());
            //}


            return Results.Created($"/employees/{result.userId}",result.userId);
        }
    }
}
