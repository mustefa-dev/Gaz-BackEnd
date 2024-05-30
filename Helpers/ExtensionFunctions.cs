using System.Security.Claims;

namespace Gaz_BackEnd.Helpers
{
    public static class ExtensionFunctions
    {
        public static Guid GetUserId(this IHttpContextAccessor _httpContextAccessor)
        {
            try
            {
                if (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.RequestAborted.IsCancellationRequested)
                    throw new Exception("Request Aborted");
                var id = Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
                    ? userId
                    : Guid.Empty;
                return id;
            }
            catch { return Guid.Empty; }
        }
    }
}
