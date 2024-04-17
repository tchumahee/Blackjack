using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Blackjack.Classes;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void addPlayerCard(Card card, int posLeft, int posTop)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Style = (Style)Application.Current.FindResource("PlayingCard");
            Canvas.SetLeft(rectangle, posLeft);
            Canvas.SetTop(rectangle, posTop);
            rectangle.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Cards/" + card.Rank + "_of_" + card.Suit + ".png"))
            };

            PlayerCardsCanvas.Children.Add(rectangle);
        }

        private void addDealerCard(Card card, int posLeft, int posTop, bool firstCard)
        {
            Rectangle rectangle = new Rectangle
            {
                Width = 80,
                Height = 112
            };
            if(firstCard)
                rectangle.Style = (Style)Application.Current.FindResource("PlayingCardBack");
            else
            {
                rectangle.Style = (Style)Application.Current.FindResource("PlayingCard");
                rectangle.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Cards/" + card.Rank + "_of_" + card.Suit + ".png"))
                };
            }
                
            Canvas.SetLeft(rectangle, posLeft);
            Canvas.SetTop(rectangle, posTop);
            

            DealerCardsCanvas.Children.Add(rectangle);
        }

        private void initializeCards(Hand playerHand, Hand dealerHand)
        {
            int playerHandTop = 30;
            int playerHandLeft = 26;
            int dealerHandTop = 1;
            int dealerHandLeft = 42;

            foreach (Card card in playerHand.PlayingCards)
            {
                addPlayerCard(card, playerHandLeft += 38, playerHandTop += 14);
            }


            bool firstCard = true;
            foreach (Card card in dealerHand.PlayingCards)
            {
                addDealerCard(card, dealerHandLeft += 24, dealerHandTop += 8, firstCard);
                firstCard = false;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            const int numDecks = 2;
            const int startingScore = 100;

            //CardDeck cardDeck = new CardDeck(2);
            //foreach (Card playingCard in cardDeck.PlayingCards)
            //    Console.WriteLine(playingCard);

            //Console.WriteLine("-------------------------------------------------------------------------------------------------");
            //Console.WriteLine("-------------------------------------------------------------------------------------------------");
            //Console.WriteLine("-------------------------------------------------------------------------------------------------");

            //cardDeck.Shuffle();

            //foreach (Card playingCard in cardDeck.PlayingCards)
            //    Console.WriteLine(playingCard);


            //Card card = new Card(Classes.HelperClasses.Rank.Ace, Classes.HelperClasses.Suit.Clubs);
            //card1.Fill = new ImageBrush
            //{
            //    ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Cards/" + card.Rank + "_of_" + card.Suit + ".png"))
            //};


            BlackJack blackjack = new BlackJack(numDecks, startingScore);

            blackjack.StartGame();

            Hand playerHand = blackjack.PlayerHand;
            Hand dealerHand = blackjack.DealerHand;

            initializeCards(playerHand, dealerHand);
        }
    }
}
