using System;

namespace Lab8.Blue
{
    public class Task5
    {
        public class Sportsman
        {
            //fields
            private string _name;
            private string _surname;
            private int _place;

            //parameters
            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            //methods
            public void SetPlace(int place)
            {
                if (_place == 0) { _place = place; }
            }

            public void Print()
            {
                System.Console.WriteLine($"Name: {_name}, Surname: {_surname}, Place: {_place}");
            }

            //constructor
            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }
        }

        public abstract class Team
        {
            //fields
            protected string _name;
            protected Sportsman[] _sportsmen;
            protected int _curr;

            //parameters
            public string Name => _name;

            public Sportsman[] Sportsmen
            {
                get
                {
                    Sportsman[] res = new Sportsman[_curr];
                    Array.Copy(_sportsmen, res, _curr);
                    return res;
                }
            }

            public int TotalScore
            {
                get
                {
                    int res = 0;
                    for (int i = 0; i < _curr; i++)
                    {
                        var p = _sportsmen[i].Place;
                        if (p > 0 && p <= 5) res += (6 - p);
                    }
                    return res;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_curr == 0) return 18;
                    int min = int.MaxValue;
                    for (int i = 0; i < _curr; i++)
                    {
                        int p = _sportsmen[i].Place;
                        if (p > 0 && p < min) min = p;
                    }
                    if (min == int.MaxValue) return 0;
                    return min;
                }
            }

            //methods
            public void Add(Sportsman sportsman)
            {
                if (_curr < 6)
                {
                    _sportsmen[_curr] = sportsman;
                    _curr++;
                }
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;
                foreach (Sportsman man in sportsmen) { Add(man); }
            }

            protected abstract double GetTeamStrength();

            public static void Sort(Team[] array)
            {
                Array.Sort(array, (a, b) =>
                {
                    int scorecomparsion = b.TotalScore.CompareTo(a.TotalScore);
                    if (scorecomparsion != 0) return scorecomparsion;
                    return a.TopPlace.CompareTo(b.TopPlace);
                });
            }

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                Team best = null;
                double bestStrength = double.NegativeInfinity;

                foreach (Team t in teams)
                {
                    if (t == null) continue;
                    double s = (double)t.GetType().GetMethod("GetTeamStrength", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                                         .Invoke(t, null);
                    if (s > bestStrength)
                    {
                        bestStrength = s;
                        best = t;
                    }
                }

                return best;
            }

            public void Print()
            {
                System.Console.WriteLine($"Name: {_name}");
            }

            //constructor
            protected Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _curr = 0;
            }
        }

        public class ManTeam : Team
        {
            //constructor
            public ManTeam(string name) : base(name) { }

            //methods
            protected override double GetTeamStrength()
            {
                if (_curr == 0) return 0;

                double sum = 0;
                int count = 0;
                for (int i = 0; i < _curr; i++)
                {
                    int p = _sportsmen[i].Place;
                    if (p <= 0) continue;
                    sum += p;
                    count++;
                }
                if (count == 0) return 0;

                double avg = sum / count;
                return 100.0 / avg;
            }
        }

        public class WomanTeam : Team
        {
            //constructor
            public WomanTeam(string name) : base(name) { }

            //methods
            protected override double GetTeamStrength()
            {
                if (_curr == 0) return 0;

                double sum = 0;
                double prod = 1.0;
                int count = 0;

                for (int i = 0; i < _curr; i++)
                {
                    int p = _sportsmen[i].Place;
                    if (p <= 0) continue;
                    sum += p;
                    prod *= p;
                    count++;
                }

                if (count == 0) return 0;
                return 100.0 * sum * count / prod;
            }
        }
    }
}