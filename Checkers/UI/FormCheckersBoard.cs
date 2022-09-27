using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ui
{
    public delegate void PlayerEnterMove(string i_From, string i_To);

    public class FormCheckersBoard : Form
    {
        private readonly int         r_BoardSize;
        private Label                m_LabelPlayer1;
        private Label                m_LabelPlayer2;
        private ButtonBoardSquare[,] m_ButtonsBoard;
        private ButtonBoardSquare    m_FromPosition;
        private Label                m_LabelPlayer1Score;
        private Label                m_LabelPlayer2Score;
        private bool                 m_IsFirstClick;

        public event PlayerEnterMove PlayerEnterMove;

        public FormCheckersBoard(int i_BoardSize, string i_InitializedBoard, string i_Player1Name, string i_Player2Name)
        {
            this.r_BoardSize = i_BoardSize;
            this.m_FromPosition = null;
            this.m_IsFirstClick = true;
            this.m_LabelPlayer1 = new Label();
            this.m_LabelPlayer2 = new Label();
            this.m_LabelPlayer1Score = new Label();
            this.m_LabelPlayer2Score = new Label();
            this.m_LabelPlayer1.Text = string.Format(i_Player1Name + ":");
            this.m_LabelPlayer2.Text = string.Format(i_Player2Name + ":");
            this.m_LabelPlayer1Score.Text = "0";
            this.m_LabelPlayer2Score.Text = "0";
            this.m_LabelPlayer1.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            InitializeComponent();
            InitializeBoard(i_InitializedBoard);
        }

        internal void UpdateBoard(string i_UpdatedBoard)
        {
            for (int row = 0, j = 0; row < this.r_BoardSize; row++)
            {
                for (int col = 0; col < this.r_BoardSize; col++)
                {
                    this.m_ButtonsBoard[row, col].Text = i_UpdatedBoard[j++].ToString();
                }
            }
        }

        private void InitializeBoard(string i_InitializedBoard)
        {
            char rowLetter = 'a';
            char colLetter = 'A';

            this.m_ButtonsBoard = new ButtonBoardSquare[this.r_BoardSize, this.r_BoardSize];
            for (int row = 0, j = 0; row < this.r_BoardSize; row++)
            {
                for (int col = 0; col < this.r_BoardSize; col++)
                {
                    m_ButtonsBoard[row, col] = new ButtonBoardSquare(rowLetter.ToString(), (colLetter++).ToString());
                    m_ButtonsBoard[row, col].Text = i_InitializedBoard[j++].ToString();
                    m_ButtonsBoard[row, col].Location = new Point(20 + (50 * col), 40 + (50 * row));
                    m_ButtonsBoard[row, col].Size = new Size(50, 50);
                    if(row % 2 == col % 2)
                    {
                        m_ButtonsBoard[row, col].BackColor = Color.Black;
                        m_ButtonsBoard[row, col].Enabled = false;
                    }
                    else
                    {
                        m_ButtonsBoard[row, col].BackColor = Color.White;
                    }

                    m_ButtonsBoard[row, col].Click += new System.EventHandler(this.buttonsBoard_Click);
                    this.Controls.Add(m_ButtonsBoard[row, col]);
                }

                rowLetter++;
                colLetter = 'A';
            }
        }

        internal void UpdateScoreBoard(int i_Player1Score, int i_Player2Score)
        {
            this.m_LabelPlayer1Score.Text = i_Player1Score.ToString();
            this.m_LabelPlayer2Score.Text = i_Player2Score.ToString();
        }

        internal void HighlightPlayerTurn(bool i_IsFirstPlayerTurn)
        {
            if(i_IsFirstPlayerTurn)
            {
                this.m_LabelPlayer1.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                this.m_LabelPlayer2.Font = new Font(Label.DefaultFont, FontStyle.Regular);
            }
            else
            {
                this.m_LabelPlayer2.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                this.m_LabelPlayer1.Font = new Font(Label.DefaultFont, FontStyle.Regular);
            }
        }

        private void buttonsBoard_Click(object sender, EventArgs e)
        {
            if(this.m_IsFirstClick)
            {
                this.m_IsFirstClick = false;
                (sender as ButtonBoardSquare).BackColor = Color.Aqua;
                this.m_FromPosition = sender as ButtonBoardSquare;
            }
            else
            {
                this.m_IsFirstClick = true;
                this.m_FromPosition.BackColor = Color.White;
                if(!this.m_FromPosition.Position.Equals((sender as ButtonBoardSquare).Position))
                {
                    this.PlayerEnterMove.Invoke(this.m_FromPosition.Position, (sender as ButtonBoardSquare).Position);
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(40 + (50 * this.r_BoardSize), 70 + (50 * this.r_BoardSize));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormCheckersBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Damka";
            this.ResumeLayout(false);
            this.m_LabelPlayer1.AutoSize = true;
            this.m_LabelPlayer1.Location = new System.Drawing.Point(70, 10);
            this.m_LabelPlayer1.Size = new System.Drawing.Size(64, 17);
            this.Controls.Add(m_LabelPlayer1);
            this.m_LabelPlayer2.AutoSize = true;
            this.m_LabelPlayer2.Location = new System.Drawing.Point(20 + ((this.r_BoardSize - 2) * 50), 10);
            this.m_LabelPlayer2.Size = new System.Drawing.Size(64, 17);
            this.Controls.Add(m_LabelPlayer2);
            this.m_LabelPlayer1Score.AutoSize = true;
            this.m_LabelPlayer1Score.Location = new System.Drawing.Point(this.m_LabelPlayer1.Right + 2, 10);
            this.m_LabelPlayer1Score.Size = new System.Drawing.Size(64, 17);
            this.Controls.Add(m_LabelPlayer1Score);
            this.m_LabelPlayer2Score.AutoSize = true;
            this.m_LabelPlayer2Score.Location = new System.Drawing.Point(this.m_LabelPlayer2.Right + 2, 10);
            this.m_LabelPlayer2Score.Size = new System.Drawing.Size(64, 17);
            this.Controls.Add(m_LabelPlayer2Score);
        }
    }
}
