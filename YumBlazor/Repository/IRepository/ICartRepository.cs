using YumBlazor.Data;

namespace YumBlazor.Repository.IRepository;

public interface ICartRepository
{
    public Task<bool> UpdateCartAsync(string userId, int productId, int updateBy);
    
    public Task<IEnumerable<Cart>> GetAllAsync(string? userId);
    public Task<bool> ClearCartAsync(string? userId);

}
