using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Logic
{
    internal class UserPlayer
    {
        // **Fields**
        private readonly GameOperation.ePlayerSigns r_PlayerSign;
        private readonly int r_PlayerId;
        private int m_NumOfWins;

        // **CTOR**
        internal UserPlayer(GameOperation.ePlayerSigns i_Sign)
        {
            r_PlayerSign = i_Sign;
            r_PlayerId = (int)i_Sign;
            m_NumOfWins = 0;
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
        // Get the gameTable and desired point, Return false in case of invalid move
        internal bool MakeMove(GameTable i_GameTable, Point i_Point)
        {
            return i_GameTable.InsertToTable(i_Point, PlayerSign);
        }
    }
}
