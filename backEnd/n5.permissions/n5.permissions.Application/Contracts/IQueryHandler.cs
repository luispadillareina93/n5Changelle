using System.Threading.Tasks;

namespace n5.permissions.Application.Contracts
{
    public interface IQueryHandler<TQuery,TResult>
    {
        Task<TResult> HandleAsync(TQuery query);

    }
}
