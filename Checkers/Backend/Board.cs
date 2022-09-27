using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    internal class Board
    {
        private readonly char[,] r_Board;
        private readonly int     r_SizeOfBoard;
        private bool             m_FirstPlayerTurn;
        private bool             m_IsOnAStreak;
        private bool             m_IsThereWinner;
        private string           m_LastMove;
        private BoardLocation    m_LastMovingPiece;

        internal Board(int i_SizeOfBoard)
        {
            r_Board = new char[i_SizeOfBoard, i_SizeOfBoard];
            r_SizeOfBoard = i_SizeOfBoard;
            m_FirstPlayerTurn = true;
            m_IsOnAStreak = false;
            m_IsThereWinner = false;
            m_LastMove = null;
            initializeBoard();
        }

        internal bool IsWinner
        {
            get
            {
                return this.m_IsThereWinner;
            }
        }

        internal bool IsOnStreak
        {
            get
            {
                return this.m_IsOnAStreak;
            }
        }

        internal int BoardSize
        {
            get
            {
                return this.r_SizeOfBoard;
            }
        }

        internal bool IsFirstPlayerTurn
        {
            get
            {
                return this.m_FirstPlayerTurn;
            }
        }

        internal string LastMove
        {
            get
            {
                return this.m_LastMove;
            }
        }

        private void initializeBoard()
        {
            int xRowIndex = ((this.r_SizeOfBoard - 2) / 2) + 2;

            for (int i = 0; i < (this.r_SizeOfBoard - 2) / 2; i++)
            {
                for (int j = 0; j < this.r_SizeOfBoard; j++)
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        this.r_Board[i, j] = ' ';
                    }
                    else
                    {
                        this.r_Board[i, j] = 'O';
                    }
                }
            }

            for(int i = 0; i < 2; i++)
            {
                for (int j = 0; j < this.r_SizeOfBoard; j++)
                {
                    this.r_Board[i + ((this.r_SizeOfBoard - 2) / 2), j] = ' ';
                }
            }

            for (int i = 0; i < (this.r_SizeOfBoard - 2) / 2; i++)
            {
                for (int j = 0; j < this.r_SizeOfBoard; j++)
                {
                    if (((i + xRowIndex) % 2 == 0 && j % 2 == 0) || ((i + xRowIndex) % 2 != 0 && j % 2 != 0))
                    {
                        this.r_Board[i + xRowIndex, j] = ' ';
                    }
                    else
                    {
                        this.r_Board[i + xRowIndex, j] = 'X';
                    }
                }
            }
        }

        internal eGameStatus CheckIfGameOver()
        {
            eGameStatus eGameStatus;
            bool player1CanMove = false;
            bool player2CanMove = false;
            bool playerTurnHolder = this.m_FirstPlayerTurn;
            bool playerStreakHolder = this.m_IsOnAStreak;
            BoardLocation boardLocation = new BoardLocation(0, 0);

            this.m_IsOnAStreak = false;
            for (int row = 0; row < this.r_SizeOfBoard; row++)
            {
                for(int col = 0; col < this.r_SizeOfBoard; col++)
                {
                    boardLocation.Row = row;
                    boardLocation.Col = col;
                    if ((this.r_Board[row, col] == 'X') || (this.r_Board[row, col] == 'K'))
                    {
                        this.m_FirstPlayerTurn = true;
                        if (checkIfCanMoveRegular(boardLocation) || checkIfCanEat(boardLocation))
                        {
                            player1CanMove = true;
                        }
                    }
                    else if((this.r_Board[row, col] == 'O') || (this.r_Board[row, col] == 'U'))
                    {
                        this.m_FirstPlayerTurn = false;
                        if (checkIfCanMoveRegular(boardLocation) || checkIfCanEat(boardLocation))
                        {
                            player2CanMove = true;
                        }
                    }
                }
            }

            this.m_FirstPlayerTurn = playerTurnHolder;
            this.m_IsOnAStreak = playerStreakHolder;
            eGameStatus = updateGameStatus(player1CanMove, player2CanMove);

            return eGameStatus;
        }

        private eGameStatus updateGameStatus(bool i_Player1CanMove, bool i_Player2CanMove)
        {
            eGameStatus eGameStatus;

            if(i_Player1CanMove && i_Player2CanMove)
            {
                eGameStatus = eGameStatus.Ongoing;
            }
            else if(!i_Player1CanMove && i_Player2CanMove)
            {
                eGameStatus = eGameStatus.Player2Won;
            }
            else if(i_Player1CanMove && !i_Player2CanMove)
            {
                eGameStatus = eGameStatus.Player1Won;
            }
            else
            {
                eGameStatus = eGameStatus.Tie;
            }

            return eGameStatus;
        }

        internal int GetKingAmountPlayer1()
        {
            int kingAmount = 0;

            for (int row = 0; row < this.r_SizeOfBoard; row++)
            {
                for (int col = 0; col < this.r_SizeOfBoard; col++)
                {
                    if(this.r_Board[row, col] == 'K')
                    {
                        kingAmount++;
                    }
                }
            }

            return kingAmount;
        }

        internal int GetKingAmountPlayer2()
        {
            int kingAmount = 0;

            for (int row = 0; row < this.r_SizeOfBoard; row++)
            {
                for (int col = 0; col < this.r_SizeOfBoard; col++)
                {
                    if (this.r_Board[row, col] == 'U')
                    {
                        kingAmount++;
                    }
                }
            }

            return kingAmount;
        }

        internal int GetRegularPieceAmountPlayer1()
        {
            int regularPieceAmount = 0;

            for (int row = 0; row < this.r_SizeOfBoard; row++)
            {
                for (int col = 0; col < this.r_SizeOfBoard; col++)
                {
                    if (this.r_Board[row, col] == 'X')
                    {
                        regularPieceAmount++;
                    }
                }
            }

            return regularPieceAmount;
        }

        internal int GetRegularPieceAmountPlayer2()
        {
            int regularPieceAmount = 0;

            for (int row = 0; row < this.r_SizeOfBoard; row++)
            {
                for (int col = 0; col < this.r_SizeOfBoard; col++)
                {
                    if (this.r_Board[row, col] == 'O')
                    {
                        regularPieceAmount++;
                    }
                }
            }

            return regularPieceAmount;
        }

        public override string ToString()
        {
            StringBuilder boardAsStringBuilder = new StringBuilder();

            for (int i = 0; i < this.r_SizeOfBoard; i++)
            {
                for (int j = 0; j < this.r_SizeOfBoard; j++)
                {
                    boardAsStringBuilder.Append(this.r_Board[i, j]);
                }
            }

            return boardAsStringBuilder.ToString();
        }

        private void concatCapitalLetters(StringBuilder io_BoardAsStringBuilder)
        {
            char capitalLetter = 'A';
            io_BoardAsStringBuilder.Append(' ', 2);
            for(int i = 0; i < this.r_SizeOfBoard; i++)
            {
                io_BoardAsStringBuilder.Append(' ');
                io_BoardAsStringBuilder.Append(capitalLetter++);
                io_BoardAsStringBuilder.Append(' ', 2);
            }

            io_BoardAsStringBuilder.Append(Environment.NewLine);
        }

        private void concatRowDivider(StringBuilder io_BoardAsStringBuilder)
        {
            io_BoardAsStringBuilder.Append(' ');
            io_BoardAsStringBuilder.Append('=', (this.r_SizeOfBoard * 4) + 1);
        }

        internal bool Play(string i_From, string i_To)
        {
            bool isValidMove = true;
            BoardLocation from, to;

            from = new BoardLocation(i_From[1] - 'a', i_From[0] - 'A');
            to = new BoardLocation(i_To[1] - 'a', i_To[0] - 'A');
            isValidMove = checkIfValidPlayMove(from, to);
            if(isValidMove)
            {
                moveThePieceOnTheBoard(from, to);
                this.m_LastMove = i_From + '>' + i_To;
                this.m_LastMovingPiece = to;
                updatePlayerTurn(from, to);
            }

            return isValidMove;
        }

        private void updatePlayerTurn(BoardLocation i_From, BoardLocation i_To)
        {
            bool isEatingMove = (Math.Abs(i_From.Row - i_To.Row) == 2) && (Math.Abs(i_From.Col - i_To.Col) == 2);

            if (!(isEatingMove && checkIfCanEat(i_To)))
            {
                this.m_FirstPlayerTurn = !this.m_FirstPlayerTurn;
                this.m_IsOnAStreak = false;
            }
            else
            {
                this.m_IsOnAStreak = true;
            }
        }

        private void moveThePieceOnTheBoard(BoardLocation i_From, BoardLocation i_To)
        {
            bool isKing = this.r_Board[i_From.Row, i_From.Col] == 'K' || this.r_Board[i_From.Row, i_From.Col] == 'U';

            if (Math.Abs(i_From.Row - i_To.Row) == 1)
            {
                this.r_Board[i_From.Row, i_From.Col] = ' ';
                if(isKing)
                {
                    this.r_Board[i_To.Row, i_To.Col] = this.m_FirstPlayerTurn ? 'K' : 'U';
                }
                else
                {
                    this.r_Board[i_To.Row, i_To.Col] = this.m_FirstPlayerTurn ? 'X' : 'O';
                }
            }
            else
            {
                this.r_Board[i_From.Row, i_From.Col] = ' ';
                this.r_Board[(i_From.Row + i_To.Row) / 2, (i_From.Col + i_To.Col) / 2] = ' ';
                if (isKing)
                {
                    this.r_Board[i_To.Row, i_To.Col] = this.m_FirstPlayerTurn ? 'K' : 'U';
                }
                else
                {
                    this.r_Board[i_To.Row, i_To.Col] = this.m_FirstPlayerTurn ? 'X' : 'O';
                }
            }

            if(!isKing)
            {
                this.r_Board[i_To.Row, i_To.Col] = this.m_FirstPlayerTurn && i_To.Row == 0 ? 'K' : this.r_Board[i_To.Row, i_To.Col];
                this.r_Board[i_To.Row, i_To.Col] = !this.m_FirstPlayerTurn && i_To.Row == this.r_SizeOfBoard - 1 ? 'U' : this.r_Board[i_To.Row, i_To.Col];
            }
        }

        private bool checkIfValidPlayMove(BoardLocation i_From, BoardLocation i_To)
        {
            bool isValidMove = true;
            bool isKing = false;

            checkOutOfBounds(i_From, i_To, ref isValidMove);
            checkIfUsingMyOwnPiece(i_From, ref isValidMove);
            checkIfDestinationIsEmpty(i_To, ref isValidMove);
            isKing = checkIfKing(i_From);
            checkIfValidSimpleOrEatingMove(i_From, i_To, ref isValidMove, isKing);
            checkValidationIfStreakMove(i_From, ref isValidMove);

            return isValidMove;
        }

        private void checkValidationIfStreakMove(BoardLocation i_From, ref bool io_IsValidMove)
        {
            if(io_IsValidMove)
            {
                if(m_IsOnAStreak && ((i_From.Row != this.m_LastMovingPiece.Row) || (i_From.Col != this.m_LastMovingPiece.Col)))
                {
                    io_IsValidMove = false;
                }
            }
        }

        private void checkIfValidSimpleOrEatingMove(BoardLocation i_From, BoardLocation i_To, ref bool io_IsValidMove, bool i_IsKing)
        {
            if(io_IsValidMove)
            {
                if(isOneMove(i_From, i_To, i_IsKing))
                {
                    if(checkIfCurrentPlayerCanEatOppponent())
                    {
                        io_IsValidMove = false;
                    }
                }
                else if(!isEatingMove(i_From, i_To, i_IsKing))
                {
                    io_IsValidMove = false;
                }
            }
        }

        private bool checkIfCurrentPlayerCanEatOppponent()
        {
            bool isCurrentPlayerCanEat = false;

            for(int row = 0; row < this.r_SizeOfBoard && !isCurrentPlayerCanEat; row++)
            {
                for(int col = 0; col < this.r_SizeOfBoard && !isCurrentPlayerCanEat; col++)
                {
                    isCurrentPlayerCanEat = checkIfCanEat(new BoardLocation(row, col));
                }
            }

            return isCurrentPlayerCanEat;
        }

        private bool checkIfCanEat(BoardLocation i_CurrentPieceLocation)
        {
            return checkIfValidPlayMove(i_CurrentPieceLocation, new BoardLocation(i_CurrentPieceLocation.Row + 2, i_CurrentPieceLocation.Col + 2))
                || checkIfValidPlayMove(i_CurrentPieceLocation, new BoardLocation(i_CurrentPieceLocation.Row + 2, i_CurrentPieceLocation.Col - 2))
                || checkIfValidPlayMove(i_CurrentPieceLocation, new BoardLocation(i_CurrentPieceLocation.Row - 2, i_CurrentPieceLocation.Col - 2))
                || checkIfValidPlayMove(i_CurrentPieceLocation, new BoardLocation(i_CurrentPieceLocation.Row - 2, i_CurrentPieceLocation.Col + 2));
        }

        private bool checkIfCanMoveRegular(BoardLocation i_CurrentPieceLocation)
        {
            return checkIfValidPlayMove(i_CurrentPieceLocation, new BoardLocation(i_CurrentPieceLocation.Row + 1, i_CurrentPieceLocation.Col + 1))
                || checkIfValidPlayMove(i_CurrentPieceLocation, new BoardLocation(i_CurrentPieceLocation.Row + 1, i_CurrentPieceLocation.Col - 1))
                || checkIfValidPlayMove(i_CurrentPieceLocation, new BoardLocation(i_CurrentPieceLocation.Row - 1, i_CurrentPieceLocation.Col - 1))
                || checkIfValidPlayMove(i_CurrentPieceLocation, new BoardLocation(i_CurrentPieceLocation.Row - 1, i_CurrentPieceLocation.Col + 1));
        }

        private bool isOneMove(BoardLocation i_From, BoardLocation i_To, bool i_IsKing)
        {
            bool isItoneMove = true;
            if(this.m_FirstPlayerTurn)
            {
                if(((((i_From.Row - i_To.Row) != 1) || (Math.Abs(i_From.Col - i_To.Col) != 1)) && !i_IsKing)
                     || (i_IsKing && ((Math.Abs(i_From.Row - i_To.Row) != 1) || (Math.Abs(i_From.Col - i_To.Col) != 1))))
                {
                    isItoneMove = false;
                }
            }
            else
            {
                if (((((i_From.Row - i_To.Row) != -1) || (Math.Abs(i_From.Col - i_To.Col) != 1)) && !i_IsKing)
                    || (i_IsKing && ((Math.Abs(i_From.Row - i_To.Row) != 1) || (Math.Abs(i_From.Col - i_To.Col) != 1))))
                {
                    isItoneMove = false;
                }
            }

            return isItoneMove;
        }

        private bool isEatingMove(BoardLocation i_From, BoardLocation i_To, bool i_IsKing)
        {
            bool isItEatingMove = true;
            BoardLocation eatenPiece = new BoardLocation((i_From.Row + i_To.Row) / 2, (i_From.Col + i_To.Col) / 2);
            if(this.m_FirstPlayerTurn)
            {
                if (((((i_From.Row - i_To.Row) != 2) || (Math.Abs(i_From.Col - i_To.Col) != 2)) && !i_IsKing)
                     || (i_IsKing && ((Math.Abs(i_From.Row - i_To.Row) != 2) || (Math.Abs(i_From.Col - i_To.Col) != 2))))
                {
                    isItEatingMove = false;
                }
                else
                {
                    if((this.r_Board[eatenPiece.Row, eatenPiece.Col] != 'O') && (this.r_Board[eatenPiece.Row, eatenPiece.Col] != 'U'))
                    {
                        isItEatingMove = false;
                    }
                }
            }
            else
            {
                if (((((i_From.Row - i_To.Row) != -2) || (Math.Abs(i_From.Col - i_To.Col) != 2)) && !i_IsKing)
                     || (i_IsKing && ((Math.Abs(i_From.Row - i_To.Row) != 2) || (Math.Abs(i_From.Col - i_To.Col) != 2))))
                {
                    isItEatingMove = false;
                }
                else
                {
                    if((this.r_Board[eatenPiece.Row, eatenPiece.Col] != 'X') && (this.r_Board[eatenPiece.Row, eatenPiece.Col] != 'K'))
                    {
                        isItEatingMove = false;
                    }
                }
            }

            return isItEatingMove;
        }

        private bool checkIfKing(BoardLocation i_From)
        {
            return this.r_Board[i_From.Row, i_From.Col] == 'K' || this.r_Board[i_From.Row, i_From.Col] == 'U';
        }

        private void checkIfDestinationIsEmpty(BoardLocation i_To, ref bool io_IsValidMove)
        {
            if (io_IsValidMove)
            {
                if(this.r_Board[i_To.Row, i_To.Col] != ' ')
                {
                    io_IsValidMove = false;
                }
            }
        }

        private void checkIfUsingMyOwnPiece(BoardLocation i_From, ref bool io_IsValidMove)
        {
            if (io_IsValidMove)
            {
                if(this.r_Board[i_From.Row, i_From.Col] == ' ')
                {
                    io_IsValidMove = false;
                }
                else if(this.IsFirstPlayerTurn && (this.r_Board[i_From.Row, i_From.Col] == 'O' || this.r_Board[i_From.Row, i_From.Col] == 'U'))
                {
                    io_IsValidMove = false;
                }
                else if(!this.IsFirstPlayerTurn && (this.r_Board[i_From.Row, i_From.Col] == 'X' || this.r_Board[i_From.Row, i_From.Col] == 'K'))
                {
                    io_IsValidMove = false;
                }
            }
        }

        private void checkOutOfBounds(BoardLocation i_From, BoardLocation i_To, ref bool io_IsValidMove)
        {
            if (io_IsValidMove)
            {
                if((i_From.Row >= this.r_SizeOfBoard) || (i_From.Col >= this.r_SizeOfBoard) || (i_To.Row >= this.r_SizeOfBoard) ||
                   (i_To.Col >= this.r_SizeOfBoard) || (i_From.Row < 0) || (i_From.Col < 0) || (i_To.Row < 0) || (i_To.Col < 0))
                {
                    io_IsValidMove = false;
                }
            }
        }

        internal LinkedList<MoveParameters> GetAllValidMovesForComputer()
        {
            LinkedList<MoveParameters> validMovesForComputer = new LinkedList<MoveParameters>();
            LinkedList<MoveParameters> validMovesForCurrentPiece = null;

            for (int row = 0; row < this.r_SizeOfBoard; row++)
            {
                for(int col = 0; col < this.r_SizeOfBoard; col++)
                {
                    if(this.r_Board[row, col] == 'O' || this.r_Board[row, col] == 'U')
                    {
                        validMovesForCurrentPiece = getAllValidMovesForPiece(new BoardLocation(row, col));
                        if(validMovesForCurrentPiece.Count != 0)
                        {
                            foreach(MoveParameters move in validMovesForCurrentPiece)
                            {
                                validMovesForComputer.AddLast(move);
                            }
                        }
                    }
                }
            }

            return validMovesForComputer;
        }

        private LinkedList<MoveParameters> getAllValidMovesForPiece(BoardLocation i_PieceLocation)
        {
            LinkedList<MoveParameters> validMovesForPiece = new LinkedList<MoveParameters>();

            for(int i = 1; i <= 2; i++)
            {
                if(checkIfValidPlayMove(i_PieceLocation, new BoardLocation(i_PieceLocation.Row + i, i_PieceLocation.Col + i)))
                {
                    validMovesForPiece.AddLast(new MoveParameters(i_PieceLocation, new BoardLocation(i_PieceLocation.Row + i, i_PieceLocation.Col + i)));
                }

                if(checkIfValidPlayMove(i_PieceLocation, new BoardLocation(i_PieceLocation.Row - i, i_PieceLocation.Col - i)))
                {
                    validMovesForPiece.AddLast(new MoveParameters(i_PieceLocation, new BoardLocation(i_PieceLocation.Row - i, i_PieceLocation.Col - i)));
                }

                if (checkIfValidPlayMove(i_PieceLocation, new BoardLocation(i_PieceLocation.Row - i, i_PieceLocation.Col + i)))
                {
                    validMovesForPiece.AddLast(new MoveParameters(i_PieceLocation, new BoardLocation(i_PieceLocation.Row - i, i_PieceLocation.Col + i)));
                }

                if (checkIfValidPlayMove(i_PieceLocation, new BoardLocation(i_PieceLocation.Row + i, i_PieceLocation.Col - i)))
                {
                    validMovesForPiece.AddLast(new MoveParameters(i_PieceLocation, new BoardLocation(i_PieceLocation.Row + i, i_PieceLocation.Col - i)));
                }
            }

            return validMovesForPiece;
        }
    }
}