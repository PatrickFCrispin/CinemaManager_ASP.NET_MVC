using CinemaManager.Models;
using Newtonsoft.Json;

namespace CinemaManager.Helpers
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionName = "UserSession";

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void CreateUserSession(UserModel userModel)
        {
            var value = JsonConvert.SerializeObject(userModel);

            _httpContextAccessor.HttpContext?.Session.SetString(SessionName, value);
        }

        public UserModel? GetUserSession()
        {
            var userSession = _httpContextAccessor.HttpContext?.Session.GetString(SessionName);

            if (string.IsNullOrEmpty(userSession)) { return null; }

            return JsonConvert.DeserializeObject<UserModel>(userSession);
        }

        public void RemoveUserSession()
        {
            _httpContextAccessor.HttpContext?.Session.Remove(SessionName);
        }
    }
}