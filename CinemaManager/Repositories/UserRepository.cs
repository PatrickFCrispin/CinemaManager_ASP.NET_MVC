using CinemaManager.Data;
using CinemaManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaManager.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _dbContext;
        private readonly CancellationToken _cancellationToken;

        private const string Admin = "Administrador";
        private const string User = "Usuário";

        public UserRepository(DBContext dBContext)
        {
            _dbContext = dBContext;
            _cancellationToken = new CancellationToken();
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            try
            {
                return await _dbContext.Users.ToListAsync(_cancellationToken);
            }
            catch (Exception) { throw; }
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, _cancellationToken);

                if (user is null) { throw new ArgumentNullException(string.Empty); }

                return user;
            }
            catch (Exception) { throw; }
        }

        public async Task<UserModel?> GetUserByLoginAsync(string login)
        {
            try
            {
                return await _dbContext.Users.FirstOrDefaultAsync(x => x.Login == login, _cancellationToken);
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> CheckIfUserAlreadyExistsAsync(UserModel userModel)
        {
            // Se o Id == 0 então o usuário está sendo adicionando
            if (userModel.Id == 0)
            {
                foreach (var u in _dbContext.Users)
                {
                    if (u.Email == userModel.Email || u.Login == userModel.Login) { return true; }
                }

                return false;
            }

            var user = await GetUserByIdAsync(userModel.Id);

            foreach (var u in _dbContext.Users)
            {
                if (u.Email == userModel.Email || u.Login == userModel.Login)
                {
                    if (u.Id != user.Id) { return true; }
                }
            }

            return false;
        }

        public async Task<bool> AddUserAsync(UserModel userModel)
        {
            try
            {
                switch (userModel.Perfil)
                {
                    case Enums.PerfilEnum.Admin:
                        userModel.PerfilDescription = Admin;
                        break;
                    case Enums.PerfilEnum.User:
                        userModel.PerfilDescription = User;
                        break;
                    default:
                        break;
                }

                userModel.CreatedAt = DateTime.Now;

                _dbContext.Users.Add(userModel);
                await _dbContext.SaveChangesAsync(_cancellationToken);

                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> UpdateUserAsync(UserModel userModel)
        {
            try
            {
                var user = await GetUserByIdAsync(userModel.Id);

                user.Name = userModel.Name;
                user.Email = userModel.Email;
                user.Login = userModel.Login;
                user.Perfil = userModel.Perfil;
                switch (user.Perfil)
                {
                    case Enums.PerfilEnum.Admin:
                        user.PerfilDescription = Admin;
                        break;
                    case Enums.PerfilEnum.User:
                        user.PerfilDescription = User;
                        break;
                    default:
                        break;
                }

                user.UpdatedAt = DateTime.Now;

                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync(_cancellationToken);

                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> RemoveUserAsync(int id)
        {
            try
            {
                var user = await GetUserByIdAsync(id);

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync(_cancellationToken);

                return true;
            }
            catch (Exception) { throw; }
        }
    }
}