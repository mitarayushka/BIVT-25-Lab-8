namespace Lab8.Blue
{
    public class Task4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;
            
            public string Name => _name;
            public int[] Scores // копия очков
            {
                get
                {
                    int[] copy = new int[_scores.Length];
                    Array.Copy(_scores, copy, _scores.Length);
                    
                    return copy;
                }
            }
            
            public int TotalScore // сумма всех очков
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
            
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }
            
            public void PlayMatch(int result) // результат нового матча 
            {
                int[] newMatch = new int[_scores.Length+1];
                Array.Copy(_scores, newMatch, _scores.Length);
                newMatch[newMatch.Length-1] = result;
                _scores = newMatch;
            }
            
            public void Print()
            {
                Console.WriteLine("Name: " + _name + ", TotalScore: " + TotalScore);
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
            private ManTeam[] _manTeams;
            private WomanTeam[] _womanTeams;
            private int _manCount;
            private int _womanCount;

            public string Name => _name;
            
            public Group(string name)
            {
                _name = name;
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

            public void Add(Team[] names)
            {
                foreach (var team in names)
                {
                    Add(team);
                }
            }

            private void SortTeamArray<T>(T[] teams, int count) where T : Team
            {
                if (count <= 1) return;
                
                Array.Sort(teams, 0, count, 
                    Comparer<T>.Create((a, b) => b.TotalScore.CompareTo(a.TotalScore)));
            }
            
            public void Sort()
            {
                SortTeamArray(_manTeams, _manCount);
                SortTeamArray(_womanTeams, _womanCount);
            }

            
            private static void MergeTeamArrays<T>(Group result, T[] teams1, T[] teams2, 
                int count1, int count2, int size) 
                where T : Team
            {
                int takeFromEach = size / 2;
    
                // Берём минимум: сколько хотим взять и сколько реально есть
                int take1 = Math.Min(takeFromEach, count1);
                int take2 = Math.Min(takeFromEach, count2);
    
                // Добавляем по очереди: из первой, потом из второй
                for (int i = 0; i < take1; i++)
                {
                    result.Add(teams1[i]);
                }
    
                for (int i = 0; i < take2; i++)
                {
                    result.Add(teams2[i]);
                }
            }
            
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group result = new Group("Финалисты");
    
                group1.Sort();
                group2.Sort();
                
    
                MergeTeamArrays(result, group1._manTeams, group2._manTeams, 
                    group1._manCount, group2._manCount, size);
                MergeTeamArrays(result, group1._womanTeams, group2._womanTeams, 
                    group1._womanCount, group2._womanCount, size);
    
                result.Sort();
    
                return result;
            }

            public void Print()
            {
                Console.WriteLine("Group: " + _name);
                
                Console.WriteLine("  Men teams:");
                for (int i = 0; i < _manCount; i++)
                {
                    Console.Write("    ");
                    _manTeams[i].Print();
                }
                
                Console.WriteLine("  Women teams:");
                for (int i = 0; i < _womanCount; i++)
                {
                    Console.Write("    ");
                    _womanTeams[i].Print();
                }
            }
        }
    }
}
