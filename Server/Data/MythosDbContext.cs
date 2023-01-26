using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mythos.Common.Users;
using Mythos.WebApp.Server.Models;

namespace Mythos.WebApp.Server.Data;

public class MythosDbContext : ApiAuthorizationDbContext<MythosUser>
{
    public MythosDbContext(
        DbContextOptions<MythosDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
    }
}
