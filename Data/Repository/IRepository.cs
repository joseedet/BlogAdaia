using BlogAdaia.ViewModels;

using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(int pageNumber);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task RenoveAsync(TEntity entity);
        
        
        

        Task<bool> SaveAllChangesAsync();
    }
}