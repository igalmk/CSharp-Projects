using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend
{
    internal class MoveParameters
    {
        private BoardLocation m_From;
        private BoardLocation m_To;

        internal MoveParameters(BoardLocation i_From, BoardLocation i_To)
        {
            m_From = new BoardLocation(i_From.Row, i_From.Col);
            m_To = new BoardLocation(i_To.Row, i_To.Col);
        }

        internal BoardLocation From
        {
            get
            {
                return m_From;
            }

            set
            {
                m_From.Row = value.Row;
                m_From.Col = value.Col;
            }
        }

        internal BoardLocation To
        {
            get
            {
                return m_To;
            }

            set
            {
                m_To.Row = value.Row;
                m_To.Col = value.Col;
            }
        }
    }
}
