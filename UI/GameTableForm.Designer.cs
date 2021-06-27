using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    public partial class GameTableForm
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
            int len = (r_TableSize * k_ButtonSize) + (r_TableSize - 1) * 5 + 10 * 2;
            this.ClientSize = new System.Drawing.Size(len, len + 40);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TicTacToeMisere";

            // Initialize Game Boxes
            Point location = new Point(10, 10);

            for (int i = 0; i < this.r_TableButtons.Count; i++)
            {
                for (int j = 0; j < this.r_TableButtons[i].Count; j++)
                {
                    this.r_TableButtons[i][j].Location = location;
                    location.X += r_TableButtons[i][j].Width + 5;
                }

                location.X = 10;
                location.Y += r_TableButtons[i][0].Height + 5;
            }

            // Initialize Score
            location.Y += 10;

            this.labelPlayer1 = new Label();
            this.labelPlayer1.Height = 25;
            this.labelPlayer1.Width = 60;
            this.labelPlayer1.Top = location.Y;
            this.labelPlayer1.Left = this.ClientSize.Width / 2 - 70;
            this.labelPlayer1.Font = new Font(Label.DefaultFont, FontStyle.Bold) ;

            this.labelPlayer1Score = new Label();
            this.labelPlayer1Score.Height = 25;
            this.labelPlayer1Score.Width = 10;
            this.labelPlayer1Score.Top = location.Y;
            this.labelPlayer1Score.Left = this.labelPlayer1.Right;
            this.labelPlayer1Score.Text = "0";

            this.labelPlayer2 = new Label();
            this.labelPlayer2.Height = 25;
            this.labelPlayer2.Width = 60;
            this.labelPlayer2.Top = location.Y;
            this.labelPlayer2.Left = this.labelPlayer1Score.Right + 10;

            this.labelPlayer2Score = new Label();
            this.labelPlayer2Score.Height = 25;
            this.labelPlayer2Score.Width = 10;
            this.labelPlayer2Score.Top = location.Y;
            this.labelPlayer2Score.Left = this.labelPlayer2.Right;
            this.labelPlayer2Score.Text = "0";

            this.Controls.Add(labelPlayer1);
            this.Controls.Add(labelPlayer1Score);
            this.Controls.Add(labelPlayer2);
            this.Controls.Add(labelPlayer2Score);

        }

        #endregion
    }
}