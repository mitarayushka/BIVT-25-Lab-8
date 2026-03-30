using System;

namespace Lab8.Blue
{
    public class Task4
    {
        public abstract class Team
        {
            //fields
            private string _name;
            private int[] _scores;

            //parameters
            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    int[] copy = new int[_scores.Length];
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _scores[i];
                    }
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        sum += _scores[i];
                    }
                    return sum;
                }
            }

            //methods
            public void PlayMatch(int result)
            {
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
            }

            public void Print()
            {
                return;
            }

            //constructor
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }
        }

        public class ManTeam : Team
        {
            //constructor
            public ManTeam(string name) : base(name) { }
        }

        public class WomanTeam : Team
        {
            //constructor
            public WomanTeam(string name) : base(name) { }
        }

        public class Group
        {
            //fields
            private string _name;
            private int _manCount;
            private int _womanCount;
            private Team[] _manTeams;
            private Team[] _womanTeams;

            //parametrs
            public string Name => _name;

            public Team[] ManTeams
            {
                get
                {
                    Team[] copy = new Team[_manTeams.Length];
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _manTeams[i];
                    }
                    return copy;
                }
            }

            public Team[] WomanTeams
            {
                get
                {
                    Team[] copy = new Team[_womanTeams.Length];
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _womanTeams[i];
                    }
                    return copy;
                }
            }

            //methods
            public void Add(Team team)
            {
                if (team is ManTeam && _manCount < 12)
                {
                    _manTeams[_manCount] = team;
                    _manCount++;
                }
                else if (team is WomanTeam && _womanCount < 12)
                {
                    _womanTeams[_womanCount] = team;
                    _womanCount++;
                }
            }

            public void Add(Team[] teams)
            {
                if (teams == null) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    Add(teams[i]);
                }
            }

            public void Sort()
            {
                SortTeams(_manTeams);
                SortTeams(_womanTeams);
            }

            private void SortTeams(Team[] team)
            {
                for (int i = 0; i < team.Length; i++)
                {
                    for (int j = 1; j < team.Length - i; j++)
                    {
                        if (team[j] == null) continue;
                        if (team[j - 1] == null)
                        {
                            (team[j - 1], team[j]) = (team[j], team[j - 1]);
                            continue;
                        }

                        if (team[j - 1].TotalScore < team[j].TotalScore)
                        {
                            (team[j - 1], team[j]) = (team[j], team[j - 1]);
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");

                int teamCount = size / 2;

                group1.Sort();
                group2.Sort();

                MergeTeams(ref result._manTeams, ref result._manCount, group1._manTeams, group2._manTeams, group1._manCount, group2._manCount, teamCount);
                MergeTeams(ref result._womanTeams, ref result._womanCount, group1._womanTeams, group2._womanTeams, group1._womanCount, group2._womanCount, teamCount);

                result.Sort();
                return result;
            }

            private static void MergeTeams(ref Team[] result, ref int resultCount, Team[] groupTeams1, Team[] groupTeams2, int count1, int count2, int count)
            {
                for (int i = 0; i < count && i < count1; i++)
                {
                    result[resultCount] = groupTeams1[i];
                    resultCount++;
                }

                for (int i = 0; i < count && i < count2; i++)
                {
                    result[resultCount] = groupTeams2[i];
                    resultCount++;
                }
            }

            public void Print()
            {
                return;
            }

            //constructor
            public Group(string name)
            {
                _name = name;
                _manTeams = new Team[12];
                _womanTeams = new Team[12];
                _manCount = 0;
                _womanCount = 0;
            }
        }
    }
}