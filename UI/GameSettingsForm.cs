using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class GameSettingsForm : Form
    {
        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked == true)
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.Text = string.Empty;
            }
            else
            {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "[Computer]";
            }
        }

        // Game table must be a square matrix - this method change cols value to be same as rows
        private void numericUpDownRows_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownCols.Value = (sender as NumericUpDown).Value;
        }

        // Game table must be a square matrix - this method change rows value to be same as cols
        private void numericUpDownCols_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownRows.Value = (sender as NumericUpDown).Value;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (textBoxPlayer1.Text.Length == 0)
            {
                MessageBox.Show("Error - Player 1 name isn't valid.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBoxPlayer2.Text.Length == 0)
            {
                MessageBox.Show("Error - Player 2 name isn't valid.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                GameTableForm gameForm = new GameTableForm((int)numericUpDownRows.Value, textBoxPlayer1.Text, !checkBoxPlayer2.Checked, textBoxPlayer2.Text);
                this.Visible = false;
                gameForm.ShowDialog();
                this.Close();
            }
        }
    }
}
