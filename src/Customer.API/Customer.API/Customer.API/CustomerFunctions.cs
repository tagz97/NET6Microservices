using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Customer.Domain.Models;
using Customer.Domain.Models.Requests;
using Customer.Domain.Models.Responses;
using Customer.Service.Validators;
using Framework.Enums;
using Framework.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Validation;

namespace Customer.API
{
    public partial class CustomerFunctions
    {
        [FunctionName("GetCustomerById")]
        [OpenApiOperation(operationId: "GetCustomerById", tags: new[] { "Customer" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Header, Required = true, Type = typeof(string), Description = "The **Id** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerResponse), Description = "The OK response")]
        public async Task<IActionResult> GetCustomerById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string id = req.Headers["id"];

            var resp = await _customerService.GetCustomerByIdAsync(id);

            return new OkObjectResult(resp);
        }

        [FunctionName("CreateCustomer")]
        [OpenApiOperation(operationId: "CreateCustomer", tags: new[] { "Customer" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateCustomerRequest))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerResponse), Description = "The OK response")]
        public async Task<IActionResult> CreateCustomer(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var resp = await _customerService.CreateCustomerFromRequestAsync(req);

            return new ObjectResult(resp)
            {
                StatusCode = (int?)(resp.ResponseCode == ResponseCode.No_Error ? HttpStatusCode.Created : HttpStatusCode.BadRequest)
            };
        }
    }
}

