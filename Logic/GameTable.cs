using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class GameTable
    {
        // **Fields**
        private readonly GameOperation.ePlayerSigns[,] r_GameTable;
        private readonly int r_Size;
        private int m_LeftSpots;
        private Point m_LastPointUpdated;

        public event CellChangedDelegate CellChanged;

        // **CTOR**
        internal GameTable(int i_Size)
        {
            r_Size = i_Size;
            m_LeftSpots = i_Size * i_Size;
            r_GameTable = new GameOperation.ePlayerSigns[i_Size, i_Size];
            ResetTable();
        }

        // **Properties**
        internal int Size
        {
            get
            {
                return r_Size;
            }
        }

        internal int LeftSpots
        {
            get
            {
                return m_LeftSpots;
            }

            set
            {
                m_LeftSpots = value;
            }
        }

        // **Methods**
        // Function get a point and sign to insert to the point on table.
        // Return false in case of occupied point or point out of range, else return true and update the table.
        internal bool InsertToTable(Point i_DesiredPoint, GameOperation.ePlayerSigns i_SignToInsert) 
        {
            bool isPointValid = false;

            if (isPointBelongToTable(i_DesiredPoint) && !isPointOccupied(i_DesiredPoint))
            {
                r_GameTable[i_DesiredPoint.X, i_DesiredPoint.Y] = i_SignToInsert;
                m_LastPointUpdated = i_DesiredPoint;
                isPointValid = true;
                m_LeftSpots--;
                CellChanged.Invoke(i_DesiredPoint, i_SignToInsert == GameOperation.ePlayerSigns.X ? 'X' : 'O');
            }
            
            return isPointValid;
        }

        // Function get sign of player, and check for all possible streaks from the last point that updated.
        // Return true in case of streak, else false.
        internal bool LoseCheck(GameOperation.ePlayerSigns i_SignToCheck)
         {
             bool isLose = false;

             if (isColStreak(i_SignToCheck) == true)
             {
                 isLose = true;
             }
             else if (isRowStreak(i_SignToCheck) == true)
             {
                 isLose = true;
             }
             else if (isDiagonalsStreak(i_SignToCheck) == true)
             {
                 isLose = true;
             }

            return isLose;
        }

        // 3 below methods return true in case of streak of i_Sign from the last position updated.
        private bool isRowStreak(GameOperation.ePlayerSigns i_Sign)
         {
             int rowToCheck = m_LastPointUpdated.X;
             bool isStreak = true;

             for (int i = 0; i < r_Size; i++)
             {
                 if (r_GameTable[rowToCheck,i] != i_Sign)
                 {
                     isStreak = false;
                     break;
                 }
             }

             return isStreak;
         }

        private bool isColStreak(GameOperation.ePlayerSigns i_Sign)
         {
             int colToCheck = m_LastPointUpdated.Y;
             bool isStreak = true;

             for (int i = 0; i < r_Size; i++)
             {
                 if (r_GameTable[i, colToCheck] != i_Sign)
                 {
                     isStreak = false;
                     break;
                 }
             }

             return isStreak;
         }

        private bool isDiagonalsStreak(GameOperation.ePlayerSigns i_Sign)
         {
            // check first diagonal
            bool isFirstDiagonalStreak = isBelongToFirstDiagonal(m_LastPointUpdated);
            bool isSecondDiagonalStreak = isBelongToSecondDiagonal(m_LastPointUpdated);

            for (int i = 0; isFirstDiagonalStreak != false && i < r_Size; i++)
            {
                 if (r_GameTable[i, i] != i_Sign)
                 {
                     isFirstDiagonalStreak = false;
                 }
            }

            // Check the second diagonal if the first test failed.
            if (isFirstDiagonalStreak == false)
            {
                for (int i = 0; isSecondDiagonalStreak != false && i < r_Size; i++)
                {
                    if (r_GameTable[(r_Size - 1) - i, i] != i_Sign)
                    {
                        isSecondDiagonalStreak = false;
                    }
                }
            }

            return isFirstDiagonalStreak || isSecondDiagonalStreak;
         }

        // Return true if the point is located on the First diagonal -> (First diagonal ==  ** \ **)
        private bool isBelongToFirstDiagonal(Point i_Point)
         {
             return i_Point.X == i_Point.Y;
         }

        // Return true if the point is located on the Second diagonal -> (Second diagonal ==  ** / **)
        private bool isBelongToSecondDiagonal(Point i_Point)
         {
             return (i_Point.X + i_Point.Y) == (r_Size - 1);
         }

        // Return true if the Point represent a cell in the table.
        private bool isPointBelongToTable(Point i_Point)
        {
            return i_Point.X >= 0 && i_Point.X < r_Size && i_Point.Y >= 0 && i_Point.Y < r_Size;
        }

        // Return true if any player already choose the Point in the table.
        private bool isPointOccupied(Point i_Point)
        {
            return r_GameTable[i_Point.X, i_Point.Y] != GameOperation.ePlayerSigns.Empty;
        }

        // Set all table cells as Empty, and reset LeftSpots
        internal void ResetTable()
        {
            for (int indexRow = 0; indexRow < r_Size; indexRow++)
            {
                for (int indexColumn = 0; indexColumn < r_Size; indexColumn++)
                {
                    r_GameTable[indexRow, indexColumn] = GameOperation.ePlayerSigns.Empty;
                }
            }

            m_LeftSpots = r_Size * r_Size;
        }
    }
}
