namespace Lab8.Blue
{
    public class Task3
    {public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penaltyTimes;
            
            public string Name => _name;
            public string Surname => _surname;
            public virtual int[] Penalties => _penaltyTimes.ToArray();

            public virtual int Total
            {
                get
                {
                    int total = 0;

                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        total += _penaltyTimes[i];
                    }
                    
                    return total;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    bool flag = false;

                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 10)
                        {
                            flag = true;
                        }
                    }
                    
                    return flag;
                }
            }
            
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[0];
            }

            public virtual void PlayMatch(int time)
            {
                Array.Resize(ref _penaltyTimes, _penaltyTimes.Length+1);
                _penaltyTimes[_penaltyTimes.Length - 1] = time;
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].Total > array[j].Total)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"В составе {IsExpelled}, {_name}, {_surname}: {Total}");
            }
            
        }

        public class BasketballPlayer : Participant
        {
            private int _countPlay;

            public override bool IsExpelled
            {
                get
                {
                    bool flag = false;
                    int count5 = 0;
                    
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 5)
                        {
                            count5++;
                        }
                    }

                    if (_countPlay > 0)
                    {
                        if ((double)count5 / _countPlay > 0.1) flag = true;
                    }

                    if (Total > _countPlay * 2) flag = true;
                    
                    return flag;
                }
            }

            public override void PlayMatch(int countFoul)
            {
                if ((countFoul < 0) || (countFoul > 5)) return;

                _countPlay++;
                
                Array.Resize(ref _penaltyTimes, _penaltyTimes.Length+1);
                _penaltyTimes[_penaltyTimes.Length - 1] = countFoul;
            }
            
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
                _countPlay = 0;
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int _players;
            private static int _totalTime;

            public override bool IsExpelled
            {
                get
                {
                    bool flag = false;
                    
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 10)
                        {
                            flag = true;
                        }
                    }

                    if (_players > 0)
                    {
                        if (Total > 0.1 * ((double)_totalTime / _players)) flag = true;
                    }
                    
                    return flag;
                }
            }
            
            public override void PlayMatch(int time)
            {
                _totalTime += time;
                
                Array.Resize(ref _penaltyTimes, _penaltyTimes.Length+1);
                _penaltyTimes[_penaltyTimes.Length - 1] = time;
            }

            static HockeyPlayer()
            {
                _players = 0;
                _totalTime = 0;
            }

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _players++;
            }
        }
    }
}
