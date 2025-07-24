using Microsoft.AspNetCore.Http;
using RentCarServer.Application.Services;
using System.Security.Claims;

namespace RentCarServer.Infrastructure.Services;
internal class UserContext(
    IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid GetUserId()
    {
        var httpContext = httpContextAccessor.HttpContext;
        if(httpContext is null)
            throw new ArgumentNullException("HttpContext bulunamadı.");
        var claims = httpContext.User.Claims;
        string? userId = claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
            throw new ArgumentNullException("Kullanıcı bilgisi bulunamadı.");
        try
        {
            Guid id = Guid.Parse(userId);
            return id;
        }
        catch (Exception)
        {
            throw new ArgumentNullException("Kullanıcı id uygun Guid formatında değil.");
        }

    }
}
