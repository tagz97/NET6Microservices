using Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ResponseModel
{
    /// <summary>
    /// Initialises a new instance of the BaseResponse with specified type
    /// </summary>
    /// <typeparam name="T">Class for type of Response</typeparam>
    public class BaseResponse<T>
    {
        /// <summary>
        /// Response code indicating success or reason for failure
        /// </summary>
        public ResponseCode ResponseCode { get; set; }

        /// <summary>
        /// Data for the response
        /// </summary>
        public T Data { get; set; }
    }
}
