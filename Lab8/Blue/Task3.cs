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
                    if (_penalties == null) return new int[0];
                    return (int[])_penalties.Clone();
                }
            }

            public int Total
            {
                get
                {
                    if (_penalties == null) return 0;
                    int sum = 0;
                    foreach (var p in _penalties)
                    {
                        sum += p;
                    }
                    return sum;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_penalties == null) return false;
                    foreach (var p in _penalties)
                    {
                        if (p >= 10) return true;
                    }
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
                if (_penalties == null) _penalties = new int[0];
                Array.Resize(ref _penalties, _penalties.Length + 1);
                _penalties[_penalties.Length - 1] = time;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                Array.Sort(array, (p1, p2) => p1.Total.CompareTo(p2.Total));
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} - {Total}");
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
                    if (_penalties == null || _penalties.Length == 0) return false;

                    int count5 = 0;
                    foreach (var p in _penalties)
                    {
                        if (p == 5) count5++;
                    }

                    bool rule1 = count5 > 0.1 * _penalties.Length;
                    bool rule2 = Total > 2 * _penalties.Length;

                    return rule1 || rule2;
                }
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int _players;
            private static int _totalTime;

            private static int _summaryPenalties;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _players++;
            }

            public override void PlayMatch(int time)
            {
                base.PlayMatch(time);
                _totalTime += time;
                _summaryPenalties += time;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (base.IsExpelled) return true;

                    if (_players > 0)
                    {
                        double limit = (_totalTime * 0.1) / _players;
                        if (Total > limit) return true;
                    }

                    return false;
                }
            }
        }
    }
}