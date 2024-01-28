using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    class Dealer
    {
        // doesn't bet
        // automatically wins if 21
        // has no agency until the end, only checks state
        // has AI for actions

        private Hand hand = new Hand();

        public Hand Hand { get => hand; }
    }
}
