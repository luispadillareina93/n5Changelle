using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n5.permissions.Infraestructure.Kafka
{
    public interface IPermissionsProducer:IDisposable
    {
        Task ProduceAsync(string topic, string operation);
    }
}
