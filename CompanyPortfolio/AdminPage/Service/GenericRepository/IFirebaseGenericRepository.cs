namespace AdminPage.Service.GenericRepository
{
    public interface IFirebaseGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(string collectionName, string userId);
        Task<T?> GetByIdAsync(string collectionName, string id, string userId);
        Task AddAsync(string collectionName, T entity, string userId);
        Task UpdateAsync(string collectionName, string id, T entity, string userId);
        Task DeleteAsync(string collectionName, string id, string userId);
    }
}
