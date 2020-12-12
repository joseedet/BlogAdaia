using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdaia.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private AppDbContext _context;

        
        //private IRepository<TEntity> _repo;
        public Repository(AppDbContext context)
        {
            _context = context;

        }

        public void Dispose()
        {
            _context.Dispose();
        }

       /* public async Task<TEntity> GetByIds(int id)
         {


            return await _context.Set<TEntity>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(t => this.Id == id);

                 //.FirstOrDefault(T => T.);

            // return await _context.Set<TEntity>().FindAsync(id);
         }*/

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        
        public async Task RenoveAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await SaveAllChangesAsync();
        }


        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }


        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            //await SaveAllChangesAsync();
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> GetAll(int pageNumber)
        {
            int pagesize = 1;

            
            return _context.Set<TEntity>().Skip(1).Take(pagesize);
        }

       
    }
    
}

