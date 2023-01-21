using BasicAuth.Client.Services;
using BasicAuth.Shared;
using Microsoft.AspNetCore.Components;

namespace BasicAuth.Client.Pages.Admin.Users;

public partial class Index
{
    [Inject] public IUsersClient UsersClient { get; set; } = null!;

    public ICollection<UserDto> Users { get; set; } = new List<UserDto>();

    protected override async Task OnInitializedAsync()
    {
        Users = await UsersClient.GetUsersAsync();
    }
}
