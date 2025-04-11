using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookClub2._0.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClub2._0.Interfaces;
using Microsoft.EntityFrameworkCore;
using BookClub2._0.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BookClub2._0.Repositories.Tests
{
    [TestClass()]
    public class BookClubRepositoryTests
    {
        private IBookClubRepository _bookClubRepository;
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

            _bookClubRepository = new BookClubRepository(_context);
            _userRepository = new UserRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }

        [TestMethod]
        public void CanAddBookClubToDatabase()
        {
            var user = new User
            {
                UserName = "TestUser",
                Email = "testuser@example.com",
                PasswordHash = "TestPassword123!",
                Role = "admin"
            };
            _userRepository.Add(user);
            Assert.AreEqual(1, user.Id);
            var bookClub = new BookClub
            {
                Name = "Test Book Club",
                Description = "A club for testing purposes.",
                OwnerId = 1,
                Members = new List<User>()
            };
            _bookClubRepository.Add(bookClub);
            Assert.AreEqual(1, _context.BookClubs.Count());
            var retrievedBookClub = _context.BookClubs.First();
            Assert.AreEqual("Test Book Club", retrievedBookClub.Name);
            Assert.AreEqual("A club for testing purposes.", retrievedBookClub.Description);
        }
        [TestMethod]
        public void CanRemoveBookClubFromDatabase()
        {
            var user = new User
            {
                UserName = "TestUser",
                Email = "testuser@example.com",
                PasswordHash = "TestPassword123!",
                Role = "admin"
            };
            _userRepository.Add(user);
            Assert.AreEqual(1, user.Id);
            var bookClub = new BookClub
            {
                Name = "Test Book Club",
                Description = "A club for testing purposes.",
                OwnerId = 1,
                Members = new List<User>()
            };
            _bookClubRepository.Add(bookClub);
            Assert.AreEqual(1, _context.BookClubs.Count());
            _bookClubRepository.Remove(bookClub.Id);
            Assert.AreEqual(0, _context.BookClubs.Count());

        }
        [TestMethod]
        public void CanUpdateBookClubFromDatabase()
        {
            var user = new User
            {
                UserName = "TestUser",
                Email = "testuser@example.com",
                PasswordHash = "TestPassword123!",
                Role = "admin"
            };
            _userRepository.Add(user);
            Assert.AreEqual(1, user.Id);
            var bookClub = new BookClub
            {
                Name = "Test Book Club",
                Description = "A club for testing purposes.",
                OwnerId = 1,
                Members = new List<User>()
            };
            _bookClubRepository.Add(bookClub);
            
            var updatedBookClub = new BookClub
            {
                Name = "Updated Book Club",
                Description = "An updated description.",
                OwnerId = 1,
                Members = new List<User>()
            };
            _bookClubRepository.Update(bookClub.Id, updatedBookClub);
            Assert.IsTrue(_context.BookClubs.Any(bc => bc.Name == "Updated Book Club"));

        }
        [TestMethod]
        public void CanGetBookClubFromDatabase()
        {
            var user = new User
            {
                UserName = "TestUser",
                Email = "testuser@example.com",
                PasswordHash = "TestPassword123!",
                Role = "admin"
            };
            _userRepository.Add(user);
            Assert.AreEqual(1, user.Id);
            var bookClub = new BookClub
            {
                Name = "Test Book Club",
                Description = "A club for testing purposes.",
                OwnerId = 1,
                Members = new List<User>()
            };
            _bookClubRepository.Add(bookClub);
            var retrievedBookClub = _bookClubRepository.GetById(bookClub.Id);
            Assert.IsNotNull(retrievedBookClub);
            Assert.AreEqual("Test Book Club", retrievedBookClub.Name);
        }
        [TestMethod]
        public void CanGetAllBookClubsFromDatabase()
        {
            var user = new User
            {
                UserName = "TestUser",
                Email = "testuser@example.com",
                PasswordHash = "TestPassword123!",
                Role = "admin"
            };
            _userRepository.Add(user);

            var bookClub1 = new BookClub
            {
                Name = "Book Club 1",
                Description = "Description 1",
                OwnerId = user.Id,
                Members = new List<User>()
            };
            var bookClub2 = new BookClub
            {
                Name = "Book Club 2",
                Description = "Description 2",
                OwnerId = user.Id,
                Members = new List<User>()
            };

            _bookClubRepository.Add(bookClub1);
            _bookClubRepository.Add(bookClub2);

            var allBookClubs = _bookClubRepository.GetAll();

            Assert.AreEqual(2, allBookClubs.Count());
            Assert.IsTrue(allBookClubs.Any(bc => bc.Name == "Book Club 1"));
            Assert.IsTrue(allBookClubs.Any(bc => bc.Name == "Book Club 2"));
        }
        [TestMethod]
        public void CanAddMemberToBookClub()
        {
            var user = new User
            {
                UserName = "TestUser",
                Email = "testuser@example.com",
                PasswordHash = "TestPassword123!",
                Role = "admin"
            };
            _userRepository.Add(user);

            var member = new User
            {
                UserName = "MemberUser",
                Email = "member@example.com",
                PasswordHash = "MemberPassword123!",
                Role = "participant"
            };
            _userRepository.Add(member);

            var bookClub = new BookClub
            {
                Name = "Test Book Club",
                Description = "A club for testing purposes.",
                OwnerId = user.Id,
                
            };
            _bookClubRepository.Add(bookClub);

            _bookClubRepository.AddMember(bookClub.Id, member);

            var retrievedBookClub = _bookClubRepository.GetById(bookClub.Id);
            Assert.IsTrue(retrievedBookClub.Members.Any(m => m.Id == member.Id));
        }
        [TestMethod]
        public void CanRemoveMemberFromBookClub()
        {
            var user = new User
            {
                UserName = "TestUser",
                Email = "testuser@example.com",
                PasswordHash = "TestPassword123!",
                Role = "admin"
            };
            _userRepository.Add(user);

            var member = new User
            {
                UserName = "MemberUser",
                Email = "member@example.com",
                PasswordHash = "MemberPassword123!",
                Role = "participant"
            };
            _userRepository.Add(member);

            var bookClub = new BookClub
            {
                Name = "Test Book Club",
                Description = "A club for testing purposes.",
                OwnerId = user.Id,
               
            };

            _bookClubRepository.Add(bookClub);
            _bookClubRepository.AddMember(bookClub.Id, member);

            var retrievedBookClub = _bookClubRepository.GetById(bookClub.Id);
            Assert.IsTrue(retrievedBookClub.Members.Any(m => m.Id == member.Id));

            _bookClubRepository.RemoveMember(bookClub.Id, member.Id);
            Assert.IsFalse(retrievedBookClub.Members.Any(m => m.Id == member.Id));
        }



    }
}