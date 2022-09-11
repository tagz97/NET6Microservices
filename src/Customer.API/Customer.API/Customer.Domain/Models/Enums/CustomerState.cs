using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Models.Enums
{
    /// <summary>
    /// Enum defining the customer state
    /// </summary>
    public enum CustomerState
    {
        ACTIVE = 0,
        SUSPENDED = 1,
        INACTIVE = 2
    }
}
