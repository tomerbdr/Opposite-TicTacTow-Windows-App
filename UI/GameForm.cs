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
    public partial class GameForm : Form
    {
        private GameOperation m_Game = null;

        private List<List<ButtonGameBox>> m_GameBoxes = null;
        private int m_GameBoxSize = 50;
        private int m_NumOfBoxPerRow;
        private int m_NumOfBoxPerCol;
        private Label m_Player1;
        private Label m_Player1Score;
        private Label m_Player2;
        private Label m_Player2Score;

        public GameForm(int i_Rows,int i_Cols, string i_Player1Name, bool i_IsPlayer2PC, string i_Player2Name)
        {
            m_Game = new GameOperation(i_Rows, new Action<Point, char>(boxSelected));
            m_Game.SetPlayerTwo(i_IsPlayer2PC == true ? GameOperation.ePlayerType.Pc : GameOperation.ePlayerType.User);

            m_NumOfBoxPerCol = i_Cols;
            m_NumOfBoxPerRow = i_Rows;
            m_GameBoxes = new List<List<ButtonGameBox>>(i_Cols);

            for (int i = 0; i < i_Cols; i++)
            {
                List<ButtonGameBox> currList = new List<ButtonGameBox>(i_Rows);
                
                for (int j = 0; j < i_Rows; j++)
                {
                    ButtonGameBox newButton = new ButtonGameBox(m_GameBoxSize,i, j);
                    newButton.Click += new EventHandler(buttonGameBox_Click);
                    this.Controls.Add(newButton);
                    currList.Add(newButton);
                }

                m_GameBoxes.Add(currList);
            }

            InitializeComponent();

            m_Player1.Text = i_Player1Name + ":";
            if (i_IsPlayer2PC == true)
            {
                m_Player2.Text = "Computer:";
            }
            else
            {
                m_Player2.Text = i_Player2Name + ":";
            }
        }

        private void buttonGameBox_Click(object sender, EventArgs e)
        {
            ButtonGameBox ClickedButton = (sender as ButtonGameBox);
            
            if (ClickedButton.Enabled == true)
            {
                m_Game.PlayTurn(ClickedButton.BoxId);
                RoundPlayed();
            }
        }

        private void boxSelected(Point i_BoxId, char i_Sign)
        {
            m_GameBoxes[i_BoxId.X][i_BoxId.Y].Text = i_Sign.ToString();
            m_GameBoxes[i_BoxId.X][i_BoxId.Y].Enabled = false;
        }

        private void RoundPlayed()
        {
            if (m_Game.IsGameOver == true)
            {
                declareResult();
            }
            else
            {
                if (m_Game.IsSecondPlayerAI == true)
                {
                    m_Game.PlayTurn();

                    if (m_Game.IsGameOver == true)
                    {
                        declareResult();
                    }
                }
                else
                {
                    if (m_Game.CurrentPlayerToPlay.PlayerId == 1)
                    {
                        m_Player1.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                        m_Player2.Font = new Font(Label.DefaultFont, FontStyle.Regular);
                    }
                    else
                    {
                        m_Player1.Font = new Font(Label.DefaultFont, FontStyle.Regular);
                        m_Player2.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                    }
                }
            }
        }

        private void declareResult() 
        {
            Player winner = m_Game.Winner;
            string title;
            StringBuilder msg = new StringBuilder();

            if (winner == null)
            {
                title = "Tie!";
                msg.Append("Tie!" + Environment.NewLine);
            }
            else
            {
                title = "Win!";
                string winnerName;
                if (winner.PlayerId == 1)
                {
                    winnerName = m_Player1.Text;
                    m_Player1Score.Text = winner.NumOfWins.ToString();
                }
                else
                {
                    winnerName = m_Player2.Text.Substring(0, m_Player2.Text.Count() - 1);
                    m_Player2Score.Text = winner.NumOfWins.ToString();
                }

                msg.Append("Win!" + Environment.NewLine + winnerName + " is the Winner." + Environment.NewLine);
            }

            msg.Append("Would you like to play another round?");

            DialogResult res = MessageBox.Show(msg.ToString(),title,MessageBoxButtons.YesNo);

            if (res == DialogResult.Yes)
            {
                startNewRound();
            }
            else
            {
                this.Close();
            }
        }

        private void startNewRound()
        {
            m_Game.ResetGame();
            for (int i = 0; i < m_GameBoxes.Count; i++)
            {
                for (int j = 0; j < m_GameBoxes.Count; j++)
                {
                    m_GameBoxes[i][j].Text = "";
                    m_GameBoxes[i][j].Enabled = true;
                }
            }
        }
    }
}
