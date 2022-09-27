namespace BackEndApp.Services.Interfaces
{
    public interface IRepository<T>
    {
        public Task<T> GetById(int id);
        public Task<List<T>> Get();
        public Task<T> Add(T t);
        public Task<T> Update(T t);
        public Task<bool> Delete(int id);

    }
}
