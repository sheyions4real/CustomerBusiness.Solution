namespace Customer.Service.API.Data.Repository.BaseRepository.Interface
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> values, CancellationToken cancellationToken);
        Task<T?> UpdateAsync(Guid id, T entity, CancellationToken cancellationToken);
        Task<bool?> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
