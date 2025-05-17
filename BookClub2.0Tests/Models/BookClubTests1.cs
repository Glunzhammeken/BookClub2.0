using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BookClub2._0.Models.Tests
{
    [TestClass]
    public class BookClubTests
    {
        [TestMethod]
        public void IdTest()
        {
            BookClub bookClub = new BookClub();
            bookClub.Id = 1;
            Assert.AreEqual(1, bookClub.Id);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bookClub.Id = -1);
        }

        [TestMethod]
        public void NameTest()
        {
            BookClub bookClub = new BookClub();
            bookClub.Name = "Test Club";
            Assert.AreEqual("Test Club", bookClub.Name);
            Assert.ThrowsException<ArgumentNullException>(() => bookClub.Name = null);
            Assert.ThrowsException<ArgumentNullException>(() => bookClub.Name = "");
        }

        [TestMethod]
        public void DescriptionTest()
        {
            BookClub bookClub = new BookClub();
            bookClub.Description = "Short description";
            Assert.AreEqual("Short description", bookClub.Description);
            Assert.ThrowsException<ArgumentNullException>(() => bookClub.Description = null);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bookClub.Description = new string('a', 51));
        }

        [TestMethod]
        public void OwnerIdTest()
        {
            BookClub bookClub = new BookClub();
            bookClub.OwnerId = 1;
            Assert.AreEqual(1, bookClub.OwnerId);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => bookClub.OwnerId = -1);
        }

        [TestMethod]
        public void OwnerTest()
        {
            BookClub bookClub = new BookClub();
            User owner = new User { Id = 1, UserName = "Owner" };
            bookClub.Owner = owner;
            Assert.AreEqual(owner, bookClub.Owner);
            Assert.ThrowsException<ArgumentNullException>(() => bookClub.Owner = null);
        }

      
    }
}
