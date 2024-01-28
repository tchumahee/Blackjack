using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    class Game
    {
        private int moneyScore;          // copy of the player's score for easy displaying during the game
        private int betAmount;

        private CardDeck cardDeck;
        private Hand dealerHand;
        private Hand playerHand;

        private PlayingCard nextCard;           // one readily drawn card to check for null (so that visual can be updated)
        private bool gameStarted = false;
        private bool stayChosen = false;

        // hit and stay implemented here
        // dealer logic implemented here

        public int BetAmount { get => betAmount; }
        public int DealerHandScore { get => dealerHand.CardScore; }

        public Game(int numDecks, int moneyScore)
        {
            cardDeck = new CardDeck(numDecks);
            this.moneyScore = moneyScore;
        }

        public bool StartGame()     // draws 2 hands, readies first card and sets game state to started
        {
            if (cardDeck.CardsLeft >= 5)
            {
                playerHand.AddPlayingCard(cardDeck.DrawCard());
                playerHand.AddPlayingCard(cardDeck.DrawCard());
                dealerHand.AddPlayingCard(cardDeck.DrawCard());
                dealerHand.AddPlayingCard(cardDeck.DrawCard());

                nextCard = cardDeck.DrawCard();

                gameStarted = true;
                return true;
            }
            else                              // if not enough cards
                return false;
        }

        public void PlaceBet(int betAmount)
        {
            this.betAmount = betAmount;
        }

        public void Hit()
        {
            if (gameStarted)
            {
                if (playerHand.CardScore < 21)
                {
                    if(nextCard != null)
                        playerHand.AddPlayingCard(nextCard);
                    nextCard = cardDeck.DrawCard();
                }
            }
        }

        public void Stay()
        {
            if (gameStarted)
            {
                gameStarted = false;
                stayChosen = true;
            }
        }

        public bool DealerDraw()
        {
            if (stayChosen)
            {
                if (playerHand.CardScore < 21)
                {
                    if (nextCard != null)
                    {
                        if (dealerHand.CardScore <= 15)
                        {
                            dealerHand.AddPlayingCard(nextCard);
                            nextCard = cardDeck.DrawCard();
                            return true;
                        }
                        else if (dealerHand.CardScore <= 17)
                        {
                            // draws with some chance
                            return false;               // until implemented, doesn't draw
                        }
                        else
                        {
                            // very small chance of drawing
                            return false;
                        }
                    }
                    return false;
                }
                throw new Exception();
            }
            else
                throw new Exception();
        }
    }
}
