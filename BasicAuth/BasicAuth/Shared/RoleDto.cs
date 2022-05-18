namespace BasicAuth.Shared;

public class RoleDto
{
    public RoleDto() : this(string.Empty, string.Empty) { }

    public RoleDto(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; set; }

    public string Name { get; set; }
}