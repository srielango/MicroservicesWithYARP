namespace BasketAPI.Data;
public class ShoppingCartItem
{
    public string UserName { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice => Price * Quantity;
}