using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

public interface IUserContextService
{
    int? GetUserId { get; }
    ClaimsPrincipal? User { get; }
}

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public int? GetUserId => User is null ? null :
        int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
}