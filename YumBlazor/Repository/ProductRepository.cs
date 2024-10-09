using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;


namespace YumBlazor.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<Product> CreateAsync(Product obj)
    {
        await _db.Products.AddAsync(obj);
        await _db.SaveChangesAsync();
        return obj;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
        if(File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
        if (product != null)
        {
            _db.Products.Remove(product);
            return (await _db.SaveChangesAsync()) > 0;
        }
        return false;
    }

    public async Task<Product> GetAsync(int id)
    {
        var product = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
        if (product == null)
        {
            return new Product();
        }
        return product;

    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _db.Products.Include(u=>u.Category).ToListAsync();
    }

    public async Task<Product> UpdateAsync(Product obj)
    {
        var objFromDb = await _db.Products.FirstOrDefaultAsync(c => c.Id == obj.Id);
        if (objFromDb != null)
        {
            objFromDb.Name = obj.Name;
            objFromDb.Description = obj.Description;
            objFromDb.Price = obj.Price;
            objFromDb.ImageUrl = obj.ImageUrl;
            objFromDb.CategoryId = obj.CategoryId;

            _db.Products.Update(obj);
            await _db.SaveChangesAsync(true);
            return objFromDb;
        }
        return obj;
    }
}



