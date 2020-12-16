using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Data.Entities
{
    public class RegisterMakeMoney
    {
        public int Id { get; set; }

        public decimal Money { get; set; }

        public string Description { get; set; }

        public DateTime DateCreate { get; set; }

        public Guid IdUser { get; set; }

        public int IdMakeMoney { get; set; }

        public AppUser AppUser { get; set; }

        public MakeMoney makeMoney { get; set; }
    }
}
