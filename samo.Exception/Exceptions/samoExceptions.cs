using System;
using System.Collections.Generic;
using System.Text;

namespace samo.Untilities.Exceptions
{
    public class samoExceptions : Exception
    {
        public samoExceptions()
        {
        }

        public samoExceptions(string message)
            : base(message)
        {
        }

        public samoExceptions(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
