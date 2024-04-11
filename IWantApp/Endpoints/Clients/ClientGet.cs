using IWantApp.Domain.Users;

namespace IWantApp.Endpoints.Clients
{
    public class ClientGet
    {
        public static string Template => "/clients";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        [AllowAnonymous]
        public static async Task<IResult> Action(HttpContext http)
        {
            var user = http.User;

            var resultado = new
            {
                Id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
                Name = user.Claims.First(c => c.Type == "name").Value
            };

            return Results.Ok(resultado);
        }
    }
}
