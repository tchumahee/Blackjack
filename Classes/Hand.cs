using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    class Hand
    {
        private List<Card> playingCards = new List<Card>();
        private int cardScore;

        public List<Card> PlayingCards { get => playingCards; }
        public int CardScore { get => cardScore; }

        public bool AddPlayingCard(Card card)
        {
            if (cardScore <= 21)
            {
                playingCards.Add(card);
                if (card.Rank == HelperClasses.Rank.Ace)
                {
                    if (cardScore + 11 <= 21)
                    {
                        cardScore += 11;
                        return true;
                    }
                }
                cardScore += card.Value;
                return true;
            }
            else return false;
        }

        public void ClearHand()
        {
            playingCards.Clear();
        }
    }
}
