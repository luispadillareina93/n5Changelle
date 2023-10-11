using n5.permissions.Infraestructure.Contexts;
using n5.permissions.Infraestructure.Repositories;

namespace n5.permissions.Infraestructure.UnitOfWorks
{
    public class UnitOfWork<TEntity, Tkey>: IUnitOfWork<TEntity, Tkey> where TEntity : class
    {

        private readonly PermissionsDbContext _context;
        public IRepository<TEntity,Tkey> Repository { get; private set; }

        public UnitOfWork(PermissionsDbContext context)
        {
            _context = context;
            Repository = new Repository<TEntity,Tkey>(context);

        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
