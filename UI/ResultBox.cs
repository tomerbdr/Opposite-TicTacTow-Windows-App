using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Logic;

namespace UI
{
    internal class ResultBox
    {
        internal enum eResult 
        {
          Win, Tie
        }

        // return DialogResult(YES/NO) according to user chooise
        internal static DialogResult Show(eResult i_Result, Player i_Winner = null, string i_WinnerName = null)
        {
            return i_Result == eResult.Win ? winBox(i_Winner, i_WinnerName) : tieBox();
        }

        // Show winning dialog box and return user chooise
        internal static DialogResult winBox(Player i_Winner, string i_WinnerName)
        {
            string title = "Win!";
            string msg = "Win!" + Environment.NewLine + i_WinnerName + " is the Winner." + Environment.NewLine + "Would you like to play another round?";
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo);
        }

        // Show tie dialog box and return user chooise
        internal static DialogResult tieBox()
        {
            string title = "Tie!";
            string msg = "Tie!" + Environment.NewLine + "Would you like to play another round?";
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo);
        }
    }
}
