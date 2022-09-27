using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend
{
    internal class Ai
    {
        private static Random m_RandomeNumber = new Random();

        internal static void GetRandomMove(LinkedList<MoveParameters> i_Moves, out string o_From, out string o_To)
        {
            MoveParameters selectedMove;
            char           fromRow, fromCol, toRow, toCol;

            selectedMove = i_Moves.ElementAt(m_RandomeNumber.Next(0, i_Moves.Count));
            fromRow = toRow = 'a';
            fromCol = toCol = 'A';
            incrementChar(selectedMove.From.Row, ref fromRow);
            incrementChar(selectedMove.From.Col, ref fromCol);
            incrementChar(selectedMove.To.Row, ref toRow);
            incrementChar(selectedMove.To.Col, ref toCol);

            o_From = fromCol.ToString() + fromRow.ToString();
            o_To = toCol.ToString() + toRow.ToString();
        }

        private static void incrementChar(int i_IncrementAmount, ref char io_Char)
        {
            for(int i = 0; i < i_IncrementAmount; i++)
            {
                io_Char++;
            }
        }
    }
}
