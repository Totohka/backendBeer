using AuthSSO.Common.Constants;
using BL.Utils;
using System.Security.Cryptography;

namespace BL.Helpers
{
    public static class PassHelper
    {
        public static string Generate() 
        {
            return RandomNumberGenerator.GetString(Constants.PasswordSymbols, 40);
        }
    }
}
