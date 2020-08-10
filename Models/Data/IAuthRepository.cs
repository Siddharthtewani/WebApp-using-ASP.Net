
using System.Threading.Tasks;
using FrndshipApp.API.Models;
namespace FrndshipApp.API.Models.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user , string password) ;
        Task<User> Login(string Username , string password);
        Task<bool> UserExist (string username);
    }
}