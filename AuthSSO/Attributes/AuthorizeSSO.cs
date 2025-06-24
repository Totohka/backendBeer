using Microsoft.AspNetCore.Authorization;

namespace AuthSSO.Attributes
{
    public class AuthorizeSSO : AuthorizeAttribute
    {
        public string? Application { get; set; }
    }
}
