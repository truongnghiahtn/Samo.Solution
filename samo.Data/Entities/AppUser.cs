using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Data.Entities
{
    public class AppUser:IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }

        public decimal LimitMoney { get; set; }

        public decimal AccountBalance { get; set; }

        public List<RegisterMakeMoney> RegisterMakeMoneys { get; set; }

        public List<RegisterSpend> RegisterSpends { get; set; }

    }
}
