using Framework.ResponseModel;

namespace Customer.Domain.Models.Responses
{
    /// <summary>
    /// Customer response using Framework BaseResponse with type of CustomerEntity for response data
    /// </summary>
    public class CustomerResponse : BaseResponse<CustomerEntity>
    {
    }
}
