using System;
using System.Threading.Tasks;

namespace FrndshipApp.API.Models.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        
        public AuthRepository(DataContext context)
        {
            _context = context;

        }




        public Task<User> Login(string Username, string password)
        {
            throw new System.NotImplementedException();
        }




        public async Task<User> Register(User user,
                                   string password)
        {
           byte[] passwordHash ,passwordSalt;
           createpasswordHash (password, out passwordHash ,out passwordSalt); 

           user.PaswwordSalt=passwordSalt;
           user.PaswwordHash=passwordHash;

           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();
       
           return user;
        }




        private void createpasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac =new System.Security.Cryptography.HMACSHA512()){
                passwordSalt=hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            
        }




        public Task<bool> UserExist(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}