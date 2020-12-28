using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Aplication.ViewModel.Register
{
    public class RegisterByDate<T>
    {
        public DateTime DateCreate { get; set; }

        public List<T> Data { get; set; }
    }
}
