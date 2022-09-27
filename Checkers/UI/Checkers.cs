using System;
using System.Windows.Forms;
using Backend;

namespace Ui
{
    public class Checkers
    {
        private FormCheckersSettings m_FormCheckersSettings;
        private FormCheckersBoard    m_FormCheckersBoard;
        private GameEngine           m_GameEngine;
        private Timer                m_Timer;

        public void Run()
        {
            this.m_Timer = new Timer();
            m_Timer.Interval = 1500;
            this.m_Timer.Tick += new System.EventHandler(this.timer_Tick);
            m_FormCheckersSettings = new FormCheckersSettings();
            m_FormCheckersSettings.ShowDialog();
            if(m_FormCheckersSettings.ClosedByDoneButton)
            {
                m_GameEngine = new GameEngine(this.m_FormCheckersSettings.BoardSize, this.m_FormCheckersSettings.IsVsHuman,
                                              this.m_FormCheckersSettings.Player1Name, this.m_FormCheckersSettings.Player2Name);
                this.m_FormCheckersBoard = new FormCheckersBoard(this.m_FormCheckersSettings.BoardSize, m_GameEngine.GetBoardAsString(),
                                                  this.m_FormCheckersSettings.Player1Name, this.m_FormCheckersSettings.Player2Name);
                this.m_FormCheckersBoard.PlayerEnterMove += this.formCheckersBoard_PlayerEnterMove;
                this.m_FormCheckersBoard.ShowDialog();
            }
        }

        private void formCheckersBoard_PlayerEnterMove(string i_From, string i_To)
        {
            bool isValidMove;

            isValidMove = this.m_GameEngine.Play(i_From, i_To);
            if(isValidMove)
            {
                this.m_FormCheckersBoard.UpdateBoard(this.m_GameEngine.GetBoardAsString());
            }
            else
            {
                MessageBox.Show("Invalid move!", "Damka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(isValidMove && !this.m_GameEngine.IsGameOver && !this.m_GameEngine.IsPlayer2Human)
            {
                while (!this.m_GameEngine.IsFirstPlayerTurn())
                {
                    this.m_GameEngine.Play(i_From, i_To);
                    this.m_Timer.Start();
                }
            }

            if(this.m_GameEngine.IsGameOver)
            {
                gameOverOccured();
            }

            this.m_FormCheckersBoard.HighlightPlayerTurn(this.m_GameEngine.IsFirstPlayerTurn());
        }

        private void gameOverOccured()
        {
            string message;
            DialogResult yesOrNoFromMessageBox;

            this.m_FormCheckersBoard.UpdateScoreBoard(this.m_GameEngine.GetPlayer1Score(), this.m_GameEngine.GetPlayer2Score());
            if (this.m_GameEngine.WinnerName.Equals("Tie"))
            {
                message = string.Format("Tie!{0}Another Round?", Environment.NewLine);
                yesOrNoFromMessageBox = MessageBox.Show(message, "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
            else
            {
                message = string.Format("{0} Won!{1}Another Round?", this.m_GameEngine.WinnerName, Environment.NewLine);
                yesOrNoFromMessageBox = MessageBox.Show(message, "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }

            if(yesOrNoFromMessageBox == DialogResult.Yes)
            {
                this.m_GameEngine.NewRound();
                this.m_FormCheckersBoard.UpdateBoard(this.m_GameEngine.GetBoardAsString());
            }
            else
            {
                this.m_FormCheckersBoard.Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.m_Timer.Stop();
            this.m_FormCheckersBoard.UpdateBoard(this.m_GameEngine.GetBoardAsString());
        }
    }
}
