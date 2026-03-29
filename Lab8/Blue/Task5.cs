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

            public string Name
            {
                get { return _name; }
            }

            public string Surname
            {
                get { return _surname; }
            }

            public int Place
            {
                get { return _place; }
            }

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
                Console.WriteLine($"{Name} {Surname} - Место: {Place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name
            {
                get { return _name; }
            }

            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null)
                    {
                        return new Sportsman[0];
                    }
                    return (Sportsman[])_sportsmen.Clone();
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_sportsmen == null)
                    {
                        return 0;
                    }

                    int sum = 0;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i].Place >= 1 && _sportsmen[i].Place <= 5)
                        {
                            sum += (6 - _sportsmen[i].Place);
                        }
                    }
                    return sum;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null || _sportsmen.Length == 0)
                    {
                        return 18;
                    }

                    int min = int.MaxValue;
                    bool hasValid = false;

                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i].Place > 0)
                        {
                            if (_sportsmen[i].Place < min)
                            {
                                min = _sportsmen[i].Place;
                            }
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
                if (sportsman == null)
                {
                    return;
                }

                if (_sportsmen == null)
                {
                    _sportsmen = new Sportsman[0];
                }

                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null)
                {
                    return;
                }

                for (int i = 0; i < sportsmen.Length; i++)
                {
                    Add(sportsmen[i]);
                }
            }

            public static void Sort(Team[] array)
            {
                if (array == null)
                {
                    return;
                }

                Array.Sort(array, (t1, t2) =>
                {
                    if (t1 == null && t2 == null)
                    {
                        return 0;
                    }
                    if (t1 == null)
                    {
                        return 1;
                    }
                    if (t2 == null)
                    {
                        return -1;
                    }

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
                if (teams == null || teams.Length == 0)
                {
                    return null;
                }

                Team champion = null;
                double maxStrength = -1;

                for (int i = 0; i < teams.Length; i++)
                {
                    if (teams[i] != null)
                    {
                        double str = teams[i].GetTeamStrength();
                        if (str > maxStrength)
                        {
                            maxStrength = str;
                            champion = teams[i];
                        }
                    }
                }

                return champion;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} - Счет: {TotalScore}, Лучшее место: {TopPlace}");
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                Sportsman[] sports = Sportsmen;
                if (sports.Length == 0)
                {
                    return 0;
                }

                double sum = 0;
                for (int i = 0; i < sports.Length; i++)
                {
                    sum += sports[i].Place;
                }

                if (sum == 0)
                {
                    return 0;
                }

                double avg = sum / sports.Length;
                return 100.0 / avg;
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                Sportsman[] sports = Sportsmen;
                if (sports.Length == 0)
                {
                    return 0;
                }

                double sum = 0;
                double prod = 1;

                for (int i = 0; i < sports.Length; i++)
                {
                    sum += sports[i].Place;
                    if (sports[i].Place != 0)
                    {
                        prod *= sports[i].Place;
                    }
                }

                if (prod == 0)
                {
                    return 0;
                }

                return 100.0 * sum * sports.Length / prod;
            }
        }
    }
}