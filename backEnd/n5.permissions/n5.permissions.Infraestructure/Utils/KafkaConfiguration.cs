using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n5.permissions.Infraestructure.Utils
{
    public class KafkaConfiguration
    {
        public string BootstrapServers { get; set; }
        public string ClientId { get; set; }
    }
}
