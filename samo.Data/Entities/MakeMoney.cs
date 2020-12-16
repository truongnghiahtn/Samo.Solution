using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Data.Entities
{
    public class MakeMoney
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Img { get; set; }

        public bool Status { get; set; }

        public List<RegisterMakeMoney> RegisterMakeMoneys { get; set; }

       

    }
}
