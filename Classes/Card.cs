using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack.Classes.HelperClasses;

namespace Blackjack.Classes
{
    class Card
    {
        private Rank rank;
        private Suit suit;
        private int value;

        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            this.suit = suit;
        }

        public int Value { get => value; }

        public Rank Rank
        {
            get => rank;
            set
            {
                this.value = (int)value <= 10 ? (int)value : 10;

                rank = value;
            }
        }

        public Suit Suit
        {
            get => suit;
            set
            {
                suit = value;
            }
        }

        public override string ToString()
        {
            return "[ " + rank + ", " + suit + " ]";
        }

        public override bool Equals(object obj)
        {
            return obj is Card card &&
                   rank == card.rank &&
                   suit == card.suit;
        }

        public override int GetHashCode()
        {
            int hashCode = 613535935;
            hashCode = hashCode * -1521134295 + rank.GetHashCode();
            hashCode = hashCode * -1521134295 + suit.GetHashCode();
            return hashCode;
        }
    }
}
