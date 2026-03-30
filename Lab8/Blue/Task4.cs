namespace Lab8.Blue
{
    public class Task4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;
            private int _matchCount;

            public string Name => _name;

            public int[] Scores
            {
                get
                {
                    int[] copy = new int[_matchCount];
                    for (int i = 0; i < _matchCount; i++)
                        copy[i] = _scores[i];
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _matchCount; i++)
                        sum += _scores[i];
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
                _scores[_matchCount] = result;
                _matchCount++;
            }

            public void Print()
            {
                Console.Write($"{Name} {TotalScore} ");
                for (int i = 0; i < _matchCount; i++)
                    Console.Write($"{_scores[i]} ");
                Console.WriteLine();
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
                if (team is ManTeam && _manCount < 12)
                {
                    _manTeams[_manCount++] = team;
                }
                else if (team is WomanTeam && _womanCount < 12)
                {
                    _womanTeams[_womanCount++] = team;
                }
            }

            public void Add(Team[] teams)
            {
                foreach (Team team in teams)
                    Add(team);
            }
            
            private static void SortArray(Team[] array, int count)
            {
                for (int i = 0; i < count - 1; i++)
                {
                    for (int j = 0; j < count - 1 - i; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }

            public void Sort()
            {
                SortArray(_manTeams, _manCount);
                SortArray(_womanTeams, _womanCount);
            }
            
            private static Team[] MergeArrays(Team[] arr1, int count1, Team[] arr2, int count2, int size)
            {
                int teamsFromEach = size / 2;
                Team[] candidates = new Team[size];
                int candidateCount = 0;

                for (int i = 0; i < teamsFromEach && i < count1; i++)
                    candidates[candidateCount++] = arr1[i];

                for (int i = 0; i < teamsFromEach && i < count2; i++)
                    candidates[candidateCount++] = arr2[i];
                
                SortArray(candidates, candidateCount);

                return candidates;
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");

                group1.Sort();
                group2.Sort();

                // Мужские команды
                Team[] mergedMan = MergeArrays(
                    group1._manTeams, group1._manCount,
                    group2._manTeams, group2._manCount,
                    size);

                foreach (Team t in mergedMan)
                    if (t != null) result.Add(t);

                // Женские команды
                Team[] mergedWoman = MergeArrays(
                    group1._womanTeams, group1._womanCount,
                    group2._womanTeams, group2._womanCount,
                    size);

                foreach (Team t in mergedWoman)
                    if (t != null) result.Add(t);

                return result;
            }

            public void Print()
            {
                Console.WriteLine(Name);
                Console.WriteLine("Мужские команды:");
                for (int i = 0; i < _manCount; i++)
                    _manTeams[i].Print();
                Console.WriteLine("Женские команды:");
                for (int i = 0; i < _womanCount; i++)
                    _womanTeams[i].Print();
            }
        }
    }
}
