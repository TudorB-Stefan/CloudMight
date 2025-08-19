namespace CloudMight.Core.DTOs;

public class UserInfoDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePictureUrl { get; set; }
    public IList<string> Roles { get; set; }
}