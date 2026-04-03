namespace Lab8.White
{
    public class Task5
    {
        public struct Match
        {
            //Приватные поля
            private int _goals;
            private int _misses;

            //Свойства
            public int Goals => _goals;
            public int Misses => _misses;
            public int Difference => _goals - _misses;
            public int Score
            {
                get
                {
                    if (_goals > _misses) return 3;
                    else if (_goals == _misses) return 1;
                    else return 0;
                }
            }
            //Конструктор структуры
            public Match(int goals, int misses)
            {
                _goals = goals;
                _misses = misses;
            }
            //Методы
            public void Print()
            {
                return;
            }
        }
        //Абстрактный класс (общие поля и методы для всех команд)
        public abstract class Team
        {
            //Поля
            private string _name;
            private Match[] _matches;

            //Свойства
            public string Name => _name;
            public Match[] Matches
            {
                get
                {
                    return _matches;
                }
            }
            //разница забитых и пропущенных голов за все матчи
            public int TotalDifference
            {
                get
                {
                    if (_matches == null || _matches.Length == 0) return 0;
                    int x = 0;
                    for (int i = 0; i < _matches.Length; i++) x += _matches[i].Difference;
                    return x;
                }
            }
            //общее количество очков (набранных командой)
            public int TotalScore
            {
                get
                {
                    if (_matches == null || _matches.Length == 0) return 0;
                    int count1 = 0;
                    for (int i = 0; i < _matches.Length; i++)
                    {
                        int matchScore = _matches[i].Score;
                        count1 = count1 + matchScore;
                    }
                    return count1;
                }
            }
            //Конструктор 
            public Team(string name)
            {
                _name = name;
                _matches = new Match[0];
            }
            //Виртуальный метод 
            public virtual void PlayMatch(int goals, int misses)
            {
                if (_matches == null) _matches = new Match[0];
                //увеличиваем размер массива и добавляем новый матч
                Array.Resize(ref _matches, _matches.Length + 1);
                _matches[_matches.Length - 1] = new Match(goals, misses);
            }
            //Статическая сортировка массива команд
            public static void SortTeams(Team[] teams)
            {
                if (teams == null || teams.Length <= 1) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 1; j < teams.Length; j++)
                    {
                        //сравниваем по очкам
                        if (teams[j - 1].TotalScore < teams[j].TotalScore)
                        {
                            (teams[j - 1], teams[j]) = (teams[j], teams[j - 1]);
                        }
                        //если очки равны, тогда сравниваем по разности голов
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
                return;
            }
        }

        public class ManTeam : Team
        {
            private ManTeam _derby; //ссылка на команду дерби

            public ManTeam Derby => _derby; //свойство (вернуть дерби)

            public ManTeam(string name, ManTeam derby = null) : base(name)
            {
                _derby = derby;
            }
            public void PlayMatch(int goals, int misses, ManTeam team = null)
            {
                if (team == _derby && team != null) goals++;
                //вызов метода (сохранение матча)
                base.PlayMatch(goals, misses);
            }
        }
        public class WomanTeam : Team
        {
            private int[] _penalties; //массив штрафов 

            public int[] Penalties => _penalties; //свойство

            //Общая сумма штрафов
            public int TotalPenalties
            {
                get
                {
                    if (_penalties == null || _penalties.Length == 0) return 0;
                    int s = 0;
                    for (int i = 0; i < _penalties.Length; i++) s += _penalties[i];
                    return s;
                }
            }
            public WomanTeam(string name) : base(name)
            {
                _penalties = new int[0];
            }
            public override void PlayMatch(int goals, int misses)
            {
                //Проверяем, пропущенные голы больше забитых
                if (misses > goals)
                {
                    int penalti = misses - goals; //размер штрафа
                    //добавляем штраф в массив
                    Array.Resize(ref _penalties, _penalties.Length + 1);
                    _penalties[_penalties.Length - 1] = penalti;
                }
                //сохраняем матч в класс 
                base.PlayMatch(goals, misses);
            }
        }
    }
}
