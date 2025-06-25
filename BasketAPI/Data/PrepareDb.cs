using System.Collections.Generic;

namespace BasketAPI.Data
{
    public class PrepareDb
    {
        public static void LoadData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!context.ShoppingCarts.Any())
                {
                    context.ShoppingCarts.AddRange(LoadShoppingCarts());
                    context.SaveChanges();
                }
            }
        }

        private static List<ShoppingCart> LoadShoppingCarts()
        {
            var userName = "user1";

            List < ShoppingCart > shoppingCarts = new List<ShoppingCart>();
            shoppingCarts.Add(new ShoppingCart()
            {
                UserName = userName,
                Items = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem { UserName = userName, ProductName = "Samsung 10", Price = 100, Quantity = 1 },
                    new ShoppingCartItem { UserName = userName, ProductName = "IPhone X", Price = 200, Quantity = 2 }
                }
            });

            return shoppingCarts;
        }
    }
}
