using BackEndApp.Models;
using BackEndApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEndApp.Services.Classes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<T> Add(T t)
        {
            context.Set<T>().Add(t);
            await context.SaveChangesAsync();

            return t;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await GetById(id);
            if(entity == null)
                return false;

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<T>> Get()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await context.Set<T>().Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<T> Update(T t)
        {
            var entity = await GetById(t.Id);
            if (entity == null)
                return null;

            entity = t;
            await context.SaveChangesAsync();

            return t;
        }
    }
}
