using BookClub2._0.Interfaces;
using BookClub2._0.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookClub2._0.Repositories
{
    public class BookClubRepository : IBookClubRepository
    {
        private readonly BookClubDbContext _context;

        public BookClubRepository(BookClubDbContext context)
        {
            _context = context;
        }

        public BookClub Add(BookClub bookClub)
        {
            if (bookClub == null)
                throw new ArgumentNullException(nameof(bookClub), "BookClub cannot be null.");

            _context.BookClubs.Add(bookClub);
            _context.SaveChanges();
            return bookClub;
        }

        public BookClub? GetById(int id)
        {
            return _context.BookClubs
                .Include(bc => bc.Owner)
                .Include(bc => bc.Members)
                .FirstOrDefault(bc => bc.Id == id);
        }

        public IEnumerable<BookClub> GetAll()
        {
            return _context.BookClubs
                .Include(bc => bc.Owner)
                .Include(bc => bc.Members)
                .ToList();
        }

        public BookClub Update(int id, BookClub updatedBookClub)
        {
            var existingBookClub = GetById(id);
            if (existingBookClub == null)
                throw new ArgumentNullException(nameof(existingBookClub), "BookClub not found.");

            existingBookClub.Name = updatedBookClub.Name;
            existingBookClub.Description = updatedBookClub.Description;
            existingBookClub.OwnerId = updatedBookClub.OwnerId;
            existingBookClub.Members = updatedBookClub.Members;

            _context.SaveChanges();
            return existingBookClub;
        }

        public BookClub Remove(int id)
        {
            var bookClub = GetById(id);
            if (bookClub == null)
                throw new ArgumentNullException(nameof(bookClub), "BookClub not found.");

            _context.BookClubs.Remove(bookClub);
            _context.SaveChanges();
            return bookClub;
        }

        public void AddMember(int bookClubId, User member)
        {
            var bookClub = GetById(bookClubId);
            if (bookClub == null)
                throw new ArgumentNullException(nameof(bookClub), "BookClub not found.");

            if (bookClub.Members.Any(m => m.Id == member.Id))
                throw new InvalidOperationException("User is already a member of the BookClub.");

            bookClub.Members.Add(member);
            _context.SaveChanges();
        }

        public void RemoveMember(int bookClubId, int memberId)
        {
            var bookClub = GetById(bookClubId);
            if (bookClub == null)
                throw new ArgumentNullException(nameof(bookClub), "BookClub not found.");

            var member = bookClub.Members.FirstOrDefault(m => m.Id == memberId);
            if (member == null)
                throw new InvalidOperationException("User is not a member of the BookClub.");

            bookClub.Members.Remove(member);
            _context.SaveChanges();
        }
    }
}
