using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    public class UserScore
    {

        public string Username { get; set; }
        public int Score { get; set; }

        public override bool Equals(object obj)
        {
            return obj is UserScore score &&
                   Username == score.Username &&
                   Score == score.Score;
        }

        public override int GetHashCode()
        {
            int hashCode = 1829643766;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Username);
            hashCode = hashCode * -1521134295 + Score.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return "[ " + Username + ", " + Score + " ]";
        }
    }
}
