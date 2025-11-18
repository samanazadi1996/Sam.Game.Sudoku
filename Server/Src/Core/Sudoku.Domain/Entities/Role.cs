using Sudoku.Domain.Common;
using System.Collections.Generic;

namespace Sudoku.Domain.Entities
{

    public class Role : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public IList<UserRole> UserRoles { get; set; }


        public const string AdminRoleName = "Admin";

        //public const string ClientAdminRoleName = "ClientAdmin";

        //public const string ClientExpertRoleName = "ClientExpert";
    }

}
