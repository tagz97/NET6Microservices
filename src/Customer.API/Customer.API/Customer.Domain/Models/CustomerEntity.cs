using CosmosDbClient;
using Customer.Domain.Models.Enums;
using Customer.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Models
{
    /// <summary>
    /// The customer entity
    /// </summary>
    public class CustomerEntity : Entity
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerEntity()
        {

        }

        /// <summary>
        /// Overloaded CTOR to allow creation of customer from request <see cref="CreateCustomerRequest"/>
        /// </summary>
        /// <param name="request">CreateCustomerRequest object <see cref="CreateCustomerRequest"/></param>
        public CustomerEntity(CreateCustomerRequest request)
        {
            Email = request.Email;
            FirstName = request.FirstName;
            LastName = request.LastName;
            MobileNumber = request.MobileNumber;
            PostCode = request.PostCode;
            State = CustomerState.ACTIVE;
        }

        /// <summary>
        /// Overloaded CTOR to allow updating customer from request <see cref="UpdateCustomerRequest"/>
        /// </summary>
        /// <param name="request">UpdateCustomerRequest object <see cref="UpdateCustomerRequest"/></param>
        public CustomerEntity(UpdateCustomerRequest request)
        {
            Email = request.Email ?? Email;
            FirstName = request.FirstName ?? FirstName;
            LastName = request.LastName ?? LastName;
            MobileNumber = request.MobileNumber ?? MobileNumber;
            PostCode = request.PostCode ?? PostCode;
        }

        /// <summary>
        /// Email address of the customer
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// First name of the customer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name / Surname of the customer
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Mobile number of the customer
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// PostCode for the customer
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// State of the customer
        /// </summary>
        public CustomerState State { get; set; }
    }
}
