using Microsoft.AspNetCore.Authorization;

namespace Restaurant_5._0.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation Operation { get; set; }
        public ResourceOperationRequirement(ResourceOperation operation)
        {
            Operation = operation;
        }
    }
}
