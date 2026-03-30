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
                    int[] copy = new int[_scores.Length];
                    Array.Copy(_scores, copy, _scores.Length);

                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _scores.Length; i++)
                        sum += _scores[i];

                    return sum;
                }
            }

            public Team(string name)
            {
                _name = name;
                _scores = [];
            }

            public void PlayMatch(int result)
            {
                int[] copy = new int[_scores.Length + 1];
                Array.Copy(_scores, copy, _scores.Length);
                copy[_scores.Length] = result;
                _scores = copy;
            }

            public void Print()
            {
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
            private Team[] _manTeams;
            private Team[] _womanTeams;

            private int _womanCount;
            private int _manCount;

            public string Name => _name;

            public Team[] ManTeams
            {
                get
                {
                    Team[] copy = new Team[_manTeams.Length];
                    Array.Copy(_manTeams, copy, _manTeams.Length);
                    return copy;
                }
            }

            public Team[] WomanTeams
            {
                get
                {
                    Team[] copy = new Team[_womanTeams.Length];
                    Array.Copy(_womanTeams, copy, _womanTeams.Length);
                    return copy;
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
                if (team is ManTeam && _manCount < 12)
                    _manTeams[_manCount++] = team;
                else if (team is WomanTeam && _womanCount < 12)
                    _womanTeams[_womanCount++] = team;
            }

            public void Add(Team[] teams)
            {
                foreach (var team in teams)
                    Add(team);
            }

            private void SortTeams(Team[] teams)
            {
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - 1 - i; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                    }
                }
            }

            public void Sort()
            {
                SortTeams(_womanTeams);
                SortTeams(_manTeams);
            }

            private static Team[] MergeSort(Team[] teams1, Team[] teams2, int countTeam)
            {
                Team[] result = new Team[countTeam];
                int i = 0, j = 0, k = 0;

                while (i < countTeam / 2 && j < countTeam / 2)
                {
                    if (teams1[i].TotalScore >= teams2[j].TotalScore)
                    {
                        result[k++] = teams1[i++];
                    }
                    else
                    {
                        result[k++] = teams2[j++];
                    }
                }

                while (i < countTeam / 2)
                {
                    result[k++] = teams1[i++];
                }

                while (j < countTeam / 2)
                {
                    result[k++] = teams2[j++];
                }

                return result;
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");

                group1.Sort();
                group2.Sort();

                result._manTeams = MergeSort(group1._manTeams, group2._manTeams, size);
                result._womanTeams = MergeSort(group1._womanTeams, group2._womanTeams, size);

                return result;
            }

            public void Print() { }

        }
    }
}