namespace CloudMight.Core.Entities;

public class Photo
{
    public Guid Id { get; set; }
    public string OrginalFileName { get; set; } = string.Empty;

    public string BucketName { get; set; } = string.Empty;
    public string ObjectKey { get; set; } = string.Empty;
    
    public string ContentType { get; set; } = string.Empty;
    public long SizeInBytes { get; set; }
    public DateTime CreatedAt { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }
}