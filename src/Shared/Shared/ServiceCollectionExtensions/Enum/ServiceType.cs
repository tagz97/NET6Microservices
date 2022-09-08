using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCollectionExtensions.Enum
{
    /// <summary>
    /// Enum defining how a service is added to DI
    /// </summary>
    public enum ServiceType
    {
        SINGLETON = 0,
        TRANSIENT = 1,
        SCOPED = 2
    }
}
