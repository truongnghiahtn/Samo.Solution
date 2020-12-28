using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Aplication.ViewModel.Register
{
    public class RequestRegisterCreate
    {
        public string type { get; set; }
        public decimal Money { get; set; }

        public string Description { get; set; }

        public Guid IdUser { get; set; }

        public int IdService { get; set; }
    }
}
