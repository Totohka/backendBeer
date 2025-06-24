using Microsoft.AspNetCore.Authorization;

namespace AuthSSO.Requirements
{
    public class RefreshRequirement : IAuthorizationRequirement
    {
        public RefreshRequirement()
        {
        }
    }
}
