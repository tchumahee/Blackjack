using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack.Classes.HelperClasses;

namespace Blackjack.Classes
{
    class CardDeck
    {
        private Card[] playingCards;
        private int index = 0;
        private int cardsLeft;

        public Card[] PlayingCards { get => playingCards; }
        public int CardsLeft { get => cardsLeft; }

        public CardDeck(int numPacks)           // Constructor taking the number of card packs used as an argument
        {
            cardsLeft = numPacks * 52;
            playingCards = new Card[cardsLeft];
            int i = 0;

            for(int pack = 1; pack <= numPacks; pack++)
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                    {
                        playingCards[i] = new Card(rank, suit);
                        i++;
                    }
                }
            }
        }

        public Card DrawCard()
        {
            if (index < playingCards.Length)
            {
                cardsLeft--;
                return playingCards[index++];
            }
                
            else return null;
        }

        public void Shuffle()
        {
            var rng = new Random();
            var keys = playingCards.Select(e => rng.NextDouble()).ToArray();

            Array.Sort(keys, playingCards);
        }

        public override bool Equals(object obj)
        {
            return obj is CardDeck deck &&
                   EqualityComparer<Card[]>.Default.Equals(playingCards, deck.playingCards);
        }

        public override int GetHashCode()
        {
            return -308505388 + EqualityComparer<Card[]>.Default.GetHashCode(playingCards);
        }
    }
}
