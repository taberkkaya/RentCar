using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RentCarServer.Application.Service;

namespace RentCarServer.Infrastructure.Services;
internal sealed class UserContext(
    IHttpContextAccessor httpContextAccessor
    ) : IUserContext
{
    public Guid GetUserId()
    {
        var httpContext = httpContextAccessor.HttpContext;
        var claims = httpContext.User.Claims;
        string? userId = claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
            throw new ArgumentException("Kullanıcı bilgisi bulunamadı");

        try
        {
            Guid id = Guid.Parse(userId);
            return id;
        }
        catch (Exception)
        {
            throw new ArgumentException("Kullanıcı id uygun GUID formatında değil");
        }
    }
}
