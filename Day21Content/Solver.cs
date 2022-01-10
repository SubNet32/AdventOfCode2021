using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Day21Content
{
    class Solver
    {
        int startPosPlayer1;
        int startPosPlayer2;

        GameState[] states;
        List<int> changedStates;
        List<int> activeStates;


        public Solver(string[] input)
        {
            List<int> startPositions = new List<int>();
            foreach(string s in input)
            {
                int pos = int.Parse(s.Split(':')[1].Replace(" ", ""));
                startPositions.Add(pos);
            }
            startPosPlayer1 = startPositions[0];
            startPosPlayer2 = startPositions[1];

            LoadGameStates();

            GameState start = FindGameState(0, startPosPlayer1, 0, startPosPlayer2,1);
            start.count++;
            
        }

        public void LoadGameStates()
        {
            List<GameState> possibleStates = new List<GameState>();
            for (int curP = 1; curP <= 2; curP++)
            {
                for (int posP1 = 1; posP1 <= 10; posP1++)
                {
                    for (int posP2 = 1; posP2 <= 10; posP2++)
                    {
                        for (int scoreP1 = 0; scoreP1 <= 21; scoreP1++)
                        {
                            for (int scoreP2 = 0; scoreP2 <= 21; scoreP2++)
                            {
                                GameState g = new GameState(scoreP1, posP1, scoreP2, posP2,curP);
                                g.index = possibleStates.Count;
                                possibleStates.Add(g);
                            }
                        }
                    }
                }
            }
            states = possibleStates.ToArray();
            Utilities.Log("Loaded " + states.Length + " possible gamestates");
        }

        public GameState FindGameState(int scP1, int posP1, int scP2, int posP2, int curP)
        {
            return states.Where(s => s.Compare(scP1, posP1, scP1, posP2, curP)).FirstOrDefault();
        }

        public long Solve()
        {
            changedStates = new List<int>();
            activeStates = new List<int>();
            activeStates.Add(states.Where(s => s.count > 0).FirstOrDefault().index);
            Utilities.Log("");
            Utilities.Log("Starting Solver. StartState: " + activeStates[0] +"  " + states[activeStates[0]]);
            while (activeStates.Count>0)
            {
                foreach (int i in activeStates)
                {
                    if (i != states[i].index)
                        throw new Exception("Index != i");
                    states[i].AcceptSavedCount(); //vom vorherigen zykl
                    if (states[i].winner == 0 && states[i].count > 0)
                    {
                        //Utilities.Log(states[i].ToString());
                        Play(states[i]);
                        AddChangedState(i);
                        states[i].count = 0;
                    }
                }
                activeStates = new List<int>(changedStates);
                changedStates.Clear();

                //Console.ReadKey();
                Utilities.Log("ActiveStates: " + activeStates.Count.ToString("00000000"));
                Utilities.Log("");

            }

            List<GameState> winnerStates = states.Where(s => s.winner > 0 && s.count > 0).ToList();
            long cP1 = 0;
            long cP2 = 0;
            foreach (GameState s in winnerStates)
            {
                if (s.winner == 1)
                    cP1+=s.count;
                if (s.winner == 2)
                    cP2+= s.count;
                //if (s.winner > 0)
                    //Utilities.Log("WinnerState: " + s);
            }
            Utilities.Log("");
            Utilities.Log("ActiveStatesCount: " + activeStates.Count);
            Utilities.Log("ChangedStatesCount: " + changedStates.Count);
            Utilities.Log("There are a total of " + winnerStates.Count + " winnerStates");

            Utilities.Log("Player 1 won in " + cP1 + " Universes");
            Utilities.Log("Player 2 won in " + cP2 + " Universes");

            return cP1 > cP2 ? cP1 : cP2;
        }

        public void Play(GameState state)
        {
            for(int fr = 1; fr<=3; fr++)
            {
                for (int sr = 1; sr <= 3; sr++)
                {
                    for (int tr = 1; tr <= 3; tr++)
                    {
                        EvokeGameState(state, fr+sr+tr);
                    }
                }
            }
        }

        int lastRoll = 0;
        public void PlaySimple(GameState state)
        {
            int roll = 0;
            string s = "";
            for (int i = 0; i < 3; i++)
            {
                lastRoll++;
                if (lastRoll > 100)
                    lastRoll = 1;
                s += lastRoll.ToString("00") + " ";
                roll += lastRoll;
            }
            Utilities.Log("Rolling Dice: " + s);
            
            EvokeGameState(state, roll);
        }

        public void EvokeGameState(GameState state, int roll)
        {
            GameState evokedGS = state.MoveCurrentPlayer(roll);
            GameState findGS = states.Where(s => s.Compare(evokedGS)).FirstOrDefault();
            findGS.AddNewCount(state.count);
            //Utilities.Log("--->Adding Score to State: " + findGS);
            AddChangedState(findGS.index);
        }     

        public void AddChangedState(int index)
        {
            if (!changedStates.Contains(index))
                changedStates.Add(index);
        }

     
    }
}
