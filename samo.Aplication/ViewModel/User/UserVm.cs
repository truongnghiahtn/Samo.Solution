﻿using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Aplication.ViewModel.User
{
    public class UserVm
    {
        public Guid id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }

        public decimal LimitMoney { get; set; }

        public decimal AccountBalance { get; set; }
    }
}