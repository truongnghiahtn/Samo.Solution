using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Data.Entities
{
    public class RegisterSpend
    {
        public int Id { get; set; }
        public decimal Money { get; set; }

        public string Description { get; set; }

        public DateTime DateCreate { get; set; }

        public Guid IdUser { get; set; }

        public int IdSpend { get; set; }

        public AppUser AppUser { get; set; }

        public Spend Spend { get; set; }
    }
}
