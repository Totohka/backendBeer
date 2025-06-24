namespace BL.Services.Tokens
{
    public interface IRefreshTokenService
    {
        public Task CreateRefreshToken(Guid userUid);
        public Task DeleteRefreshToken(Guid userUid);
    }
    public class RefreshTokenService : IRefreshTokenService
    {
        public Task CreateRefreshToken(Guid userUid)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRefreshToken(Guid userUid)
        {
            throw new NotImplementedException();
        }
    }
}
