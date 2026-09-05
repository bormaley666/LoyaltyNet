using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LoyaltyNet.Web.Infrastructure.Filters;

//public class AddRequiredHeaderParameter : IOperationFilter
//{
//    public void Apply(OpenApiOperation operation, OperationFilterContext context)
//    {
//        var idempotencyAttributes = context.MethodInfo.GetCustomAttributes<IdempotencyAttribute>();

//        if (idempotencyAttributes.Any())
//        {
//            operation.Parameters.Add(new OpenApiParameter
//            {
//                Name = "Idempotency-Token",
//                In = ParameterLocation.Header
//            });
//        }
//    }
//}
