namespace IWantApp.Endpoints.Products
{
    public class ProductGetAll
    {
        public static string Template => "/products";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;
    }
}
