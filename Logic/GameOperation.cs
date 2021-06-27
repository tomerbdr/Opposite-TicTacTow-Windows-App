using System;
using System.Reflection;
using System.Drawing;

namespace Logic
{
    public delegate void CellChangedDelegate(Point i_Point, char i_Sign);

    public class GameOperation
    {
        public enum ePlayerType
        {
            User = 1, Pc
        }

        internal enum ePlayerSigns
        {
            Empty, X, O
        }

        // **Fields**
        private const int k_MinSize = 3;
        private const int k_MaxSize = 9;
        private readonly GameTable r_GameTable = null;
        private readonly Player r_PlayerOne = null; 
        private readonly int r_TableSize;
        private Player m_PlayerTwo = null;
        private Player m_CurrentPlayerToPlay = null;
        private Player m_Winner = null;
        private bool m_IsGameOver = false;
        private bool m_IsSecondPlayerAI;

        // **CTOR**
        public GameOperation(int i_TableSize, CellChangedDelegate i_OnCellChange)
        {
            r_GameTable = new GameTable(i_TableSize);
            r_GameTable.CellChanged += i_OnCellChange;
            r_TableSize = i_TableSize;
            r_PlayerOne = new Player(ePlayerSigns.X,ePlayerType.User);
            m_CurrentPlayerToPlay = r_PlayerOne;
            m_IsGameOver = false;
        }

        // **Properties**
        public static int MinSize
        {
            get
            {
                return k_MinSize;
            }
        }

        public static int MaxSize
        {
            get
            {
                return k_MaxSize;
            }
        }

        public bool IsGameOver
        {
            get
            {
            return m_IsGameOver;
            }
        }

        public Player Winner
        {
            get
            {
                return m_Winner;
            }
        }

        public Player PlayerOne
        {
            get
            {
                return r_PlayerOne;
            }
        }

        public Player PlayerTwo
        {
            get
            {
                return m_PlayerTwo;
            }
        }

        public int TableSize
        {
            get
            {
                return r_TableSize;
            }
        }

        internal GameTable GameTable
        {
            get
            {
                return r_GameTable;
            }
        }

        public bool IsSecondPlayerPC
        {
            get
            {
                return m_IsSecondPlayerAI;
            }
        }

        public Player CurrentPlayerToPlay
        {
            get
            {
                return m_CurrentPlayerToPlay;
            }
        }

        // **Methods**
        // Function get enum which represent User chooise for second player: User/AI, and Create instance of the desired Player type.
        public void SetPlayerTwo(ePlayerType i_SecondPlayer)
        {
            if (i_SecondPlayer == ePlayerType.User)
            {
                m_PlayerTwo = new Player(ePlayerSigns.O,ePlayerType.User);
                m_IsSecondPlayerAI = false;
            }
            else
            {
                m_PlayerTwo = new Player(ePlayerSigns.O, ePlayerType.Pc);
                m_IsSecondPlayerAI = true;
            }
        }

        // Function get Point which represent a position in the table that the Player choose
        // Or don't get any parameter to operate PC Player 
        // The function return false in case of occupied cell or point position out of range.
        public bool PlayTurn(Point i_Point = new Point())
        {
            bool validTurn = false;

            if (m_CurrentPlayerToPlay.PlayerType == ePlayerType.User)
            { 
                validTurn = m_CurrentPlayerToPlay.MakeMove(r_GameTable,i_Point); // UserPlayer
            }
            else
            {
                validTurn = m_CurrentPlayerToPlay.MakeMove(r_GameTable); // PcPlayer
            }

            if (validTurn)
            {
                if (r_GameTable.LoseCheck(m_CurrentPlayerToPlay.PlayerSign) == true)
                {
                    endGame(m_CurrentPlayerToPlay); // Current player lost
                }
                else if (r_GameTable.LeftSpots == 0)
                {
                    endGame(null); // passing null as Loser Player <==> TIE
                }
                else
                {
                    m_CurrentPlayerToPlay = otherPlayer(); // Set other player as current player for next turn
                }
            }

            return validTurn; // Return false in case of occupied cell or point position out of range.
        }

        // Get reference to player who quit the game, set other as winner.
        public void SomeoneQuit(Player i_QuitPlayer)
        {
            endGame(i_QuitPlayer);
        }

        // Function get reference to Player who lost and set the other player as a winner.
        // In case of tie - function get null as player.
        private void endGame(Player i_LoserPlayer)
        {
            if (i_LoserPlayer == null)
            {
                m_Winner = null; // Tie - no winner.
            }
            else
            {
                m_Winner = otherPlayer(); // Set other player as winner.
                m_Winner.NumOfWins++;
            }

            m_IsGameOver = true;
        }

        // Function return reference to the other player from the CurrentPlayerToPlay
        private Player otherPlayer()
        {
            if (m_CurrentPlayerToPlay == r_PlayerOne)
            {
               return m_PlayerTwo;
            }
            else
            {
               return r_PlayerOne;
            }
        }
        
        // Function reset all fields that relevant to start a new round.
        public void ResetGame()
        {
            m_IsGameOver = false;
            r_GameTable.ResetTable();
            m_Winner = null;
            m_CurrentPlayerToPlay = r_PlayerOne;
        }
    }
}
