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
    class ButtonGameBox : Button
    {
        private Point m_BoxId;
        public ButtonGameBox(int i_Size, int i_X, int i_Y)
        {
            m_BoxId.X = i_X;
            m_BoxId.Y = i_Y;
            this.Width = i_Size;
            this.Height = i_Size;
            this.Enabled = true;
        }

        public Point BoxId { get => m_BoxId; }
    }
}
