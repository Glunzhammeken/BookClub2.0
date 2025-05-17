using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BookClub2._0.Models;

namespace BookClub2._0.Interfaces
{
    public interface IUserRepository
    {
        User Add(User user);
        User? GetUserById(int id);
        IEnumerable<User> GetUsers();
        User Remove(int id);
        User? UpdateUser(int id, User nyData);
        User? GetUserByEmail(string email);
    }
}
