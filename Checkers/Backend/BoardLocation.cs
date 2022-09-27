using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend
{
    internal struct BoardLocation
    {
        private int m_Col;
        private int m_Row;

        internal BoardLocation(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        internal int Col
        {
            get
            {
                return m_Col;
            }

            set
            {
                m_Col = value;
            }
        }

        internal int Row
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }
    }
}
