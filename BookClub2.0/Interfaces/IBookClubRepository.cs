using BookClub2._0.Models;
using System.Collections.Generic;

namespace BookClub2._0.Interfaces
{
    public interface IBookClubRepository
    {
        BookClub Add(BookClub bookClub, int ownerId);
        BookClub? GetById(int id);
        IEnumerable<BookClub> GetAll();
        BookClub Update(int id, BookClub updatedBookClub);
        BookClub Remove(int id);
        void AddMember(int bookClubId, int memberid);
        void RemoveMember(int bookClubId, int memberId);
    }
}
