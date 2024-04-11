namespace IWantApp.Endpoints.Products
{
    public class ProductGetShowCase
    {
        public static string Template => "/products/showcase";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;
        [AllowAnonymous]
        public static async Task<IResult> Action(ApplicationDbContext context, int page = 1, int row = 10, string orderBy = "name")
        {

            if (row > 10)
            {
                return Results.Problem(title: "Row with max 10", statusCode: 400);
            }

            var queryBase = context.Products.AsNoTracking().Include(p => p.Category)
                .Where(p => p.HasStock && p.Category.Active);

            var queryFilter = queryBase.Skip((page - 1) * row).Take(row);
            if (orderBy == "name")
            {
                queryFilter = queryFilter.OrderBy(p => p.Name);
            }
            else if (orderBy == "name")
            {
                queryFilter = queryFilter.OrderBy(p => p.Price);
            }
            else
            {
                return Results.Problem(title: "Order by only by price or name", statusCode: 400);
            }

            var products = queryFilter.ToList();

            var results = products.Select(p => new ProductResponse(p.Name, p.Category.Name, p.Description, p.HasStock, p.Price, p.Active));
            return Results.Ok(results);
        }
    }
}
