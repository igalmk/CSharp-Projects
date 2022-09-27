using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend
{
    internal class Player
    {
        private readonly string r_PlayerName;
        private readonly char r_Marker;
        private readonly bool r_IsHuman;
        private int m_Score;

        internal Player(string i_PlayerName, char i_UserSign, bool i_Human)
        {
            this.m_Score = 0;
            this.r_PlayerName = i_PlayerName;
            this.r_Marker = i_UserSign;
            this.r_IsHuman = i_Human;
        }

        internal int Score
        {
            get
            {
                return this.m_Score;
            }
        }

        internal string Name
        {
            get
            {
                return this.r_PlayerName;
            }
        }

        internal char Marker
        {
            get
            {
                return this.r_Marker;
            }
        }

        internal bool IsHuman
        {
            get
            {
                return this.r_IsHuman;
            }
        }

        internal void UpdateScore(int i_Points)
        {
            this.m_Score = this.m_Score + i_Points;
        }
    }
}
