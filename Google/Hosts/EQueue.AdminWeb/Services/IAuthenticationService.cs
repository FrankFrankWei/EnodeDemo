namespace EQueue.AdminWeb.Services
{
    public interface IAuthenticationService
    {
        void SignIn(string accountId, string accountName, bool createPersistentCookie);
        void SignOut();
    }
}
