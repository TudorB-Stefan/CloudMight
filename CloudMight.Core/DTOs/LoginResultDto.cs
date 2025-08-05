namespace CloudMight.Core.DTOs;

public class LoginResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string AccessToken { get; set; } // short-lived
    public string RefreshToken { get; set; } // longer-lived, stored in secure cookie
    public DateTime AccessTokenExpiresAt { get; set; }
    public DateTime RefreshTokenExpiresAt { get; set; }
    public UserInfoDto User { get; set; }
}