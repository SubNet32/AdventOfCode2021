using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day21Content
{
    class GameState
    {
        public static int winScore = 21;

        public int index=0;

        public int scorePlayer1;    //0-21
        public int posPlayer1;      //1-10
        public int scorePlayer2;    //0-21
        public int posPlayer2;      //1-10
        public int currentPlayer;
        public int winner;          //0-2

        public long count;
        public long saveCount;

        public GameState() { }

        public GameState(int scP1, int posP1, int scP2, int posP2, int curP) 
        {
            this.scorePlayer1 = scP1;
            this.scorePlayer2 = scP2;
            this.posPlayer1 = posP1;
            this.posPlayer2 = posP2;
            this.currentPlayer = curP;
            this.count = 0;
            this.saveCount = 0;
            CheckWinner();
        }

        public GameState(GameState gameState)
        {
            this.scorePlayer1 = gameState.scorePlayer1;
            this.scorePlayer2 = gameState.scorePlayer2;
            this.posPlayer1 = gameState.posPlayer1;
            this.posPlayer2 = gameState.posPlayer2;
            this.currentPlayer = gameState.currentPlayer;
            this.winner = gameState.winner;
            this.count = gameState.count;
            this.saveCount = gameState.saveCount;
        }

        public void CheckWinner()
        {
            if(scorePlayer1>= winScore)
            {
                winner = 1;
            }
            else if (scorePlayer2 >= winScore)
            {
                winner = 2;
            }
        }

        public bool Compare(int scP1, int posP1, int scP2, int posP2, int curP)
        {
            return scP1 == scorePlayer1 &&
                scP2 == scorePlayer2 &&
                posP1 == posPlayer1 &&
                posP2 == posPlayer2 &&
                curP == currentPlayer;
        }
        public bool Compare(GameState gs)
        {
            return gs.scorePlayer1 == scorePlayer1 &&
                gs.scorePlayer2 == scorePlayer2 &&
                gs.posPlayer1 == posPlayer1 &&
                gs.posPlayer2 == posPlayer2 &&
                gs.currentPlayer == currentPlayer;
        }

        public void AddNewCount(long add)
        {
            saveCount += add;
        }

        public void AcceptSavedCount()
        {
            if (saveCount > 0)
            {
                count += saveCount;
                saveCount = 0;
            }
         
        }

        public GameState MoveCurrentPlayer(int roll)
        {
            GameState g = new GameState(this);
            if(g.currentPlayer==1)
            {
                g.posPlayer1 = GameState.FindNewPos(g.posPlayer1, roll);
                g.scorePlayer1 = Math.Min(g.scorePlayer1+g.posPlayer1, winScore);
                g.currentPlayer = 2;
            }
            else if(g.currentPlayer == 2)
            {
                g.posPlayer2 = GameState.FindNewPos(g.posPlayer2, roll);
                g.scorePlayer2 = Math.Min(g.scorePlayer2 + g.posPlayer2, winScore);
                g.currentPlayer = 1;
            }
            return g;
        }

        public static int FindNewPos(int curPos, int dist)
        {
            curPos += dist;
            while (curPos > 10)
                curPos -= 10;
            return curPos;
        }

        public override string ToString()
        {
            return (winner>0 ? "Winner: "+winner.ToString() : "NoWinner") +
                " | CurP: " + currentPlayer +
                " | P1: P(" + posPlayer1.ToString("00") + ") S(" + scorePlayer1.ToString("00") + ")" +
                " | P2: P(" + posPlayer2.ToString("00") + ") S(" + scorePlayer2.ToString("00") + ")" +
                " |   Count: " + count;
        }

    }
}
