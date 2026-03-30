namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penaltyTimes;
            protected int _matchCount;

            public string Name => _name;
            public string Surname => _surname;

            public virtual int[] Penalties
            {
                get
                {
                    if (_penaltyTimes == null) return new int[0];
                    int[] copy = new int[_matchCount];
                    Array.Copy(_penaltyTimes, copy, _matchCount);
                    return copy;
                }
            }

            public virtual int Total
            {
                get
                {
                    if (_penaltyTimes == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _matchCount; i++)
                        sum += _penaltyTimes[i];
                    return sum;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null) return false;
                    for (int i = 0; i < _matchCount; i++)
                        if (_penaltyTimes[i] == 10) return true;
                    return false;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[30];
                _matchCount = 0;
            }

            public virtual void PlayMatch(int time)
            {
                if (time >= 0 && _matchCount < _penaltyTimes.Length)
                {
                    _penaltyTimes[_matchCount] = time;
                    _matchCount++;
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[i].Total > array[j].Total)
                            (array[i], array[j]) = (array[j], array[i]);
                    }
                }
            }

            public void Print()
            {
                Console.Write($"{Name} {Surname} {Total} ");
                if (_penaltyTimes != null)
                    for (int i = 0; i < _matchCount; i++)
                        Console.Write($"{_penaltyTimes[i]} ");
                Console.WriteLine();
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override void PlayMatch(int fouls)
            {
                if (fouls >= 0 && fouls <= 5 && _matchCount < _penaltyTimes.Length)
                {
                    _penaltyTimes[_matchCount] = fouls;
                    _matchCount++;
                }
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || _matchCount == 0) return false;

                    int fiveFoulMatches = 0;
                    for (int i = 0; i < _matchCount; i++)
                        if (_penaltyTimes[i] == 5) fiveFoulMatches++;

                    if (fiveFoulMatches > _matchCount * 0.10) return true;
                    if (Total >= _matchCount * 2) return true;

                    return false;
                }
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

            public override void PlayMatch(int time)
            {
                if (time >= 0 && _matchCount < _penaltyTimes.Length)
                {
                    _totalTime += time;
                    _penaltyTimes[_matchCount] = time;
                    _matchCount++;
                }
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || _matchCount == 0) return false;
                    
                    for (int i = 0; i < _matchCount; i++)
                        if (_penaltyTimes[i] >= 10) return true;
                    
                    if (_players > 0 && Total > (double)_totalTime / _players / 10.0)
                        return true;

                    return false;
                }
            }
        }
    }
}
