using Swaraj.Domain.Entities.Identifiers;
using System;
using System.Text.Json.Serialization;

using static Swaraj.Domain.Entities.User;

namespace Swaraj.Domain.Entities
{
    public class User : BaseEntity<UserIdentifier>
    {
        public User()
            : base()
        {
            Id = new UserIdentifier(Guid.NewGuid());
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
