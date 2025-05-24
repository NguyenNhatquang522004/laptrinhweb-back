using backapi.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;

namespace backapi.Middleware
{
    public class JwtTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtTokenMiddleware> _logger;
        private readonly IJwtRepository _jwtService;

        private readonly HashSet<string> _protectedRoutes = new()
        {
            "/api/user/profile",
            "/api/user/update",
            "/api/user/delete",
            "/api/order",
            "/api/order/create",
            "/api/order/update",
            "/api/admin"
        };
        private readonly List<string> _protectedRoutePatterns = new()
        {
            "/api/admin/",
            "/api/user/secure/",
            "/api/order/manage/"
        };
        public JwtTokenMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<JwtTokenMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }
        private bool IsProtectedRoute(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;

            // Kiểm tra exact match
            if (_protectedRoutes.Contains(path)) return true;

            // Kiểm tra pattern match
            return _protectedRoutePatterns.Any(pattern => path.StartsWith(pattern));
        }
        private string ExtractToken(HttpContext context)
        {
            // Lấy token từ Authorization header
            if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var token = authHeader.ToString().Replace("Bearer ", "").Trim();
                return token;
            }
            // Nếu không có trong header, kiểm tra query string
            if (context.Request.Query.TryGetValue("token", out var tokenQuery))
            {
                return tokenQuery.ToString();
            }
            return null;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            // Kiểm tra xem route có cần authentication không
            if (!IsProtectedRoute(path))
            {
                await _next(context);
                return;
            }

            _logger.LogInformation($"Checking JWT token for protected route: {path}");

            try
            {
                var token = ExtractToken(context);

                if (string.IsNullOrEmpty(token))
                {
                    await HandleUnauthorized(context, "Token không được tìm thấy");
                    return;
                }

                var principal = _jwtService.ValidateToken(token);
                if (principal == null)
                {
                    await HandleUnauthorized(context, "Token không hợp lệ");
                    return;
                }

                // Gán user vào HttpContext
                context.User = principal;

                // Thêm user info vào HttpContext.Items để dễ truy cập
                var userIdClaim = principal.FindFirst("userId")?.Value;
                var usernameClaim = principal.FindFirst("username")?.Value;
                var roleClaim = principal.FindFirst(ClaimTypes.Role)?.Value;

                context.Items["UserId"] = userIdClaim;
                context.Items["Username"] = usernameClaim;
                context.Items["UserRole"] = roleClaim;

                _logger.LogInformation($"JWT validation successful for user: {usernameClaim}");
            }
            catch (SecurityTokenExpiredException)
            {
                await HandleUnauthorized(context, "Token đã hết hạn");
                return;
            }
            catch (SecurityTokenException ex)
            {
                await HandleUnauthorized(context, $"Token không hợp lệ: {ex.Message}");
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi validate JWT token");
                await HandleUnauthorized(context, "Lỗi xác thực token");
                return;
            }

            await _next(context);
        }
        private async Task HandleUnauthorized(HttpContext context, string message)
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Code = "401",
                Message = message,
                Data = (object)null
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }


}

