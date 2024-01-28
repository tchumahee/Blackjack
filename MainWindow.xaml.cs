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
        public MainWindow()
        {
            InitializeComponent();


            String test = "";

            CardDeck cardDeck = new CardDeck(2);
            foreach (PlayingCard playingCard in cardDeck.PlayingCards)
                Console.WriteLine(playingCard);

            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");

            cardDeck.Shuffle();

            foreach (PlayingCard playingCard in cardDeck.PlayingCards)
                Console.WriteLine(playingCard);

            tbTest.Content = test;
        }
    }
}
