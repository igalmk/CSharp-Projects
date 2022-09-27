using System.Windows.Forms;

namespace Ui
{
    public class ButtonBoardSquare : Button
    {
        private string m_Row;
        private string m_Col;

        public ButtonBoardSquare(string i_Row, string i_Col)
        {
            this.m_Col = i_Col;
            this.m_Row = i_Row;
        }

        public string Position
        {
            get
            {
                return m_Col.ToUpper() + m_Row;
            }
        }
    }
}
