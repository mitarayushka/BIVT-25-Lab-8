namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penaltyTimes;
            private int _matchCount;

            public string Name
            {
                get { return _name; }
            }

            public string Surname
            {
                get { return _surname; }
            }

            public int[] Penalties
            {
                get
                {
                    if (_penaltyTimes == null)
                    {
                        return new int[0];
                    }

                    int[] copy = new int[_penaltyTimes.Length];
                    Array.Copy(_penaltyTimes, copy, _penaltyTimes.Length);
                    return copy;
                }
            }

            public int Total
            {
                get
                {
                    if (_penaltyTimes == null)
                    {
                        return 0;
                    }

                    int sum = 0;

                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        sum += _penaltyTimes[i];
                    }

                    return sum;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null)
                    {
                        return false;
                    }

                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 10)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            protected int MatchCount
            {
                get { return _matchCount; }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[0];
                _matchCount = 0;
            }

            public virtual void PlayMatch(int time)
            {
                if (_penaltyTimes == null)
                {
                    return;
                }

                Array.Resize(ref _penaltyTimes, _penaltyTimes.Length + 1);
                _penaltyTimes[_penaltyTimes.Length - 1] = time;
                _matchCount++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1)
                {
                    return;
                }

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[i] != null && array[j] != null && array[i].Total > array[j].Total)
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
                Console.WriteLine($"{_name} {_surname}: {Total}");
            }
        }

        public class BasketballPlayer : Participant
        {
            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || MatchCount == 0)
                    {
                        return false;
                    }

                    int matchesWithFiveFouls = 0;

                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 5)
                        {
                            matchesWithFiveFouls++;
                        }
                    }

                    if (matchesWithFiveFouls * 10 > MatchCount)
                    {
                        return true;
                    }

                    if (Total > MatchCount * 2)
                    {
                        return true;
                    }

                    return false;
                }
            }

            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
            }

            public override void PlayMatch(int fouls)
            {
                if (fouls < 0 || fouls > 5)
                {
                    return;
                }

                base.PlayMatch(fouls);
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int _players;
            private static int _totalTime;

            static HockeyPlayer()
            {
                _players = 0;
                _totalTime = 0;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || _players == 0)
                    {
                        return false;
                    }

                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 10)
                        {
                            return true;
                        }
                    }

                    double averageLimit = 0.1 * _totalTime / _players;

                    if (Total > averageLimit)
                    {
                        return true;
                    }

                    return false;
                }
            }

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _players++;
            }

            public override void PlayMatch(int time)
            {
                if (time < 0)
                {
                    return;
                }

                base.PlayMatch(time);
                _totalTime += time;
            }
        }
    }
}
