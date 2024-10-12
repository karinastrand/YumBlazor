using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _db;
    public CartRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> ClearCartAsync(string? userId)
    {
        var cartItems = await _db.Carts.Where(u=>u.UserId == userId).ToListAsync();
        _db.Carts.RemoveRange(cartItems);
        return await _db.SaveChangesAsync()>0;
    }

    public async Task<IEnumerable<Cart>> GetAllAsync(string? userId)
    {
        return await _db.Carts.Where(u=>u.UserId == userId).Include(u=>u.Product).ToListAsync();
    }

    public async Task<int> GetTotalCartCountAsync(string? userId)
    {
        int cartCount = 0;
        var cartItems = await _db.Carts.Where(u => u.UserId == userId).ToListAsync();

        foreach (var item in cartItems)
        {
            cartCount+=item.Count;
        }
        return cartCount;
    }

    public async Task<bool> UpdateCartAsync(string userId, int productId, int updateBy)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return false;
        }

        var cart=await _db.Carts.FirstOrDefaultAsync(u=>u.UserId == userId && u.ProductId==productId);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                ProductId = productId,
                Count = updateBy
            };
            await _db.Carts.AddAsync(cart);
        }
        else
        {
            cart.Count += updateBy;
            if (cart.Count <= 0)
            {
                _db.Carts.Remove(cart);
            }
        }
        return await _db.SaveChangesAsync()>0;
    }

    
}