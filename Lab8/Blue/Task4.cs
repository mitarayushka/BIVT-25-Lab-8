namespace Lab8.Blue
{
    public class Task4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;
            private int _matchCount;

            public string Name
            {
                get { return _name; }
            }

            public int[] Scores
            {
                get
                {
                    if (_scores == null)
                    {
                        return new int[0];
                    }

                    int[] copy = new int[_matchCount];
                    Array.Copy(_scores, copy, _matchCount);
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_scores == null)
                    {
                        return 0;
                    }

                    int sum = 0;

                    for (int i = 0; i < _matchCount; i++)
                    {
                        sum += _scores[i];
                    }

                    return sum;
                }
            }

            protected Team(string name)
            {
                _name = name;
                _scores = new int[100];
                _matchCount = 0;
            }

            public void PlayMatch(int result)
            {
                if (_scores == null)
                {
                    return;
                }

                if (_matchCount < _scores.Length)
                {
                    _scores[_matchCount] = result;
                    _matchCount++;
                }
            }

            public void Print()
            {
                Console.Write($"{_name}: ");

                if (_scores != null)
                {
                    for (int i = 0; i < _matchCount; i++)
                    {
                        Console.Write($"{_scores[i]} ");
                    }
                }

                Console.WriteLine($"(сумма: {TotalScore})");
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name)
            {
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name)
            {
            }
        }

        public class Group
        {
            private string _name;
            private ManTeam[] _manTeams;
            private WomanTeam[] _womanTeams;
            private int _manCount;
            private int _womanCount;

            public string Name
            {
                get { return _name; }
            }

            public ManTeam[] ManTeams
            {
                get
                {
                    if (_manTeams == null)
                    {
                        return new ManTeam[0];
                    }

                    ManTeam[] copy = new ManTeam[_manTeams.Length];
                    Array.Copy(_manTeams, copy, _manTeams.Length);
                    return copy;
                }
            }

            public WomanTeam[] WomanTeams
            {
                get
                {
                    if (_womanTeams == null)
                    {
                        return new WomanTeam[0];
                    }

                    WomanTeam[] copy = new WomanTeam[_womanTeams.Length];
                    Array.Copy(_womanTeams, copy, _womanTeams.Length);
                    return copy;
                }
            }

            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                _manCount = 0;
                _womanCount = 0;
            }

            public void Add(Team team)
            {
                if (team == null)
                {
                    return;
                }

                if (team is ManTeam)
                {
                    AddManTeam((ManTeam)team);
                    return;
                }

                if (team is WomanTeam)
                {
                    AddWomanTeam((WomanTeam)team);
                }
            }

            public void Add(Team[] teams)
            {
                if (teams == null)
                {
                    return;
                }

                for (int i = 0; i < teams.Length; i++)
                {
                    Add(teams[i]);
                }
            }

            public void Sort()
            {
                SortManTeams();
                SortWomanTeams();
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");

                if (group1 == null || group2 == null)
                {
                    return result;
                }

                ManTeam[] manFinalists = MergeManTeams(
                    group1._manTeams,
                    group1._manCount,
                    group2._manTeams,
                    group2._manCount,
                    size);

                WomanTeam[] womanFinalists = MergeWomanTeams(
                    group1._womanTeams,
                    group1._womanCount,
                    group2._womanTeams,
                    group2._womanCount,
                    size);

                for (int i = 0; i < manFinalists.Length; i++)
                {
                    if (manFinalists[i] != null)
                    {
                        result.Add(manFinalists[i]);
                    }
                }

                for (int i = 0; i < womanFinalists.Length; i++)
                {
                    if (womanFinalists[i] != null)
                    {
                        result.Add(womanFinalists[i]);
                    }
                }

                return result;
            }

            public void Print()
            {
                Console.WriteLine($"{_name}:");

                for (int i = 0; i < _manCount; i++)
                {
                    if (_manTeams[i] != null)
                    {
                        Console.Write($"  M{i + 1}. ");
                        _manTeams[i].Print();
                    }
                }

                for (int i = 0; i < _womanCount; i++)
                {
                    if (_womanTeams[i] != null)
                    {
                        Console.Write($"  W{i + 1}. ");
                        _womanTeams[i].Print();
                    }
                }
            }

            private void AddManTeam(ManTeam team)
            {
                if (_manTeams == null || team == null)
                {
                    return;
                }

                if (_manCount < _manTeams.Length)
                {
                    _manTeams[_manCount] = team;
                    _manCount++;
                }
            }

            private void AddWomanTeam(WomanTeam team)
            {
                if (_womanTeams == null || team == null)
                {
                    return;
                }

                if (_womanCount < _womanTeams.Length)
                {
                    _womanTeams[_womanCount] = team;
                    _womanCount++;
                }
            }

            private void SortManTeams()
            {
                if (_manTeams == null || _manCount <= 1)
                {
                    return;
                }

                for (int i = 0; i < _manCount - 1; i++)
                {
                    for (int j = 0; j < _manCount - 1 - i; j++)
                    {
                        if (_manTeams[j] != null && _manTeams[j + 1] != null &&
                            _manTeams[j].TotalScore < _manTeams[j + 1].TotalScore)
                        {
                            ManTeam temp = _manTeams[j];
                            _manTeams[j] = _manTeams[j + 1];
                            _manTeams[j + 1] = temp;
                        }
                    }
                }
            }

            private void SortWomanTeams()
            {
                if (_womanTeams == null || _womanCount <= 1)
                {
                    return;
                }

                for (int i = 0; i < _womanCount - 1; i++)
                {
                    for (int j = 0; j < _womanCount - 1 - i; j++)
                    {
                        if (_womanTeams[j] != null && _womanTeams[j + 1] != null &&
                            _womanTeams[j].TotalScore < _womanTeams[j + 1].TotalScore)
                        {
                            WomanTeam temp = _womanTeams[j];
                            _womanTeams[j] = _womanTeams[j + 1];
                            _womanTeams[j + 1] = temp;
                        }
                    }
                }
            }

            private static ManTeam[] MergeManTeams(
                ManTeam[] first,
                int firstCount,
                ManTeam[] second,
                int secondCount,
                int size)
            {
                if (size < 0)
                {
                    size = 0;
                }

                int total = firstCount + secondCount;
                if (size > total)
                {
                    size = total;
                }

                ManTeam[] result = new ManTeam[size];

                int i = 0;
                int j = 0;
                int k = 0;

                while (i < firstCount && j < secondCount && k < size)
                {
                    if (first[i].TotalScore >= second[j].TotalScore)
                    {
                        result[k] = first[i];
                        i++;
                    }
                    else
                    {
                        result[k] = second[j];
                        j++;
                    }

                    k++;
                }

                while (i < firstCount && k < size)
                {
                    result[k] = first[i];
                    i++;
                    k++;
                }

                while (j < secondCount && k < size)
                {
                    result[k] = second[j];
                    j++;
                    k++;
                }

                return result;
            }

            private static WomanTeam[] MergeWomanTeams(
                WomanTeam[] first,
                int firstCount,
                WomanTeam[] second,
                int secondCount,
                int size)
            {
                if (size < 0)
                {
                    size = 0;
                }

                int total = firstCount + secondCount;
                if (size > total)
                {
                    size = total;
                }

                WomanTeam[] result = new WomanTeam[size];

                int i = 0;
                int j = 0;
                int k = 0;

                while (i < firstCount && j < secondCount && k < size)
                {
                    if (first[i].TotalScore >= second[j].TotalScore)
                    {
                        result[k] = first[i];
                        i++;
                    }
                    else
                    {
                        result[k] = second[j];
                        j++;
                    }

                    k++;
                }

                while (i < firstCount && k < size)
                {
                    result[k] = first[i];
                    i++;
                    k++;
                }

                while (j < secondCount && k < size)
                {
                    result[k] = second[j];
                    j++;
                    k++;
                }

                return result;
            }
        }
    }
}
