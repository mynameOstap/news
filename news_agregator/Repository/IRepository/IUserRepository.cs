using Microsoft.AspNetCore.Mvc;
using Model;
using System.Linq.Expressions;

namespace IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Update(int id, User updatedUser);
    }
}

