namespace Lab8.Blue
{
    public class Task4
    {
        public abstract class Team
        {
            // поля
            private string _name;
            private int[] _scores;

            // свойства
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

            // конструктор
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                int[] newMatch = new int[_scores.Length + 1];
                for (int i = 0; i < _scores.Length; i++)
                {
                    newMatch[i] = _scores[i];
                }
                newMatch[newMatch.Length - 1] = result;
                _scores = newMatch;
            }
            public void Print()
            {
                return;
            }
        }
        public class ManTeam : Team
        {
            //konstuktor
            public ManTeam(string name) : base(name)
            {
            }
        }
        public class WomanTeam : Team
        {
            //konstruktor
            public WomanTeam(string name) : base(name)
            {

            }
        }
        public class Group
        {
            //поля
            private string _name;
            private Team[] _manteams;
            private Team[] _womanteams;
            private int womancount;
            private int mancount;

            // свойства
            public string Name => _name;
            public Team[] ManTeams
            {
                get
                {
                    Team[] copy = new Team[_manteams.Length];
                    for (int i = 0; i < _manteams.Length; i++)
                    {
                        copy[i] = _manteams[i];
                    }
                    return copy;
                }
            }
            public Team[] WomanTeams
            {
                get
                {
                    Team[] copy = new Team[_womanteams.Length];
                    for (int i = 0; i < _womanteams.Length; i++)
                    {
                        copy[i] = _womanteams[i];
                    }
                    return copy;
                }
            }

            //конструктор
            public Group(string name)
            {
                _name = name;
                _manteams = new Team[12];
                _womanteams = new Team[12];
                womancount = 0;
                mancount = 0;
            }

            public void Add(Team name)
            {
                if (name is ManTeam && mancount < 12)
                {
                    _manteams[mancount] = name;
                    mancount++;
                }
                if (name is WomanTeam && womancount < 12)
                {
                    _womanteams[womancount] = name;
                    womancount++;
                }
            }
            public void Add(Team[] names)
            {
                for (int i = 0; i < names.Length; i++)
                {
                    Add(names[i]);
                }
            }
            private void SortTeam(Team[] team)
            {
                for (int i = 0; i < team.Length; i++)
                {
                    for (int j = 1; j < team.Length - i; j++)
                    {
                        if (team[j - 1].TotalScore < team[j].TotalScore) // убывания очков команды
                        {
                            (team[j - 1], team[j]) = (team[j], team[j - 1]);
                        }
                    }
                }
            }
            public void Sort()
            {

                SortTeam(_manteams);
                SortTeam(_womanteams);
            }
            private static Team[] MergeSort(Team[] teams1, Team[] teams2, int sizeTeam) //уже отсортированные команды
            {
                Team[] result = new Team[sizeTeam];
                int thebestteams = sizeTeam / 2;
                int indteam1 = 0; int indteam2 = 0; int indresult = 0;
                while (indteam1 < thebestteams && indteam2 < thebestteams)
                {
                    if (teams1[indteam1].TotalScore >= teams2[indteam2].TotalScore)
                    {
                        result[indresult++] = teams1[indteam1++];
                    }
                    else
                    {
                        result[indresult++] = teams2[indteam2++];
                    }
                }
                while (indteam1 < thebestteams) //остатки первого массива команд
                {
                    result[indresult++] = teams1[indteam1++];
                }
                while (indteam2 < thebestteams)
                {
                    result[indresult++] = teams2[indteam2++];
                }
                return result;
            }
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");
                if (group1._manteams == null || group2._manteams == null || group2._womanteams == null || group1._womanteams == null)
                {
                    return null;
                }
                group1.Sort();
                group2.Sort();
                if (size % 2 != 0)
                    return null;
                result._manteams = MergeSort(group1._manteams, group2._manteams, size);
                result._womanteams = MergeSort(group1._womanteams, group2._womanteams, size);

                //result.Sort();
                return result;
            }
            public void Print()
            {
                return;
            }
        }
    }
}
