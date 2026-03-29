using System;

namespace Lab8.Blue
{
    public class Task5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }

            public void SetPlace(int place)
            {
                _place = place;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} - Ìוסעמ: {Place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name => _name;

            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) return new Sportsman[0];
                    return (Sportsman[])_sportsmen.Clone();
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int sum = 0;
                    foreach (var s in _sportsmen)
                    {
                        if (s.Place >= 1 && s.Place <= 5)
                        {
                            sum += (6 - s.Place);
                        }
                    }
                    return sum;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null || _sportsmen.Length == 0) return 18;

                    int min = int.MaxValue;
                    bool hasValid = false;
                    foreach (var s in _sportsmen)
                    {
                        if (s.Place > 0)
                        {
                            if (s.Place < min) min = s.Place;
                            hasValid = true;
                        }
                    }
                    return hasValid ? min : 0;
                }
            }

            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }

            public void Add(Sportsman sportsman)
            {
                if (sportsman == null) return;
                if (_sportsmen == null) _sportsmen = new Sportsman[0];
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;
                foreach (var s in sportsmen)
                {
                    Add(s);
                }
            }

            public static void Sort(Team[] array)
            {
                if (array == null) return;
                Array.Sort(array, (t1, t2) =>
                {
                    if (t1 == null && t2 == null) return 0;
                    if (t1 == null) return 1;
                    if (t2 == null) return -1;

                    int cmp = t2.TotalScore.CompareTo(t1.TotalScore);
                    if (cmp == 0)
                    {
                        return t1.TopPlace.CompareTo(t2.TopPlace);
                    }
                    return cmp;
                });
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                Team champion = null;
                double maxStrength = -1;

                foreach (var t in teams)
                {
                    if (t != null)
                    {
                        double str = t.GetTeamStrength();
                        if (str > maxStrength)
                        {
                            maxStrength = str;
                            champion = t;
                        }
                    }
                }

                return champion;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} - Ñקוע: {TotalScore}, Ëףקרוו לוסעמ: {TopPlace}");
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                var sports = Sportsmen;
                if (sports.Length == 0) return 0;

                double sum = 0;
                foreach (var s in sports)
                {
                    sum += s.Place;
                }

                if (sum == 0) return 0;
                double avg = sum / sports.Length;

                return 100.0 / avg;
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                var sports = Sportsmen;
                if (sports.Length == 0) return 0;

                double sum = 0;
                double prod = 1;

                foreach (var s in sports)
                {
                    sum += s.Place;
                    if (s.Place != 0) prod *= s.Place;
                }

                if (prod == 0) return 0;

                return 100.0 * sum * sports.Length / prod;
            }
        }
    }
}