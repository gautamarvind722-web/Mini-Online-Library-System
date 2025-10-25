using Mini_Backed.Models;

namespace Mini_Backed.Services.Interface
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Register(User user);
        string GenerateJwt(User user);
        Task<User> GetById(int id);
    }
}
