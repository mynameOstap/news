using AutoMapper;
using Data;
using IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq.Expressions;

namespace Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly Context _db;
       
    

        public UserRepository(Context db):base(db)
        {
            _db = db;
     
            
        }
        public async Task<User> Update(int id, User updatedUser)
        {
            var user  = await _db.Users.FindAsync(id);
            user.Name = updatedUser.Name;
            user.HashPassword = updatedUser.HashPassword;
            user.Administrator = updatedUser.Administrator;
            user.CreatedAt = updatedUser.CreatedAt;
          
            await _db.SaveChangesAsync();
            return user;
        }

       
    }
}

