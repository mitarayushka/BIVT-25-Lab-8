using System;

namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penalties;

            public string Name => _name;
            public string Surname => _surname;

            public int[] Penalties
            {
                get
                {
                    if (_penalties == null) return null;
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
                    foreach (int p in _penalties) sum += p;
                    return sum;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_penalties == null) return false;
                    foreach (int p in _penalties)
                        if (p == 10) return true;
                    return false;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalties = new int[0];
            }

            public virtual void PlayMatch(int time)
            {
                Array.Resize(ref _penalties, _penalties.Length + 1);
                _penalties[^1] = time;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                Array.Sort(array, (x, y) => x.Total.CompareTo(y.Total));
            }
            public void Print()
            {
                
            }
        }


        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string n, string s) : base(n, s) { }

            public override void PlayMatch(int fouls)
            {
                if (fouls < 0 || fouls > 5) return;
                base.PlayMatch(fouls);
            }

            public override bool IsExpelled
            {
                get
                {
                    int count5 = 0;
                    foreach (int f in _penalties)
                        if (f == 5) count5++;

                    bool c1 = (double)count5 / _penalties.Length > 0.1;
                    bool c2 = Total > _penalties.Length * 2;
                    return c1 || c2;
                }
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int _playersCount;
            private static int _totalTime;

            public HockeyPlayer(string n, string s) : base(n, s)
            {
                _playersCount++;
            }

            public override void PlayMatch(int time)
            {
                base.PlayMatch(time);
                _totalTime += time;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penalties.Length == 0) return false;

                    bool has10 = base.IsExpelled;
                    double threshold = (_totalTime / (double)_playersCount) * 0.10;
                    bool tooMuch = Total > threshold;

                    return has10 || tooMuch;
                }
            }
        }
    }
}
