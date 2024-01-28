using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes.Exceptions
{
    class StayNotSelectedException : Exception
    {
        public StayNotSelectedException() : base("Stay not selected.") { }
        public StayNotSelectedException(string message) : base(message) { }
    }
}
