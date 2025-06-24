using Microsoft.AspNetCore.Authorization;

namespace AuthSSO.Requirements
{
    public class TokenRequirement : IAuthorizationRequirement
    {
        public TokenRequirement()
        {
        }
    }
}
