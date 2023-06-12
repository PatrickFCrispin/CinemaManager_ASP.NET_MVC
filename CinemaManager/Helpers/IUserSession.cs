using CinemaManager.Models;

namespace CinemaManager.Helpers
{
    public interface IUserSession
    {
        void CreateUserSession(UserModel userModel);
        UserModel? GetUserSession();
        void RemoveUserSession();
    }
}