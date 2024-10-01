using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Category> CreateAsync(Category obj)
        {
            await _db.Categories.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                return (await _db.SaveChangesAsync())>0;
            }
            return false;
        }

        public async Task<Category> GetAsync(int id)
        {
            var category= await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {  
                return new Category(); 
            }
            return category;
            
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category obj)
        {
            var objFromDb=await _db.Categories.FirstOrDefaultAsync(c=>c.Id==obj.Id);
            if (objFromDb != null)
            {   objFromDb.Name = obj.Name;
                _db.Categories.Update(obj);
                await _db.SaveChangesAsync(true);
                return objFromDb;
            }
            return obj;
        }
    }
}
