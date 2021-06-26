using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Logic
{
    internal class PcPlayer
    {
        // **Fields**
        private readonly GameOperation.ePlayerSigns r_PlayerSign;
        private readonly int r_PlayerId;
        private int m_NumOfWins;
        private Random m_pointGenerator;

        // **CTOR**
        internal PcPlayer(GameOperation.ePlayerSigns i_Sign)
        {
            r_PlayerSign = i_Sign;
            r_PlayerId = (int)i_Sign;
            m_NumOfWins = 0;
            m_pointGenerator = new Random();
        }

        // **Properties
        internal GameOperation.ePlayerSigns PlayerSign
        {
            get
            {
                return r_PlayerSign;
            }
        }

        public int PlayerId
        {
            get
            {
                return r_PlayerId;
            }
        }

        public int NumOfWins
        {
            get
            {
                return m_NumOfWins;
            }

            set
            {
                m_NumOfWins = value;
            }
        }

        // **Methods**
        // Get the gameTable, generate random Point until its a free spot.
        internal bool MakeMove(GameTable i_GameTable)
        {
            Point currPoint = new Point(m_pointGenerator.Next(i_GameTable.Size - 1), m_pointGenerator.Next(i_GameTable.Size - 1));

            // Genereate new point until its possible to use this point
            while (i_GameTable.InsertToTable(currPoint, PlayerSign) == false)
            {
                currPoint.X = m_pointGenerator.Next(i_GameTable.Size);
                currPoint.Y = m_pointGenerator.Next(i_GameTable.Size);
            }

            return true;
        }
    }
}
