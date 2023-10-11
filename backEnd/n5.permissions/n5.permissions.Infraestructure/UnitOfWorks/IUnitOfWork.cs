using n5.permissions.Infraestructure.Repositories;

namespace n5.permissions.Infraestructure.UnitOfWorks
{
    public interface IUnitOfWork<TEntity, Tkey> where TEntity : class
    {
        IRepository<TEntity,Tkey> Repository { get; }
        Task<int> CommitAsync();
    }
}
