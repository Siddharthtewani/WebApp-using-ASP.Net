using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FrndshipApp.API.Models.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        
        public AuthRepository(DataContext context)
        {
            _context = context;

        }




        public async Task<User> Login(string Username, string password)
        {
            var user = await  _context.Users.FirstOrDefaultAsync(x=>x.Username==Username);
            if(user==null){
                return null;
            }
            

            if (!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt)){
                return null;

            }
            return user;
           
        }



        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
             using(var hmac =new System.Security.Cryptography.HMACSHA512(passwordSalt)){
               
                var computeHash =hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (var i=0;i<computeHash.Length;i++){
                    if(computeHash[i] != passwordHash[i]) return false;
                       
                }
               return true; 
            }

        }





        public async Task<User> Register(User user,
                                   string password)
        {


           byte[] passwordHash ,passwordSalt;
           createpasswordHash (password, out passwordHash ,out passwordSalt); 

           user.PasswordSalt=passwordSalt;
           user.PasswordHash=passwordHash;

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




        public async  Task<bool> UserExist(string username)
        {
            if (await _context.Users.AnyAsync(x=>x.Username==username))
              return true; 

            return false  ;
        }
    }
}