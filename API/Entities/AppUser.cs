namespace API.Entities;

public class AppUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); // system generates new GUID
    public required string DisplayName { get; set; }
    public required string Email { get; set; }

}
