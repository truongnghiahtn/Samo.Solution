using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Aplication.ViewModel.Service
{
    public class RequestUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Img { get; set; }

        public bool Status { get; set; }
    }
}
