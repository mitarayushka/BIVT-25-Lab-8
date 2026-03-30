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
                    int[] array = new int[_scores.Length];
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        array[i] = _scores[i];
                    }
                    return array;
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

            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                int[] array = new int[_scores.Length + 1];
                for (int i = 0; i < _scores.Length; i++)
                {
                    array[i] = _scores[i];
                }
                array[array.Length - 1] = result;
                _scores = array;
            }

            public void Print()
            {
                Console.WriteLine($"{Name}, {TotalScore}");
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

            public Team[] ManTeams
            {
                get
                {
                    Team[] array = new Team[_manTeams.Length];
                    for (int i = 0; i < _manTeams.Length; i++)
                    {
                        array[i] = _manTeams[i];
                    }
                    return array;
                }
            }

            public Team[] WomanTeams
            {
                get
                {
                    Team[] array = new Team[_womanTeams.Length];
                    for (int i = 0; i < _womanTeams.Length; i++)
                    {
                        array[i] = _womanTeams[i];
                    }
                    return array;
                }
            }

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
                if (team is ManTeam)
                {
                    if (_manCount < _manTeams.Length)
                    {
                        _manTeams[_manCount++] = team;
                    }
                }
                else if (team is WomanTeam)
                {
                    if (_womanCount < _womanTeams.Length)
                    {
                        _womanTeams[_womanCount++] = team;
                    }
                }
            }

            public void Add(Team[] teams)
            {
                if (teams == null) return;
                foreach (Team t in teams)
                {
                    if (t != null) Add(t);
                }
            }

            public void Sort()
            {
                SortTeams(_manTeams, _manCount);
                SortTeams(_womanTeams, _womanCount);
            }

            private void SortTeams(Team[] teams, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    for (int j = 1; j < count; j++)
                    {
                        if (teams[j].TotalScore > teams[j - 1].TotalScore)
                        {
                            (teams[j - 1], teams[j]) = (teams[j], teams[j - 1]);
                        }
                    }
                }
            }
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group final = new Group("Финалисты");
                MergeTeams(final, group1.ManTeams, group2.ManTeams, size);
                MergeTeams(final, group1.WomanTeams, group2.WomanTeams, size);
                return final;
            }

            private static void MergeTeams(Group final, Team[] teams1, Team[] teams2, int size)
            {
                int t1 = size / 2;
                int t2 = size - t1;

                int i = 0, j = 0;
                while (i < t1 && j < t2)
                {
                    if (teams1[i] == null && teams2[j] == null) break;

                    if (teams1[i] == null)
                    {
                        final.Add(teams2[j++]);
                    }
                    else if (teams2[j] == null)
                    {
                        final.Add(teams1[i++]);
                    }
                    else if (teams1[i].TotalScore >= teams2[j].TotalScore)
                    {
                        final.Add(teams1[i++]);
                    }
                    else
                        final.Add(teams2[j++]);
                }

                while (i < t1 && teams1[i] != null) final.Add(teams1[i++]);
                while (j < t2 && teams2[j] != null) final.Add(teams2[j++]);
            }

            public void Print()
            {
                Console.WriteLine($"Группа: {Name}");
                Console.WriteLine("Список мужских команд:");
                foreach (Team t in _manTeams)
                {
                    if (t != null) Console.WriteLine($"  {t.Name} (очков: {t.TotalScore})");
                }
                Console.WriteLine("Список женских команд:");
                foreach (Team t in _womanTeams)
                {
                    if (t != null) Console.WriteLine($"  {t.Name} (очков: {t.TotalScore})");
                }
            }
        }
    }
}
