using Microsoft.AspNetCore.Authorization;
using Restaurant_5._0.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Restaurant_5._0.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Restaurant>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Restaurant resource)
        {
            if(requirement.Operation == ResourceOperation.Read || requirement.Operation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }
            var userId = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(resource.CreatedById == int.Parse(userId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
