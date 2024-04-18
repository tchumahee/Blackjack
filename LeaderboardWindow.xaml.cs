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
using System.Windows.Shapes;
using System.IO;
using Blackjack.Classes;
using System.Data;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for LeaderboardWindow.xaml
    /// </summary>
    public partial class LeaderboardWindow : Window
    {

        public List<UserScore> userScores { get; set; }
        public List<UserScore> Leaderboard { get; set; }

        private void LoadLeaderboard()
        {
            userScores = new List<UserScore>();

            // Read data from the CSV file
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "leaderboard.csv");
            if (File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        if (values.Length == 2 && int.TryParse(values[1], out int score))
                        {
                            userScores.Add(new UserScore { Username = values[0], Score = score });
                        }
                    }
                    Leaderboard = userScores.OrderByDescending(item => item.Score).ToList();
                }
            }
            else
                Console.WriteLine("Error reading file");

            foreach (var item in userScores)
                Console.WriteLine("Item: " + item);
        }

        public LeaderboardWindow()
        {
            InitializeComponent();
            LoadLeaderboard();
            DataContext = this;

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
