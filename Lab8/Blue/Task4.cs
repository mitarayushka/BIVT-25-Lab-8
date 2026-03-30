using System;

namespace Lab8.Blue
{
    public class Task4
    {


        public abstract class Team
        {

            private string _name;
            private int[] _scores;

            public string Name => _name;

            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;

                    int[] copy = new int[_scores.Length];
                    Array.Copy(_scores, copy, _scores.Length);
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {

                    if (_scores == null) return 0;

                    int sum = 0;
                    foreach (int s in _scores) sum += s;
                    return sum;
                }
            }

            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
            }

            public void Print()
            {

            }
        }



        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }



        public class Group
        {
            

            private string _name;
            private Team[] _manTeams;
            private Team[] _womanTeams;
            private int _manCount;
            private int _womanCount;


            public string Name => _name;
            public Team[] ManTeams => _manTeams;
            public Team[] WomanTeams => _womanTeams;

            public Group(string name)
            {

                _name = name;
                _manTeams = new Team[12];
                _womanTeams = new Team[12];
                _manCount = 0;
                _womanCount = 0;
            }

            public void Add(Team team)
            {
  
                if (team == null) return;

                if (team is ManTeam)
                {
                    if (_manCount < 12) _manTeams[_manCount++] = team;
                }
                else if (team is WomanTeam)
                {
                    if (_womanCount < 12) _womanTeams[_womanCount++] = team;
                }
            }

            public void Add(Team[] teams)
            {
                if (teams == null) return;

                foreach (var t in teams)
                    Add(t);
            }

            public void Sort()
            {

                SortArray(_manTeams, _manCount);
                SortArray(_womanTeams, _womanCount);
            }
      
            private void SortArray(Team[] array, int count)
            {
                if (array == null || count < 2) return;

                for (int i = 0; i < count - 1; i++)
                {
                    for (int j = 0; j < count - i - 1; j++)
                    {

                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            var temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public static Group Merge(Group g1, Group g2, int size)
            {


                Group result = new Group("Финалисты");
                int halfSize = size / 2;


                g1.Sort();
                g2.Sort();
                for (int i = 0; i < halfSize; i++)
                {
                    if (i < g1._manCount) result.Add(g1._manTeams[i]);
                }
                for (int i = 0; i < halfSize; i++)
                {
                    if (i < g2._manCount) result.Add(g2._manTeams[i]);
                }

                for (int i = 0; i < halfSize; i++)
                {
                    if (i < g1._womanCount) result.Add(g1._womanTeams[i]);
                }
                for (int i = 0; i < halfSize; i++)
                {
                    if (i < g2._womanCount) result.Add(g2._womanTeams[i]);
                }

                result.Sort();

                return result;
            }

            public void Print()
            {
            }
        }
    }
}
