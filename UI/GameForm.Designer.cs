using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ShowIcon = false;
            this.ShowInTaskbar = true;
            int len = (m_NumOfBoxPerCol * m_GameBoxSize) + (m_NumOfBoxPerRow - 1) * 5 + 10 * 2;
            this.ClientSize = new System.Drawing.Size(len, len + 40);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TicTacToeMisere";

            // Initialize Game Boxes
            Point location = new Point(10, 10);

            for (int i = 0; i < this.m_GameBoxes.Count; i++)
            {
                for (int j = 0; j < this.m_GameBoxes[i].Count; j++)
                {
                    this.m_GameBoxes[i][j].Location = location;
                    location.X += m_GameBoxes[i][j].Width + 5;
                }

                location.X = 10;
                location.Y += m_GameBoxes[i][0].Height + 5;
            }

            // Initialize Score
            location.Y += 10;

            this.m_Player1 = new Label();
            this.m_Player1.Height = 25;
            this.m_Player1.Width = 60;
            this.m_Player1.Top = location.Y;
            this.m_Player1.Left = this.ClientSize.Width / 2 - 70;
            this.m_Player1.Font = new Font(Label.DefaultFont, FontStyle.Bold) ;

            this.m_Player1Score = new Label();
            this.m_Player1Score.Height = 25;
            this.m_Player1Score.Width = 10;
            this.m_Player1Score.Top = location.Y;
            this.m_Player1Score.Left = this.m_Player1.Right;
            this.m_Player1Score.Text = "0";

            this.m_Player2 = new Label();
            this.m_Player2.Height = 25;
            this.m_Player2.Width = 60;
            this.m_Player2.Top = location.Y;
            this.m_Player2.Left = this.m_Player1Score.Right + 10;

            this.m_Player2Score = new Label();
            this.m_Player2Score.Height = 25;
            this.m_Player2Score.Width = 10;
            this.m_Player2Score.Top = location.Y;
            this.m_Player2Score.Left = this.m_Player2.Right;
            this.m_Player2Score.Text = "0";

            this.Controls.Add(m_Player1);
            this.Controls.Add(m_Player1Score);
            this.Controls.Add(m_Player2);
            this.Controls.Add(m_Player2Score);

        }

        #endregion
    }
}