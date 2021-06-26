using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Player
    {
        // **Fields**
        private readonly UserPlayer r_UserPlayer = null;
        private readonly PcPlayer r_PcPlayer = null;
        private readonly GameOperation.ePlayerType r_PlayerType;

        // **CTOR**
        internal Player(GameOperation.ePlayerSigns i_Sign, GameOperation.ePlayerType i_PlayerType)
        {
            if (i_PlayerType == GameOperation.ePlayerType.User)
            {
                r_UserPlayer = new UserPlayer(i_Sign);
            }
            else
            {
                r_PcPlayer = new PcPlayer(i_Sign);
            }

            r_PlayerType = i_PlayerType;
        }

        // **Properties
        // All Properties get and set data of the relevant type
        internal GameOperation.ePlayerSigns PlayerSign
        {
            get
            {
                GameOperation.ePlayerSigns playerSign;

                if (r_PlayerType == GameOperation.ePlayerType.User)
                {
                    playerSign = r_UserPlayer.PlayerSign;
                }
                else
                {
                    playerSign = r_PcPlayer.PlayerSign;
                }

                return playerSign;
            }
        }

        public int PlayerId
        {
            get
            {
                int playerId;

                if (r_PlayerType == GameOperation.ePlayerType.User)
                {
                    playerId = r_UserPlayer.PlayerId;
                }
                else
                {
                    playerId = r_PcPlayer.PlayerId;
                }

                return playerId;
            }
        }

        public int NumOfWins
        {
            get
            {
                int numOfWins;

                if (r_PlayerType == GameOperation.ePlayerType.User)
                {
                    numOfWins = r_UserPlayer.NumOfWins;
                }
                else
                {
                    numOfWins = r_PcPlayer.NumOfWins;
                }

                return numOfWins;
            }

            set
            {
                if (r_PlayerType == GameOperation.ePlayerType.User)
                {
                    r_UserPlayer.NumOfWins = value;
                }
                else
                { 
                    r_PcPlayer.NumOfWins = value;
                }
            }
        }

        public GameOperation.ePlayerType PlayerType
        {
            get
            {
                return r_PlayerType;
            }
        }

        // **Methods**
        // Operate UserPlayer MakeMove, Return false in case of invalid move
        internal bool MakeMove(GameTable i_GameTable, Point i_Point)
        {
            bool moveSucceeded = false;

            if (r_UserPlayer != null)
            {
                moveSucceeded = r_UserPlayer.MakeMove(i_GameTable, i_Point);
            }

            return moveSucceeded;
        }

        // Operate PcPlayer MakeMove, Return false in case of invalid move
        internal bool MakeMove(GameTable i_GameTable)
        {
            bool moveSucceeded = false;

            if (r_PcPlayer != null)
            {
                moveSucceeded = r_PcPlayer.MakeMove(i_GameTable);
            }

            return moveSucceeded;
        }
    }
}