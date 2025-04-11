using BookClub2._0.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace BookClub2._0.Models
{
    public class BookClub : IIdentifiable
    {
        private int _id;
        private string _name;
        private string _description;
        private int _ownerId;
        private User _owner;
        private ICollection<User> _members;

        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Id must be higher than 0");
                _id = value;
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Name cannot be null or empty");
                _name = value;
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Description cannot be null or empty");
                if (value.Length > 50)
                    throw new ArgumentOutOfRangeException("Description cant be higer than 50 charecters");
                _description = value;

            }
        }
        public int OwnerId
        {
            get => _ownerId;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("OwnerId cant be null");
                _ownerId = value;
            }
        }
        public User Owner
        {
            get => _owner;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Owner cannot be null");
                _owner = value;
            }
        }
        public ICollection<User> Members
        {
            get => _members;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Members cannot be null");
                
                _members = value;

            }
        }
        public BookClub()
        {
            Members = new List<User>();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
