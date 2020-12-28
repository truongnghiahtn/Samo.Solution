﻿using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Aplication.ViewModel.Common
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }

        public int TotalRecords { get; set; }
    }
}
