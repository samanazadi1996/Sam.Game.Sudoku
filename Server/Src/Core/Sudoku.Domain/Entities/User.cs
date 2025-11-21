using Sudoku.Domain.Common;
using System;
using System.Collections.Generic;

namespace Sudoku.Domain.Entities
{
    public class User : AuditableBaseEntity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string ProfileImage { get; set; }
        public IList<UserRole> UserRoles { get; set; }
        public IList<UserGame> UserGames { get; set; }
        public string SecurityStamp { get; set; }

        public User UpdateSecurityStamp()
        {
            SecurityStamp = Guid.NewGuid().ToString();

            return this;
        }
    }

}
