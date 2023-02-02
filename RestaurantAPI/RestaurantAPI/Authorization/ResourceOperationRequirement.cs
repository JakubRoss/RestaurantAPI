using Microsoft.AspNetCore.Authorization;

public enum ResourceOperation
{
    Create,
    Read,
    Update,
    Delete
}
public class ResourceOperationRequirement : IAuthorizationRequirement
{
    public ResourceOperationRequirement(ResourceOperation resourceOperation)
    {
        this.resourceOperation = resourceOperation;
    }

    public ResourceOperation resourceOperation { get; }
}