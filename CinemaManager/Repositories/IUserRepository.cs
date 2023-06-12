using CinemaManager.Models;

namespace CinemaManager.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task<UserModel> GetUserByIdAsync(int id);
        Task<UserModel?> GetUserByLoginAsync(string login);
        Task<bool> CheckIfUserAlreadyExistsAsync(UserModel userModel);
        Task<bool> AddUserAsync(UserModel userModel);
        Task<bool> UpdateUserAsync(UserModel userModel);
        Task<bool> RemoveUserAsync(int id);
    }
}