using System.ComponentModel.DataAnnotations;

namespace YumBlazor.Data;

public class OrderHeader
{
    public int Id { get; set; }
    [Required] 
    public string UserId { get; set; }
    [Required]
    public double OrderTotal {  get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    [Required]
    public String Status { get; set; }
    [Required]
    public String Name { get; set; }
    [Required]
    public String PhoneNumber { get; set; }
    [Required]
    public String Email { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }=new List<OrderDetail>();
}
