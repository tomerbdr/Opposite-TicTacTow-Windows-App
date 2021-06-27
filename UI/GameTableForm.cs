using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;

namespace UI
{
    public partial class GameTableForm : Form
    {
        private const int k_ButtonSize = 50;
        private readonly GameOperation r_Game = null;
        private readonly List<List<ButtonTableCell>> r_TableButtons = null;
        private readonly int r_TableSize;

        private Label labelPlayer1;
        private Label labelPlayer1Score;
        private Label labelPlayer2;
        private Label labelPlayer2Score;

        // Get Desired TableSize, First player name, true if the second player is PC, Second player name
        // Initilize Game and GameTableForm according to input.
        public GameTableForm(int i_TableSize, string i_Player1Name, bool i_IsPlayer2PC, string i_Player2Name)
        {
            r_Game = new GameOperation(i_TableSize, new CellChangedDelegate(OnCellChangeInLogic));
            r_Game.SetPlayerTwo(i_IsPlayer2PC == true ? GameOperation.ePlayerType.Pc : GameOperation.ePlayerType.User);

            r_TableSize = i_TableSize;
            r_TableButtons = new List<List<ButtonTableCell>>(i_TableSize);

            // Creating GameTableForm's buttons according to TableSize
            for (int i = 0; i < i_TableSize; i++)
            {
                List<ButtonTableCell> currList = new List<ButtonTableCell>(i_TableSize);
                
                for (int j = 0; j < i_TableSize; j++)
                {
                    ButtonTableCell newButton = new ButtonTableCell(k_ButtonSize,i, j);
                    newButton.Click += new EventHandler(buttonTableCell_Click);
                    this.Controls.Add(newButton); // Add the button to the form.
                    currList.Add(newButton);
                }

                r_TableButtons.Add(currList);
            }

            InitializeComponent();
            labelPlayer1.Text = i_Player1Name + ":";
            labelPlayer2.Text = (i_IsPlayer2PC == true ? "Computer" : i_Player2Name) + ":";
        }

        // GameTable's button click event handler - Updating m_Game which box id was clicked.
        private void buttonTableCell_Click(object sender, EventArgs e)
        {
            ButtonTableCell ClickedButton = sender as ButtonTableCell;
            
            if (ClickedButton.Enabled == true)
            {
                r_Game.PlayTurn(ClickedButton.CellId);
                turnPlayed();
            }
        }

        // Method that send as delegate to Logic part, The Logic part invoke this method after changing a cell of the table.
        // The method Change the relevant button text to the sign of the player and disable it.
        private void OnCellChangeInLogic(Point i_BoxId, char i_Sign)
        {
            r_TableButtons[i_BoxId.X][i_BoxId.Y].Text = i_Sign.ToString();
            r_TableButtons[i_BoxId.X][i_BoxId.Y].Enabled = false;
        }

        private void turnPlayed()
        {
            if (r_Game.IsGameOver == true)
            {
                declareResult(); 
            }
            else
            {
                if (r_Game.IsSecondPlayerPC == true)
                {
                    r_Game.PlayTurn(); // PC play turn

                    if (r_Game.IsGameOver == true)
                    {
                        declareResult(); // Check if after PC played turn the game is over.
                    }
                }
                else
                {
                    // Bold the text of current player.
                    if (r_Game.CurrentPlayerToPlay.PlayerId == 1)
                    {
                        labelPlayer1.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                        labelPlayer2.Font = new Font(Label.DefaultFont, FontStyle.Regular);
                    }
                    else
                    {
                        labelPlayer1.Font = new Font(Label.DefaultFont, FontStyle.Regular);
                        labelPlayer2.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                    }
                }
            }
        }

        // Show result message and get chooise from user about next round.
        private void declareResult() 
        {
            Player winner = r_Game.Winner;
            ResultBox.eResult gameResult = winner == null ? ResultBox.eResult.Tie : ResultBox.eResult.Win;
            DialogResult dialogResult;

            if (gameResult == ResultBox.eResult.Tie)
            {
                dialogResult = ResultBox.Show(gameResult); // Tie MessageBox
            }
            else
            {
                string winnerName;

                // Get winner name and update score
                if (winner.PlayerId == 1)
                {
                    winnerName = labelPlayer1.Text.Substring(0, labelPlayer1.Text.Count() - 1);
                    labelPlayer1Score.Text = winner.NumOfWins.ToString();
                }
                else
                {
                    winnerName = labelPlayer2.Text.Substring(0, labelPlayer2.Text.Count() - 1);
                    labelPlayer2Score.Text = winner.NumOfWins.ToString();
                }

                dialogResult = ResultBox.Show(gameResult, winner, winnerName);
            }

            if (dialogResult == DialogResult.Yes)
            {
                startNewRound(); // User decided start a new round
            }
            else
            {
                this.Close(); // User decided end game
            }
        }

        // Reset game and form to start a new round.
        private void startNewRound()
        {
            r_Game.ResetGame();
            for (int i = 0; i < r_TableButtons.Count; i++)
            {
                // Clear and enable all Table buttons
                for (int j = 0; j < r_TableButtons.Count; j++)
                {
                    r_TableButtons[i][j].Text = string.Empty;
                    r_TableButtons[i][j].Enabled = true;
                }
            }
        }
    }
}
