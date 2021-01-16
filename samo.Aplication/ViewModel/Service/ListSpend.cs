using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Aplication.ViewModel.Service
{
    public class ListSpend<T>
    {
        public string Type { get; set; }
        public string Img { get; set; }
        public List<T> Data { get; set; }
    }
}
