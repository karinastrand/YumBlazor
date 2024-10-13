using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _db;
    public OrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<OrderHeader> CreateAsync(OrderHeader orderHeader)
    {
        orderHeader.OrderDate = DateTime.Now;
        await _db.OrderHeaders.AddAsync(orderHeader);
        await _db.SaveChangesAsync();
        return orderHeader;
    }

    public async Task<OrderHeader> GetAsync(int id)
    {
        return await _db.OrderHeaders.Include(v=>v.OrderDetails).FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<OrderHeader> UpdateStatusAsync(int orderId, string status, string paymentIntentId)
    {
        OrderHeader orderHeader= await _db.OrderHeaders.FirstOrDefaultAsync(v=>v.Id == orderId);
        if (orderHeader != null)
        {
            orderHeader.Status = status;
            if(!string.IsNullOrEmpty(paymentIntentId))
            {
                orderHeader.PaymentIntentId = paymentIntentId;
            }
            await _db.SaveChangesAsync();
            
        }
        return orderHeader;
    }

    public async Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId)
    {
        if (!string.IsNullOrEmpty(userId))
        {
            return await _db.OrderHeaders.Where(v=>v.UserId==userId).ToListAsync();
        }
        return await _db.OrderHeaders.ToListAsync();
    }

   

    public async Task<OrderHeader> GetOrderBySessionIdAsync(string sessionId)
    {
        return await _db.OrderHeaders.FirstOrDefaultAsync(u => u.SessionId == sessionId);
    }

   

   
}