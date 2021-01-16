using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Aplication.ViewModel.Register
{
    public class RegisterVm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Img { get; set; }
        public decimal Money { get; set; }

        public string Type { get; set; }

        public DateTime DateCreate { get; set; }

        public string DateCreateToString { get; set; }
    }
}
