﻿using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Extensions;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Options;
using FlexibleAuth.Server.Models;
using FlexibleAuth.Shared.Authorization;
using InfiniteEnumFlags;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
#pragma warning disable CS8618

namespace FlexibleAuth.Server.Data;

// Based on https://github.com/dotnet/aspnetcore/blob/main/src/Identity/ApiAuthorization.IdentityServer/src/Data/ApiAuthorizationDbContext.cs
// Customised to add TRole.
public class ApiAuthorizationDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, string>, IPersistedGrantDbContext where TUser : IdentityUser where TRole : IdentityRole
{
    private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

    /// <summary>
    /// Initializes a new instance of <see cref="ApiAuthorizationDbContext{TUser}"/>.
    /// </summary>
    /// <param name="options">The <see cref="DbContextOptions"/>.</param>
    /// <param name="operationalStoreOptions">The <see cref="IOptions{OperationalStoreOptions}"/>.</param>
    public ApiAuthorizationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options)
    {
        _operationalStoreOptions = operationalStoreOptions;
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{PersistedGrant}"/>.
    /// </summary>
    public DbSet<PersistedGrant> PersistedGrants { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{DeviceFlowCodes}"/>.
    /// </summary>
    public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{Key}"/>.
    /// </summary>
    public DbSet<Key> Keys { get; set; }

    Task<int> IPersistedGrantDbContext.SaveChangesAsync() => base.SaveChangesAsync();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Role>()
            .Property(q => q.Permission)
            .HasConversion(
                flag => flag.ToUniqueId() ,
                str => Permission.FromUniqueId(str));
        builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
    }
}
