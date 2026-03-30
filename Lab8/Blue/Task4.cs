namespace Lab8.Blue
{
    public class Task4
    {
    public abstract class Team
        {
            private string _name;
            private int[] _scores;
            
            public string Name => _name;
            public int[] Scores => _scores.ToArray();

            public int TotalScore
            {
                get
                {
                    int total = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        total += _scores[i];
                    }
                    
                    return total;
                }
            }
            
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                if (result < 0) return;
                
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
            }

            public void Print()
            {
                Console.WriteLine($"{_name}: {TotalScore} score");
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
            private int _manCount;
            private int _womanCount;
            
            public string Name => _name;

            public Team[] ManTeams
            {
                get
                {
                    Team[] copy = new Team[_manTeams.Length];
                    for (int i = 0; i < _manTeams.Length; i++)
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
                    for (int i = 0; i < _womanTeams.Length; i++)
                    {
                        copy[i] = _womanTeams[i];
                    }

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
                if (_manCount < 12)
                    if (team is ManTeam)
                    {
                        _manTeams[_manCount++] = team;
                    }
                if (_womanCount < 12)
                    if (team is WomanTeam)
                    {
                        _womanTeams[_womanCount++] = team;
                    }
            }
            
            public void Add(Team[] teams)
            {
                foreach (Team t in teams)
                {
                    Add(t);
                }
            }

            private void SortTeams(Team[] teams)
            {
                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 1; j < teams.Length; j++)
                    {
                        if (teams[j - 1].TotalScore < teams[j].TotalScore)
                        {
                            (teams[j - 1], teams[j]) = (teams[j], teams[j - 1]);
                        }
                    }
                }
            }

            public void Sort()
            {
                SortTeams(_manTeams);
                SortTeams(_womanTeams);
            }

            private static Team[] MergeTeams(Team[] t1, Team[] t2, int size)
            {
                Team[] result = new Team[size];
                int i = 0, j = 0, k = 0;
                
                
                while (i < size / 2 && j < size / 2)
                {
                    if (t1[i].TotalScore >= t2[j].TotalScore)
                    {
                        result[k++] = t1[i++];
                    }
                    else
                    {
                        result[k++] = t2[j++];
                    }
                }

                while (i < size / 2)
                    result[k++] = t1[i++];
                while (j < size / 2)
                {
                    result[k++] = t2[j++];
                }

                return result;
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                if (group1 == null || group2 == null) return null;
                
                
                group1.Sort();
                group2.Sort();

                Group final = new Group("Финалисты");
                
                if (size % 2 != 0)
                    return null;

                final._manTeams = MergeTeams(group1.ManTeams, group2.ManTeams, size);
                final._womanTeams = MergeTeams(group1.WomanTeams, group2.WomanTeams, size);
                
                return final;
            }

            public void Print()
            {
                
            }
        }
    }
}
