using System.Security.Permissions;
using YumBlazor.Data;

namespace YumBlazor.Utility;

public static class SD
{
    public static string Role_Admin = "Admin";
    public static string Role_Customer = "Customer";

    public static string StatusPending = "Pending";
    public static string StatusReadyForPickUp = "ReadyForPickUp";
    public static string StatusCompleted = "Completed";
    public static string StatusCancelled = "Cancelled";

    public static List<OrderDetail> ConvertCartListToOrderDetails(List<Cart> carts)
    {
        List<OrderDetail> orderDetails = new List<OrderDetail>();
        foreach (Cart cart in carts)
        {
            OrderDetail orderDetail = new OrderDetail
            {
                ProductId = cart.ProductId,
                Count = cart.Count,
                Price = Convert.ToDouble(cart.Product.Price),
                ProductName = cart.Product.Name,
            };
            orderDetails.Add(orderDetail);
        }

        return orderDetails;
    }
}
