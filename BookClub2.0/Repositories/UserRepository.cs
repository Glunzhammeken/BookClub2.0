using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookClub2._0.Interfaces;
using BookClub2._0.Models;
using System.Numerics;

namespace BookClub2._0.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookClubDbContext _context;

        public UserRepository(BookClubDbContext context)
        {
            _context = context;
        }

        public User Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "user cannot be null.");
            }
            user.Id = 0;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public User Remove(int id)
        {
            User? user = GetUserById(id);
            if (user == null)
            {
                throw new ArgumentNullException("User does not exist");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }
        public User? GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
           
            return user;
        }
        public IEnumerable<User> GetUsers()
        {
            List<User> values = new List<User>();
            values = _context.Users.ToList();
            return values;
        }
        public User? UpdateUser(int id, User newData)
        {
            var user = GetUserById(id);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "user cannot be null.");
            }
            user.UserName = newData.UserName;
            user.Email = newData.Email;
            user.PasswordHash = newData.PasswordHash;
            user.Role = newData.Role;
            _context.SaveChanges();
            return user;
        }
        public User? GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user;
        }


    }

    }
