using System;

namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {
            //fields
            private string _name;
            private string _surname;
            protected int[] _penalties;

            //parameters
            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties => (int[])_penalties.Clone();

            public int Total
            {
                get
                {
                    int summ = 0;
                    foreach (int i in _penalties) summ += i;
                    return summ;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    foreach (int i in _penalties)
                        if (i >= 10) return true;
                    return false;
                }
            }

            //methods
            public virtual void PlayMatch(int time)
            {
                if (time >= 0)
                {
                    Array.Resize(ref _penalties, _penalties.Length + 1);
                    _penalties[_penalties.Length - 1] = time;
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array != null && array.Length != 0)
                {
                    Array.Sort(array, (a, b) => a.Total.CompareTo(b.Total));
                }
            }

            public void Print()
            {
                System.Console.WriteLine($"Name: {Name}, Surname: {Surname}, Total: {Total}");
            }

            //constructor
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalties = new int[0];
            }
        }

        public class BasketballPlayer : Participant
        {
            //constructor
            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            //methods
            public override void PlayMatch(int fouls)
            {
                if (fouls >= 0 && fouls <= 5) base.PlayMatch(fouls);
            }

            public override bool IsExpelled
            {
                get
                {
                    int matches = _penalties.Length;
                    if (matches == 0) return false;

                    int count5 = 0;
                    int total = 0;
                    foreach (int f in _penalties)
                    {
                        total += f;
                        if (f == 5) count5++;
                    }

                    if (count5 > 0.1 * matches) return true;
                    if (total > 2 * matches) return true;

                    return false;
                }
            }
        }

        public class HockeyPlayer : Participant
        {
            //fields
            private static int _players = 0;
            private static int _totalTime = 0;
            private static int _summaryPenalties = 0;

            //constructor
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _players++;
            }

            //methods
            public override void PlayMatch(int time)
            {
                if (time >= 0)
                {
                    base.PlayMatch(time);
                    _totalTime += time;
                    _summaryPenalties += time;
                }
            }

            public override bool IsExpelled
            {
                get
                {
                    foreach (int i in _penalties)
                        if (i >= 10) return true;

                    if (_players <= 0) return false;

                    double avg = (double)_totalTime / _players;
                    if (this.Total > 0.1 * avg) return true;

                    return false;
                }
            }
        }
    }
}