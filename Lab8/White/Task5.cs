namespace Lab8.White
{
    public class Task5
    {
        public struct Match
        {
            //поля
            private int _goals;
            private int _misses;


            //свойства
            public int Goals => _goals;
            public int Misses => _misses;
            //возвращают разность забитых и пропущенных голов
            public int Difference => _goals - _misses;
            //{
            //    get
            //    {
            //        return _goals-_misses;

            //    }
            //}
            //количество очков за матч (выигрыш – 3, ничья – 1, проигрыш – 0)
            public int Score
            {
                get
                {
                    if (_goals > _misses)
                    {
                        return 3;
                    }
                    else if (_goals == _misses)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }



            }
            //конструктор
            public Match(int goals, int misses)
            {
                _goals = goals;
                _misses = misses;
            }

            //метод
            public void Print()
            {
                Console.WriteLine(_goals);
                Console.WriteLine(_misses);
                Console.WriteLine(Difference);
                Console.WriteLine(Score);
            }
        }
        public abstract class Team
        {
            //поля
            private string _name;
            private Match[] _matches;

            //свойство
            public string Name => _name;
            public Match[] Matches // берем массив первой структуры
            {
                get
                {

                    return _matches;
                }
            }
            // Суммарная разность забитых и пропущенных голов во всех матчах
            public int TotalDifference
            {
                get
                {
                    if (_matches == null || _matches.Length == 0)
                        return 0;

                    int count = 0;
                    for (int i = 0; i < _matches.Length; i++)
                    {
                        int matchDifference = _matches[i].Difference;
                        count = count + matchDifference;

                    }
                    return count;

                }
            }
            //суммарное количество баллов, набранных командой
            public int TotalScore
            {
                get
                {
                    if (_matches == null || _matches.Length == 0)
                        return 0;
                    int count1 = 0;
                    for (int i = 0; i < _matches.Length; i++)
                    {
                        int matchScore = _matches[i].Score;
                        count1 = count1 + matchScore;
                    }
                    return count1;
                }
            }
            //конструктор
            public Team(string name)
            {
                _name = name;
                _matches = new Match[0];
            }
            //метод
            public virtual void PlayMatch(int goals, int misses)
            {
                if (_matches == null)
                    _matches = new Match[0];

                Array.Resize(ref _matches, _matches.Length + 1);
                _matches[_matches.Length - 1] = new Match(goals, misses);//добавляем новый матч в новый созданный массив

            }
            public static void SortTeams(Team[] teams)
            {
                if (teams == null || teams.Length <= 1)
                    return;

                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 1; j < teams.Length; j++)
                    {
                        // Сначала сравниваем по очкам
                        if (teams[j - 1].TotalScore < teams[j].TotalScore)
                        {
                            (teams[j - 1], teams[j]) = (teams[j], teams[j - 1]);
                        }
                        // сравниваем по разности голов(усли количество очков равно)
                        else if (teams[j - 1].TotalScore == teams[j].TotalScore)
                        {
                            if (teams[j - 1].TotalDifference < teams[j].TotalDifference)
                            {
                                (teams[j - 1], teams[j]) = (teams[j], teams[j - 1]);
                            }
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine(_name);
                Console.WriteLine(_matches.Length);
                Console.WriteLine(TotalScore);
                Console.WriteLine(TotalDifference);
            }
        }
       
        public class ManTeam : Team
        {
            private ManTeam _derby;

            public  ManTeam Derby =>_derby;

            public ManTeam(string name,ManTeam derby = null) :base(name)
            {
                _derby = derby;

            }

            public void PlayMatch(int goals, int misses, ManTeam team= null)
            {
                if (team ==_derby && team!= null)
                {
                    goals++;

                }
                base.PlayMatch(goals, misses);


            }
           
                
            




        }
        public class WomanTeam : Team
        {
            private int[] _penalties;

            public int[] Penalties => _penalties;

            public int TotalPenalties
            {
                get
                {
                    if (_penalties == null || _penalties.Length == 0)
                        return 0;




                    int count = 0;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        count += _penalties[i];

                    }
                    return count;

                }

            }
            public WomanTeam(string name) : base(name)
            {
                _penalties = new int[0];


            }
            public override void PlayMatch(int goals, int misses)
            {
                if (misses > goals)
                {
                    int penalti = misses - goals;
                    Array.Resize(ref _penalties, _penalties.Length + 1);
                    _penalties[_penalties.Length - 1] = penalti;


                }
                base.PlayMatch(goals, misses);



            }


        }

    }
}
