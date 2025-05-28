namespace BasketAPI;
public class ShoppingCartItem
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Color { get; set; }
    public ShoppingCartItem(string productId, string productName, decimal price, int quantity)
    {
        ProductId = productId;
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }
    public ShoppingCartItem() { } // Parameterless constructor for deserialization
}