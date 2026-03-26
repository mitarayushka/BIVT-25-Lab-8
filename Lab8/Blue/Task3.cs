namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {

            private string _name;

            private string _surname;

            protected int[] _penaltyTimes;
            
            
            public string Name => _name;
            public string Surname => _surname;

            public int[] Penalties
            {
                get
                {
                    int[] copy = new int[_penaltyTimes.Length];
                    Array.Copy(_penaltyTimes, copy, _penaltyTimes.Length);
                    
                    return copy;
                }
            }

            public int Total
            {
                get
                {
                    int sum = 0;

                    foreach (var time in _penaltyTimes)
                        sum += time;

                    return sum;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    foreach (var t in _penaltyTimes)
                        if (t == 10)
                            return true;

                    return false;
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
                if (time == -1)
                    return;
                
                int[] newArray = new int[_penaltyTimes.Length + 1];

                Array.Copy(_penaltyTimes, newArray, _penaltyTimes.Length);

                newArray[^1] = time;
                _penaltyTimes = newArray;
            }

            public static void Sort(Participant[] array)
            {
                Array.Sort(array, (a, b) => a.Total.CompareTo(b.Total));
            }
            
            public void Print() { }
            
        } 
        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override void PlayMatch(int fouls)
            {
                if (fouls <= 0 || fouls > 5)
                    return;
                
                Array.Resize(ref _penaltyTimes, _penaltyTimes.Length + 1);
                _penaltyTimes[^1] = fouls;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (Total > _penaltyTimes.Length * 2)
                        return true;

                    int countFiveFouls = 0;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 5)
                            countFiveFouls++;
                    }

                    if (countFiveFouls > 0.1 * _penaltyTimes.Length)
                        return true;
                    
                    return false;
                }
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
            public HockeyPlayer(string name, string surname) : base(name, surname) { _players++; }

            public override void PlayMatch(int time)
            {
                if (time < 0)
                    return;
                
                Array.Resize(ref _penaltyTimes, _penaltyTimes.Length + 1);
                _penaltyTimes[^1] = time;
                
                _totalTime += time;
            }

            public override bool IsExpelled
            {
                get
                {
                    foreach (var time in _penaltyTimes)
                    {
                        if (time >= 10)
                            return true;
                    }
                    if (Total > _totalTime * 0.1 / _penaltyTimes.Length)
                        return true;
                    
                    return false;
                }
            }
        }
    }
}