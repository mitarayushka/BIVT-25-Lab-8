namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penalties;
            private int _matchCount;

            public string Name => _name;
            public string Surname => _surname;

            public int[] Penalties
            {
                get
                {
                    if (_penalties == null) return new int[0];
                    int[] copy = new int[_penalties.Length];
                    Array.Copy(_penalties, copy, _penalties.Length);
                    return copy;
                }
            }
            public int Total
            {
                get
                {
                    if (_penalties == null) return 0;
                    int sum = 0;
                    foreach (int time in _penalties)
                    {
                        sum += time;
                    }
                    return sum;
                }
            }
            public virtual bool IsExpelled
            {
                get
                {
                    if (_penalties == null) return false;
                    foreach (int time in _penalties)
                    {
                        if (time == 10) return true;
                    }
                    return false;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalties = new int[0];
                _matchCount = 0;
            }
            public virtual void PlayMatch(int value)
            {
                if (value == 0 || value == 2 || value == 5 || value == 10)
                {
                    if (_penalties == null) return;
                    Array.Resize(ref _penalties, _penalties.Length + 1);
                    _penalties[_penalties.Length - 1] = value;
                    _matchCount++;
                }
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1) return;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[i].Total > array[j].Total)
                        {
                            Participant temp = array[i];
                            array[i] = array[j];
                            array[j] = temp;
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}: {Total} мин штрафа, исключен: {IsExpelled}");
            }
        }
        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }
            public override bool IsExpelled
            {
                get
                {
                    if (_penalties == null || _penalties.Length == 0) return false;
                    int foulsMoreThan10Percent = 0;
                    int totalFouls = 0;
                    int matchesPlayed = _penalties.Length;
                    foreach (int fouls in _penalties)
                    {
                        totalFouls += fouls;
                        if (fouls == 5)
                            foulsMoreThan10Percent++;
                    }
                    bool condition1 = foulsMoreThan10Percent > matchesPlayed * 0.1;
                    bool condition2 = totalFouls > matchesPlayed * 2;
                    return condition1 || condition2;
                }
            }
            public override void PlayMatch(int fouls)
            {
                if (fouls < 0 || fouls > 5) return;

                if (_penalties == null) return;

                Array.Resize(ref _penalties, _penalties.Length + 1);
                _penalties[_penalties.Length - 1] = fouls;
            }
        }
        public class HockeyPlayer : Participant
        {
            private static int _players = 0;
            private static int _totalTime = 0;
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _players++;
            }
            public override bool IsExpelled
            {
                get
                {
                    if (_penalties == null || _penalties.Length == 0) return false;

                    foreach (int time in _penalties)
                    {
                        if (time == 10) return true;
                    }
                    double averageTime = _totalTime / (double)_players;
                    double threshold = averageTime * 0.1;
                    return this.Total > threshold;
                }
            }
            public override void PlayMatch(int minutes)
            {
                if (minutes == 0 || minutes == 2 || minutes == 5 || minutes == 10)
                {
                    if (_penalties == null) return;
                    Array.Resize(ref _penalties, _penalties.Length + 1);
                    _penalties[_penalties.Length - 1] = minutes;
                    _totalTime += minutes;
                }
            }
        }
    }
}
