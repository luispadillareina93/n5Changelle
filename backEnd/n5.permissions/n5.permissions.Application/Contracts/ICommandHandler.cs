using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n5.permissions.Application.Contracts
{
    public interface ICommandHandler<in TCommand>
    {
        Task HandleAsync(TCommand command);

    }
}
