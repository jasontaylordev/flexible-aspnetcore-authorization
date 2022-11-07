using FlexibleAuth.Client.Services;
using FlexibleAuth.Shared;
using Microsoft.AspNetCore.Components;
#pragma warning disable CS8618

namespace FlexibleAuth.Client.Pages.Admin.Users;

public partial class Index
{
    [Inject]
    public IUsersClient UsersClient { get; set; }

    public ICollection<UserDto> Users { get; set; } = new List<UserDto>();

    protected override async Task OnInitializedAsync()
    {
        Users = await UsersClient.GetUsersAsync();
    }
}
