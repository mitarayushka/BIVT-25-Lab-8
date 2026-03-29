namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penaltytimes;

            public string Name => _name;
            public string Surname => _surname;

            public int[] Penalties
            {
                get
                {
                    int[] copy = new int[_penaltytimes.Length];
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _penaltytimes[i];
                    }
                    return copy;
                }
            }

            public int Total
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        sum += _penaltytimes[i];
                    }
                    return sum;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] == 10)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltytimes = new int[0];
            }

            public virtual void PlayMatch(int time)
            {
                int[] array = new int[_penaltytimes.Length + 1];
                for (int i = 0; i < _penaltytimes.Length; i++)
                {
                    array[i] = _penaltytimes[i];
                }
                array[array.Length - 1] = time;
                _penaltytimes = array;
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j].Total < array[j - 1].Total)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{IsExpelled}, имя {_name}, фамилия: {_surname}, время: {Total}");
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override void PlayMatch(int time)
            {
                if (time >= 0 && time <= 5)
                {
                    base.PlayMatch(time);
                }
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltytimes.Length == 0) return false;

                    int count5 = 0;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] == 5) count5++;
                    }

                    bool rule1 = count5 > (_penaltytimes.Length * 0.1);

                    bool rule2 = Total > (_penaltytimes.Length * 2);

                    return rule1 || rule2;
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
                int[] array = new int[_penaltytimes.Length + 1];
                for (int i = 0; i < _penaltytimes.Length; i++)
                {
                    array[i] = _penaltytimes[i];
                }
                array[array.Length - 1] = time;
                _penaltytimes = array;

                _totalTime += time;
            }

            public override bool IsExpelled
            {
                get
                {
                    foreach (int t in _penaltytimes)
                    {
                        if (t == 10) return true;
                    }

                    if (_players > 0)
                    {
                        double limit = 0.10 * ((double)_totalTime / _players);
                        if (Total > limit)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
        }
    }
}
