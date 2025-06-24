using AuthSSO.Common.Constants;
using System.Security.Cryptography;

namespace BL.Helpers
{
    public static class RefreshTokenHelper
    {
        public static string Generate()
        {
            return RandomNumberGenerator.GetString(Constants.RefreshTokenSymbols, 80);
        }
    }
}
