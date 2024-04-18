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
        const int numDecks = 2;
        const int startingScore = 100;
        const int startingBet = 5;

        int bet = startingBet;


        BlackJack blackjack;


        int playerHandTop;
        int playerHandLeft;
        int dealerHandTop;
        int dealerHandLeft;
        int playerCardDirection = 1;
        int dealerCardDirection = 1;

        Rectangle HiddenDealerCard;

        private void gameOver()
        {

        }

        private void endRound()
        {
            System.Threading.Thread roundEndThread = new System.Threading.Thread(new System.Threading.ThreadStart(
                () =>
                {
                    System.Threading.Thread.Sleep(2000);

                    if (blackjack.MoneyScore == 0)
                    {
                        gameOver();
                        return;
                    }

                    this.Dispatcher.Invoke(() =>
                    {
                        DealerCardsCanvas.Children.Clear();
                        PlayerCardsCanvas.Children.Clear();

                        Arrow.Fill = null;

                        playerCardDirection = 1;
                        dealerCardDirection = 1;

                        initializeBetButtons();
                    });
                }));

            roundEndThread.SetApartmentState(System.Threading.ApartmentState.STA);
            roundEndThread.IsBackground = true;
            roundEndThread.Start();

        }

        private void winState()
        {
            Console.WriteLine("Player won.");

            Arrow.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/ArrowUp.png"))
            };

            HiddenDealerCard.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Cards/" + blackjack.DealerHand.PlayingCards[0].Rank + "_of_" + blackjack.DealerHand.PlayingCards[0].Suit + ".png"))
            };

            blackjack.RoundWin();
            ScoreLabel.Content = blackjack.MoneyScore;

            endRound();
            // animate arrow
        }

        private void loseState()
        {
            Console.WriteLine("Player lost.");

            Arrow.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/ArrowDown.png"))
            };

            HiddenDealerCard.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Cards/" + blackjack.DealerHand.PlayingCards[0].Rank + "_of_" + blackjack.DealerHand.PlayingCards[0].Suit + ".png"))
            };

            blackjack.RoundLose();
            ScoreLabel.Content = blackjack.MoneyScore;

            endRound();
        }

        private void raiseBetButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Raise bet clicked.");

            if (bet + 10 <= blackjack.MoneyScore)
            {
                bet += 10;
                CurrentBetLabel.Content = bet;
            }
        }

        private void lowerBetButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Lower bet clicked.");

            if (bet - 10 > 0)
            {
                bet -= 10;
                CurrentBetLabel.Content = bet;
            }
        }

        private void confirmBetButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Confirm bet clicked.");

            blackjack.PlaceBet(bet);

            initializeGameStart();
        }

        private void HitButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Hit clicked.");

            Card newCard = blackjack.Hit();

            if (newCard != null)
                addPlayerCard(newCard, playerHandLeft += 38, playerHandTop += 14 * (playerCardDirection *= -1));

            if (blackjack.PlayerHand.CardScore > 21)
            {
                loseState();
                return;
            }
            else if (blackjack.PlayerHand.CardScore == 21)
            {
                winState();
                return;
            }
        }

        private void StayButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Stay clicked.");

            blackjack.Stay();

            HiddenDealerCard.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Cards/" + blackjack.DealerHand.PlayingCards[0].Rank + "_of_" + blackjack.DealerHand.PlayingCards[0].Suit + ".png"))
            };

            System.Threading.Thread dealerThread = new System.Threading.Thread(new System.Threading.ThreadStart(
                () =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;

                while (true)
                {
                    System.Threading.Thread.Sleep(1500);
                    Card newCard = blackjack.DealerDraw();
                    if (newCard != null)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            addDealerCard(newCard, dealerHandLeft += 24, dealerHandTop += 8 * (dealerCardDirection *= -1), false);
                        });
                    }
                        
                    else
                        break;
                    if (blackjack.DealerHand.CardScore > 21)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            winState();
                        });
                        return;
                    }
                }

                if (blackjack.DealerHand.CardScore >= blackjack.PlayerHand.CardScore)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        loseState();
                    });
                    
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        winState();
                    });
                    
                }
            }));

            dealerThread.SetApartmentState(System.Threading.ApartmentState.STA);
            dealerThread.IsBackground = true;
            dealerThread.Start();

        }

        private void SaveScoreButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Save score clicked.");

            AddScoreWindow addScoreWindow = new AddScoreWindow();
            addScoreWindow.Owner = this;
            addScoreWindow.Show();
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

            blackjack.StartGame();
            initializeCards();

            if (blackjack.DealerHand.CardScore == 21)
                loseState();
            else if (blackjack.PlayerHand.CardScore == 21)
                winState();
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
            if (firstCard)
            {
                rectangle.Style = (Style)Application.Current.FindResource("PlayingCardBack");
                rectangle.Name = "HiddenDealerCard";
                HiddenDealerCard = rectangle;
            }
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

            this.Dispatcher.Invoke(() =>
            {
                DealerCardsCanvas.Children.Add(rectangle);
            });
            
        }

        private void initializeCards()
        {
            playerHandTop = (int)Canvas.GetTop(PlayerCardRef) + 14;
            playerHandLeft = (int)Canvas.GetLeft(PlayerCardRef) - 38;
            dealerHandTop = (int)Canvas.GetTop(DealerCardRef) + 8;
            dealerHandLeft = (int)Canvas.GetLeft(DealerCardRef) - 24;

            foreach (Card card in blackjack.PlayerHand.PlayingCards)
            {
                addPlayerCard(card, playerHandLeft += 38, playerHandTop += 14 * (playerCardDirection *= -1));
            }


            bool firstCard = true;
            foreach (Card card in blackjack.DealerHand.PlayingCards)
            {
                addDealerCard(card, dealerHandLeft += 24, dealerHandTop += 8 * (playerCardDirection *= -1), firstCard);
                firstCard = false;
            }
        }

        public MainWindow()
        {
            InitializeComponent();


            blackjack = new BlackJack(numDecks, startingScore);

            ScoreLabel.Content = blackjack.MoneyScore;
            CurrentBetLabel.Content = bet;

            initializeBetButtons();
        }
    }
}
