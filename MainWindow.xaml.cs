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
        const int numDecks = 1;
        const int startingScore = 100;
        const int startingBet = 5;


        BlackJack blackjack;


        int playerHandTop;
        int playerHandLeft;
        int dealerHandTop;
        int dealerHandLeft;
        int cardDirection = 1;


        private void raiseBetButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Raise bet clicked.");

            if(blackjack.BetAmount + 10 <= blackjack.MoneyScore)
            {
                blackjack.BetAmount += 10;
                CurrentBetLabel.Content = blackjack.BetAmount;
            }
        }

        private void lowerBetButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Lower bet clicked.");

            if (blackjack.BetAmount - 10 > 0)
            {
                blackjack.BetAmount -= 10;
                CurrentBetLabel.Content = blackjack.BetAmount;
            }
        }

        private void confirmBetButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Confirm bet clicked.");

            initializeGameStart();
        }

        private void HitButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Hit clicked.");

            Card newCard = blackjack.Hit();

            if (newCard != null)
                addPlayerCard(newCard, playerHandLeft += 38, playerHandTop += 14 * (cardDirection *= -1));

            if (blackjack.PlayerHand.CardScore > 21)
            {
                Console.WriteLine("Player lost.");
                return;
            }
        }

        private void StayButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Stay clicked.");
        }

        private void SaveScoreButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Save score clicked.");
        }

        private void addButton(string name, string content, RoutedEventHandler btnClick)
        {
            Button btn = new Button
            {
                Style = (Style)Application.Current.FindResource("PanelButton"),
                Name = name,
                Content = content
            };
            btn.Click += new RoutedEventHandler(btnClick);

            ButtonsPanel.Children.Add(btn);
        }

        private void initializeBetButtons()
        {
            ButtonsPanel.Children.Clear();
            addButton("RaiseBetButton", "Raise bet [E]", raiseBetButtonClick);
            addButton("LowerBetButton", "Lower bet [Q]", lowerBetButtonClick);
            addButton("ConfirmBetButton", "Deal [W]", confirmBetButtonClick);
        }

        private void initializeGameStart()
        {
            ButtonsPanel.Children.Clear();
            addButton("HitButton", "Hit [E]", HitButtonClick);
            addButton("StayButton", "Stay [S]", StayButtonClick);
            addButton("SaveButton", "Save score", SaveScoreButtonClick);

            initializeCards();
        }

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

        private void initializeCards()
        {
            playerHandTop = (int)Canvas.GetTop(PlayerCardRef) + 14;
            playerHandLeft = (int)Canvas.GetLeft(PlayerCardRef) - 38;
            dealerHandTop = (int)Canvas.GetTop(DealerCardRef) - 8;
            dealerHandLeft = (int)Canvas.GetLeft(DealerCardRef) - 24;

            foreach (Card card in blackjack.PlayerHand.PlayingCards)
            {
                addPlayerCard(card, playerHandLeft += 38, playerHandTop += 14 * (cardDirection *= -1));
            }


            bool firstCard = true;
            foreach (Card card in blackjack.DealerHand.PlayingCards)
            {
                addDealerCard(card, dealerHandLeft += 24, dealerHandTop += 8, firstCard);
                firstCard = false;
            }
        }

        public MainWindow()
        {
            InitializeComponent();


            blackjack = new BlackJack(numDecks, startingScore);
            ScoreLabel.Content = blackjack.MoneyScore;
            CurrentBetLabel.Content = blackjack.BetAmount;

            blackjack.StartGame();

            initializeBetButtons();
        }
    }
}
