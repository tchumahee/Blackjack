using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack.Classes.Exceptions;

namespace Blackjack.Classes
{
    class BlackJack
    {
        private int moneyScore;          // copy of the player's score for easy displaying during the game
        private int betAmount;

        private CardDeck cardDeck;
        private Hand dealerHand = new Hand();
        private Hand playerHand = new Hand();

        private Card nextCard;           // one readily drawn card to check for null (so that visual can be updated)
        private bool gameStarted = false;
        private bool stayChosen = false;

        private enum HandType { Player, Dealer }

        // hit and stay implemented here
        // dealer logic implemented here

        public int DealerHandScore { get => dealerHand.CardScore; }
        public int MoneyScore { get => moneyScore; }

        public Hand DealerHand { get => dealerHand; }
        public Hand PlayerHand { get => playerHand; }



        public BlackJack(int numDecks, int moneyScore)
        {
            ShuffleDeck(numDecks);

            foreach (Card card in cardDeck.PlayingCards)
            {
                Console.WriteLine(card);
            }

            this.moneyScore = moneyScore;
            this.betAmount = 5;
        }

        public void ShuffleDeck(int numDecks)           // to use if deck is empty during game 
        {
            cardDeck = new CardDeck(numDecks);
            cardDeck.Shuffle();
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
            else                              // if not enough cards (to be shuffled by the GUI handler)
                return false;
        }

        private void EndRound()
        {
            gameStarted = false;
            stayChosen = false;

            dealerHand.ClearHand();
            playerHand.ClearHand();
        }

        private Card DrawCard(HandType handType)
        {
            if (gameStarted)
            {
                if (nextCard != null)
                {
                    if (handType == HandType.Player)
                    {
                        Card addedCard = playerHand.AddPlayingCard(nextCard);
                        nextCard = cardDeck.DrawCard();
                        return addedCard;
                    }
                    else if (handType == HandType.Dealer)
                    {
                        Card addedCard = dealerHand.AddPlayingCard(nextCard);
                        nextCard = cardDeck.DrawCard();
                        return addedCard;
                    }
                    return null;
                }
            }
            return null;
        }

        public void RoundWin()
        {
            moneyScore += betAmount * 2;
            EndRound();
        }

        public void RoundLose()
        {
            moneyScore -= betAmount;
            EndRound();
        }

        public void PlaceBet(int betAmount)
        {
            this.betAmount = betAmount;
        }

        public Card Hit()
        {
            return DrawCard(HandType.Player);
        }

        public void Stay()
        {
            if (gameStarted)
            {
                gameStarted = false;
                stayChosen = true;
            }
        }

        public Card DealerDraw()
        {
            if (stayChosen)
            {
                if (dealerHand.CardScore > playerHand.CardScore)
                    return null;
                if (nextCard != null)
                {
                        if(dealerHand.CardScore < playerHand.CardScore)
                        {
                            Card addedCard = dealerHand.AddPlayingCard(nextCard);
                            nextCard = cardDeck.DrawCard();
                            return addedCard;
                        }
                        return null;
                }
                return null;
            }
            else
                throw new StayNotSelectedException();
        }
    }
}
