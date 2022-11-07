using FlexibleAuth.Client.Services;
using FlexibleAuth.Shared;
using FlexibleAuth.Shared.Authorization;
using InfiniteEnumFlags;
using Microsoft.AspNetCore.Components;

namespace FlexibleAuth.Client.Pages.Admin.AccessControl;

public partial class Index
{
    [Inject] private IAccessControlClient AccessControlClient { get; set; } = null!;

    private AccessControlVm? _vm;

    protected override async Task OnInitializedAsync()
    {
        _vm = await AccessControlClient.GetConfigurationAsync();
    }

    private async Task Set(RoleDto role, string permissionKey, bool granted)
    {
        role.Set(permissionKey, granted);

        await AccessControlClient.UpdateConfigurationAsync(role);
    }
}