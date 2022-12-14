global using Customer.Domain.Models;
global using Customer.Domain.Models.Enums;
global using Customer.Domain.Models.Requests;
global using Customer.Repository;
global using Customer.Repository.ServiceExtensions;
global using Customer.Service;
global using Customer.Service.Extensions;
global using Customer.Service.Validators;
global using Customer.Service.Validators.CustomerEntityPropertyValidators;
global using Framework.Enums;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Moq;
global using System.Text;
global using System.Text.Json;
global using Xunit;