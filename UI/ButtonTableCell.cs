using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    internal class ButtonTableCell : Button
    {
        private readonly Point r_CellId;

        internal ButtonTableCell(int i_Size, int i_X, int i_Y)
        {
            r_CellId.X = i_X;
            r_CellId.Y = i_Y;
            this.Width = i_Size;
            this.Height = i_Size;
            this.Enabled = true;
        }

        internal Point CellId { get => r_CellId; }
    }
}
