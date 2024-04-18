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

        public Card AddPlayingCard(Card card)
        {
            if (cardScore < 21)
            {
                playingCards.Add(card);

                cardScore = 0;

                int numAces = 0;
                foreach (Card existingCard in playingCards)
                {
                    Console.WriteLine(existingCard);
                    if(existingCard.Rank == HelperClasses.Rank.Ace)
                        numAces++;
                    else
                        cardScore += existingCard.Value;
                }

                if(numAces > 0)
                {
                    if (cardScore + 11 + (numAces - 1) <= 21)
                    {
                        cardScore += 11 + (numAces - 1);
                    }
                    else
                    {
                        cardScore += numAces;
                    }
                }
                Console.WriteLine("Cards: " + cardScore);
                return card;
            }
            else return null;
        }

        public void ClearHand()
        {
            playingCards.Clear();
        }
    }
}
