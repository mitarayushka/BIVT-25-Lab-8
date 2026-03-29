using System;

namespace Lab8.Blue
{
    public class Task4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;
            private int _totalScore;

            public string Name => _name;
            public int TotalScore => _totalScore;

            public int[] Scores
            {
                get
                {
                    if (_scores == null) return new int[0];
                    return (int[])_scores.Clone();
                }
            }

            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
                _totalScore = 0;
            }

            public void PlayMatch(int result)
            {
                if (_scores == null) _scores = new int[0];
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
                _totalScore += result;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} - {TotalScore}");
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

            public string Name => _name;
            public Team[] ManTeams => _manTeams;
            public Team[] WomanTeams => _womanTeams;

            public Group(string name)
            {
                _name = name;
                _manTeams = new Team[12];
                _womanTeams = new Team[12];
            }

            private void AddTeam(Team team, Team[] array)
            {
                if (team == null || array == null) return;

                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == null)
                    {
                        array[i] = team;
                        break;
                    }
                }
            }

            public void Add(Team team)
            {
                if (team is ManTeam)
                {
                    AddTeam(team, _manTeams);
                }
                else if (team is WomanTeam)
                {
                    AddTeam(team, _womanTeams);
                }
            }

            public void Add(Team[] teams)
            {
                if (teams == null) return;
                foreach (var t in teams)
                {
                    Add(t);
                }
            }

            private void SortTeams(Team[] array)
            {
                if (array == null) return;

                Array.Sort(array, (t1, t2) =>
                {
                    if (t1 == null && t2 == null) return 0;
                    if (t1 == null) return 1;
                    if (t2 == null) return -1;

                    return t2.TotalScore.CompareTo(t1.TotalScore);
                });
            }

            public void Sort()
            {
                SortTeams(_manTeams);
                SortTeams(_womanTeams);
            }

            private static void MergeTeams(Team[] array1, Team[] array2, Team[] target, int size)
            {
                int i = 0, j = 0, k = 0;
                int limit = size / 2;

                while (k < size && k < target.Length)
                {
                    bool has1 = i < limit && i < array1.Length && array1[i] != null;
                    bool has2 = j < limit && j < array2.Length && array2[j] != null;

                    if (!has1 && !has2) break;

                    if (has1 && has2)
                    {
                        if (array1[i].TotalScore >= array2[j].TotalScore)
                        {
                            target[k++] = array1[i++];
                        }
                        else
                        {
                            target[k++] = array2[j++];
                        }
                    }
                    else if (has1)
                    {
                        target[k++] = array1[i++];
                    }
                    else
                    {
                        target[k++] = array2[j++];
                    }
                }
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");

                if (group1 != null && group2 != null)
                {
                    MergeTeams(group1.ManTeams, group2.ManTeams, result.ManTeams, size);
                    MergeTeams(group1.WomanTeams, group2.WomanTeams, result.WomanTeams, size);
                }

                return result;
            }

            public void Print()
            {
                Console.WriteLine(Name);
            }
        }
    }
}