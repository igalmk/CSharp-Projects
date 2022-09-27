using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend
{
    public class GameEngine
    {
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private Board m_Board;
        private bool m_IsGameOver;
        private string m_CurrentWinnerName;

        public GameEngine(int i_BoardSize, bool i_IsPlayerVsPlayer, string i_Player1Name, string i_Player2Name = "Computer")
        {
            bool isHuman = true;

            this.m_IsGameOver = false;
            this.m_CurrentWinnerName = null;
            this.m_Board = new Board(i_BoardSize);
            r_Player1 = new Player(i_Player1Name, 'X', isHuman);
            if(i_IsPlayerVsPlayer)
            {
                r_Player2 = new Player(i_Player2Name, 'O', isHuman);
            }
            else
            {
                r_Player2 = new Player(i_Player2Name, 'O', !isHuman);
            }
        }

        private enum eGameMode
        {
            PlayerVsPlayer = 0,
            PlayerVsComputer = 1,
        }

        public string WinnerName
        {
            get
            {
                return this.m_CurrentWinnerName;
            }
        }

        public string Player1Name
        {
            get
            {
                return this.r_Player1.Name;
            }
        }

        public bool IsPlayer1Human
        {
            get
            {
                return this.r_Player1.IsHuman;
            }
        }

        public bool IsGameOver
        {
            get
            {
                return this.m_IsGameOver;
            }
        }

        public string Player2Name
        {
            get
            {
                return this.r_Player2.Name;
            }
        }

        public bool IsPlayer2Human
        {
            get
            {
                return this.r_Player2.IsHuman;
            }
        }

        public char Player1Marker
        {
            get
            {
                return this.r_Player1.Marker;
            }
        }

        public char Player2Marker
        {
            get
            {
                return this.r_Player2.Marker;
            }
        }

        public string GetBoardAsString()
        {
            return this.m_Board.ToString();
        }

        public bool Play(string i_From, string i_To)
        {
            bool isValidPlayMove = false;

            if (!this.IsFirstPlayerTurn() && !this.r_Player2.IsHuman)
            {
                isValidPlayMove = true;
                computerPlay();
            }
            else
            {
                isValidPlayMove = this.m_Board.Play(i_From, i_To);
            }

            eGameStatus eGameStatus = this.m_Board.CheckIfGameOver();

            if(eGameStatus != eGameStatus.Ongoing)
            {
                this.m_IsGameOver = true;
                updateScore(eGameStatus);
            }

            return isValidPlayMove;
        }

        private void computerPlay()
        {
            string from, to;

            Ai.GetRandomMove(this.m_Board.GetAllValidMovesForComputer(), out from, out to);
            this.m_Board.Play(from, to);
        }

        private void updateScore(eGameStatus i_GameStatus)
        {
            switch (i_GameStatus)
            {
                case eGameStatus.Player1Won:
                {
                 addPointsForPlayer1();
                 break;
                }

                case eGameStatus.Player2Won:
                {
                 addPointsForPlayer2();
                 break;
                }

                case eGameStatus.Tie:
                {
                 this.m_CurrentWinnerName = "Tie";
                 break;
                }
            }
        }

        private void addPointsForPlayer1()
        {
            this.r_Player1.UpdateScore((this.m_Board.GetKingAmountPlayer1() * 4) + this.m_Board.GetRegularPieceAmountPlayer1()
                                     - ((this.m_Board.GetKingAmountPlayer2() * 4) + this.m_Board.GetRegularPieceAmountPlayer2()));
            this.m_CurrentWinnerName = this.r_Player1.Name;
        }

        private void addPointsForPlayer2()
        {
            this.r_Player2.UpdateScore((this.m_Board.GetKingAmountPlayer2() * 4) + this.m_Board.GetRegularPieceAmountPlayer2()
                         - ((this.m_Board.GetKingAmountPlayer1() * 4) + this.m_Board.GetRegularPieceAmountPlayer1()));
            this.m_CurrentWinnerName = this.r_Player2.Name;
        }

        public bool IsFirstPlayerTurn()
        {
            return this.m_Board.IsFirstPlayerTurn;
        }

        public string GetLastMove()
        {
            return this.m_Board.LastMove;
        }

        public int GetPlayer1Score()
        {
            return this.r_Player1.Score;
        }

        public int GetPlayer2Score()
        {
            return this.r_Player2.Score;
        }

        public void NewRound()
        {
            int boardSize = this.m_Board.BoardSize;

            this.m_Board = new Board(boardSize);
            m_IsGameOver = false;
            m_CurrentWinnerName = null;
        }

        public bool IsCurrentPlayerOnStreak()
        {
            return this.m_Board.IsOnStreak;
        }

        public void PlayerQuitGame()
        {
            int player1Score, player2Score;

            player1Score = (this.m_Board.GetKingAmountPlayer1() * 4) + this.m_Board.GetRegularPieceAmountPlayer1();
            player2Score = (this.m_Board.GetKingAmountPlayer2() * 4) + this.m_Board.GetRegularPieceAmountPlayer2();
            this.m_IsGameOver = true;
            if(this.m_Board.IsFirstPlayerTurn && (player2Score > player1Score))
            {
                this.r_Player2.UpdateScore(player2Score - player1Score);
            }
            else if(!this.m_Board.IsFirstPlayerTurn && (player2Score < player1Score))
            {
                this.r_Player1.UpdateScore(player1Score - player2Score);
            }
        }
    }
}