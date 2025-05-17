using BookClub2._0.Interfaces;
using System.Collections.Generic;

namespace BookClub2._0.Models
{
    public class BookClub : IIdentifiable
    {
        private int _id;
        private string _name;
        private string _description;
        private int? _ownerId;
        private User? _owner;
        private ICollection<User>? _members;

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
                    throw new ArgumentOutOfRangeException("Description can't be longer than 50 characters");
                _description = value;
            }
        }
        public int? OwnerId
        {
            get => _ownerId;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("OwnerId cannot be null");
                _ownerId = (int)value; // Explicit cast to handle nullable type
            }
        }
        public User? Owner
        {
            get => _owner;
            set => _owner = value;
            
        }
        public ICollection<User>? Members
        {
            get => _members ??= new List<User>(); // Initialize as empty list if null
            set => _members = value ?? new List<User>(); // Allow null but replace with empty list
        }

        public BookClub()
        {
            Members = new List<User>();
        }

        public override string ToString()
        {
            return $"BookClub: {Name}, Description: {Description}, Owner: {Owner.UserName}";
        }
    }
}
