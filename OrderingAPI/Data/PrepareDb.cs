namespace OrderingAPI.Data
{
    public class PrepareDb
    {
        public static void LoadData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(LoadOrders());
                    context.SaveChanges();
                }
            }
        }
        private static List<Order> LoadOrders()
        {
            return new List<Order>()
            {
                new Order() {
                OrderId = 1,
                UserName = "user1",
                FirstName = "Elango",
                LastName = "Srinivasan",
                EmailAddress = "srielango@gmail.com",
                AddressLine = "Chennai",
                Country = "India",
                State ="TN",
                ZipCode = "123456",
                TotalPrice = 350
                }
            };
        }
    }
}
