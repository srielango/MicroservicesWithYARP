namespace BasketAPI.Data;
public class ShoppingCartItem
{
    public string UserName { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? Color { get; set; }
    public ShoppingCartItem(string userName, string productName, decimal price, int quantity)
    {
        UserName = userName;
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }
    public ShoppingCartItem() { } // Parameterless constructor for deserialization
}