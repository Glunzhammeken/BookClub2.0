using BookClub2._0.Interfaces;
using System.Xml.Linq;
using System.Security.Cryptography;
namespace BookClub2._0.Models
{
    public class User : IIdentifiable
    {
        private int _userId;
        private string? _userName;
        private string? _email;
        private string? _passwordHash;
        private string? _role;

        public int Id
        {
            get => _userId;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("id must be higher than 0");
                _userId = value;
            }
        }
        public string UserName
        {
            get => _userName;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Username is null");

                if (value.Length < 4)
                    throw new ArgumentOutOfRangeException("Name must be more than 4 letters");

                // Check for invalid characters
                if (value.Any(ch => !char.IsLetterOrDigit(ch) && ch != '_'))
                    throw new ArgumentException("Username can only contain letters, digits, and underscores");

                _userName = value;
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Email cannot be null");

                // Check if the email matches a standard pattern
                var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

                if (!emailRegex.IsMatch(value))
                    throw new ArgumentException("Invalid email format");

                _email = value;
            }
        }
        public string PasswordHash
        {
            get => _passwordHash;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Password cannot be null or empty");

                if (value.Length < 8)
                    throw new ArgumentException("Password must be at least 8 characters long");

                // Check for at least one uppercase letter
                if (!value.Any(char.IsUpper))
                    throw new ArgumentException("Password must contain at least one uppercase letter");

                // Check for at least one lowercase letter
                if (!value.Any(char.IsLower))
                    throw new ArgumentException("Password must contain at least one lowercase letter");

                // Check for at least one number
                if (!value.Any(char.IsDigit))
                    throw new ArgumentException("Password must contain at least one number");

                // Check for at least one special character
                if (!value.Any(ch => "!@#$%^&*()_+-=[]|,./<>?".Contains(ch))) // Add more special characters as needed
                    throw new ArgumentException("Password must contain at least one special character");

                // Hash the password before storing it
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value));
                    _passwordHash = Convert.ToBase64String(hashedBytes);
                }
            }
        }

        public string Role
        {
            get => _role;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Role cannot be null or empty");

                // Check if the role is either "admin" or "participant"
                if (value.ToLower() != "admin" && value.ToLower() != "participant")
                    throw new ArgumentException("Role must be either 'admin' or 'participant'");

                _role = value.ToLower();
            }
        }

        public ICollection<BookClub> OwnedBookClubs { get; set; } 
        public ICollection<BookClub> MemberOfBookClubs { get; set; } 

        public User()
        {
            OwnedBookClubs = new List<BookClub>();
            MemberOfBookClubs = new List<BookClub>();
        }
        public override string ToString()
        {
            return $"Name of User: {_userName} Role of User: {_role}: E-Mail of User {_email}";
        }
    }
}
