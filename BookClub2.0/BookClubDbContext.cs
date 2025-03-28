using BookClub2._0.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub2._0
{
    public class BookClubDbContext : DbContext
    {
        public BookClubDbContext(
            DbContextOptions<BookClubDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        }
    }
