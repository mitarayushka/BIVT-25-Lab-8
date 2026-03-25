namespace Lab8.Blue
{
    public class Task5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;
            private bool _setPlace = false;

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
                if (place < 0) return;
                if (_setPlace)      // если место уже установили вернуть без изменений
                {
                    return;
                }
                _place = place;
                _setPlace = true;
            }
            public void Print()
            {
                Console.WriteLine($"{Name}, {Surname} : {Place}");
            }
        }
        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsman;
            private int _count;

            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    Sportsman[] copy = new Sportsman[_sportsman.Length];
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i] = _sportsman[i];
                    }
                    return copy;
                }
            }
            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _count; i++)
                    {
                        int place = _sportsman[i].Place;
                        if (place == 1) sum += 5;
                        else if (place == 2) sum += 4;
                        else if (place == 3) sum += 3;
                        else if (place == 4) sum += 2;
                        else if (place == 5) sum += 1;
                    }
                    return sum;
                }
            }
            public int TopPlace
            {
                get
                {
                    if (_sportsman == null || _sportsman.Length == 0)
                        return 18;
                    int top = _sportsman[0].Place;
                    for (int i = 1; i < _sportsman.Length; i++)
                    {
                        if (top > _sportsman[i].Place)
                        {
                            top = _sportsman[i].Place;
                        }
                    }
                    return top;
                }
            }
            public Team(string name)
            {
                _name = name;
                _sportsman = new Sportsman[0];
                _count = 0;
            }

            public void Add(Sportsman name)
            {
                if (_name == null)
                    return;
                if (_count >= 6)
                {
                    return;
                }
                Array.Resize(ref _sportsman, _count + 1);
                _sportsman[_count] = name;
                _count++;
            }
            public void Add(Sportsman[] names)
            {
                if (_count >= 6)
                {
                    return;
                }
                for (int i = 0; i < names.Length; i++)
                {
                    Add(names[i]);
                }
            }
            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length <= 1)
                    return;

                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - 1 - i; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                        }
                        else if (teams[j].TotalScore == teams[j + 1].TotalScore)
                        {
                            if (teams[j].TopPlace > teams[j + 1].TopPlace)
                            {
                                (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                            }
                        }
                    }
                }
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                Team champion = null;
                for (int i = 0; i < teams.Length; i++)
                {
                    if (champion == null || teams[i].GetTeamStrength() > champion.GetTeamStrength())
                    {
                        champion = teams[i];
                    }
                }

                return champion;

            }
            public void Print()
            {
                Console.WriteLine($"{_name}, {TotalScore}, {TopPlace}");
                for (int i = 0; i < _count; i++)
                {
                    Console.Write("  ");
                    _sportsman[i].Print();
                }
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen.Length == 0)
                {
                    return 0;
                }
                double sumSportsman = 0;
                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    sumSportsman += Sportsmen[i].Place;
                }
                double sr = sumSportsman / Sportsmen.Length;
                return (100 / sr);
            }
        }
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen.Length == 0)
                {
                    return 0;
                }
                int sumSportsman = 0;
                double pr = 1;
                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    sumSportsman += Sportsmen[i].Place;
                    pr *= Sportsmen[i].Place;
                }
                return ((100 * sumSportsman * Sportsmen.Length) / pr);
            }
        }
    }
}
