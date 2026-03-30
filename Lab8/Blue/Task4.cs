namespace Lab8.Blue
{
    public class Task4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;
            public string Name => _name;
            public int[] Scores => _scores;

            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    foreach (int score in _scores)
                    {
                        sum += score;
                    }
                    return sum;
                }
            }
            public Team(string Name)
            {
                _name = Name;
                _scores = new int[1];
            }
            public void PlayMatch(int result)
            {
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
            }

            public void Print()
            {
                Console.WriteLine($"Команда: {Name}, Очки: {TotalScore}");
            }


        }
        public class Group
        {
            private string _name;
            private ManTeam[] _manTeams;
            private WomanTeam[] _womanTeams;
            private int _manCount;
            private int _womanCount;
            public string Name => _name;

            public Group(string Name)
            {
                _name = Name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                _manCount = 0;
                _womanCount = 0;
            }

            public ManTeam[] ManTeams
            {
                get
                {
                    ManTeam[] copy = new ManTeam[12];
                    Array.Copy(_manTeams, copy, _manCount);
                    return copy;
                }
            }
            public WomanTeam[] WomanTeams
            {
                get
                {
                    WomanTeam[] copy = new WomanTeam[12];
                    Array.Copy(_womanTeams, copy, _womanCount);
                    return copy;
                }
            }

            public void Add(Team name)
            {
                if (name is ManTeam manTeam)
                {
                    if (_manCount < 12)
                    {
                        _manTeams[_manCount] = manTeam;
                        _manCount++;
                    }
                }
                else if (name is WomanTeam womanTeam)
                {
                    if (_womanCount < 12)
                    {
                        _womanTeams[_womanCount] = womanTeam;
                        _womanCount++;
                    }
                }

            }
            public void Add(Team[] Names)
            {
                foreach (Team name in Names)
                {
                    Add(name);

                }
            }
            public void Sort()
            {
                SortArray(_manTeams, _manCount);
                SortArray(_womanTeams, _womanCount);
            }

            private void SortArray(Team[] teams, int count)
            {
                if (teams == null || count <= 1) return;

                for (int i = 0; i < count - 1; i++)
                {
                    for (int j = 0; j < count - 1 - i; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");
                group1.Sort();
                group2.Sort();

                MergeTeam(result, group1.ManTeams, group2.ManTeams, size);
                MergeTeam(result, group1.WomanTeams, group2.WomanTeams, size);

                result.Sort();
                return result;
            }

            private static void MergeTeam(Group target, Team[] teams1, Team[] teams2, int size)
            {
                for (int i = 0; i < size / 2; i++)
                {
                    if (i < teams1.Length) target.Add(teams1[i]);
                }

                for (int i = 0; i < size / 2; i++)
                {
                    if (i < teams2.Length) target.Add(teams2[i]);
                }
            }
            public void Print()
            {
            }

        }

        public class ManTeam: Team
        {
            public ManTeam(string name): base(name) { }
        }

        public class WomanTeam: Team
        {
            public WomanTeam(string name): base(name) { }
        }
    }
}
