using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Enums
{
    /// <summary>
    /// Response code enum for different responses
    /// </summary>
    public enum ResponseCode
    {
        // Default
        No_Error = 0,
        // Customer
        Update_Fail = 2,
        Delete_Fail = 3,
        Create_Fail = 4,
        Get_Fail = 5
    }
}
