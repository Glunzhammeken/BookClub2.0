using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookClub2._0.Repositories;
using BookClub2._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub2._0.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using BookClub2._0.Models;



namespace BookClub2._0.Repositories.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {
        private IRepository _userRepository;
        private BookClubDbContext _context;
        
        [TestInitialize]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<BookClubDbContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookClub2.0;Integrated Security=True;")
                .Options;

            _context = new BookClubDbContext(options);

            // Sletter og genskaber databasen
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _userRepository = new UserRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }


        [TestMethod]
        public void CanAddUserToDatabase()
        {
            var user = new User
            {
                UserName = "TestUser",
                Email = "testuser@example.com",
                PasswordHash = "TestPassword123!",
                Role = "admin"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            Assert.AreEqual(1, _context.Users.Count());
            var retrievedUser = _context.Users.First();
            Assert.AreEqual("TestUser", retrievedUser.UserName);
            Assert.AreEqual("testuser@example.com", retrievedUser.Email);
            Assert.AreEqual("admin", retrievedUser.Role);

            //Manglende test på Exceptions
        }
    }
}