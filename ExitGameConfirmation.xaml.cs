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

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for ExitGameConfirmation.xaml
    /// </summary>
    public partial class ExitGameConfirmation : Window
    {
        Action callback;

        public ExitGameConfirmation(Action callback)
        {
            InitializeComponent();

            this.callback = callback;
        }

        private void ConfirmBackButton_Click(object sender, RoutedEventArgs e)
        {
            callback();
            this.Close();
        }
    }
}
