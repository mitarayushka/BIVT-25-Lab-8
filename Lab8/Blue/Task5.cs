namespace Lab8.Blue
{
    public class Task5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place = 0; 
            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;
            public Sportsman(string Name, string Surname)
            {
                _name = Name;
                _surname = Surname;
            }
            public void SetPlace(int place)
            {
                if (_place == 0)
                {
                    _place = place;
                }
            }
            public void Print()
            {
                Console.WriteLine($"Имя: {Name} Фамилия: {Surname}, Место: {Place}");
            }

        }
        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmens;
            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    Sportsman[] copy = new Sportsman[_sportsmens.Length];
                    Array.Copy(_sportsmens, 0, copy, 0, _sportsmens.Length);
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    foreach (Sportsman sportsmen in _sportsmens)
                    {
                        if (sportsmen.Place <= 5)
                        {
                            sum += 6 - sportsmen.Place;
                        }
                    }
                    return sum;
                }
            }
            public int TopPlace
            {
                get
                {
                    int topplace = 18;
                    foreach (Sportsman sportsmen in _sportsmens)
                    {
                        if (sportsmen.Place > 0)
                        {
                            topplace = int.Min(topplace, sportsmen.Place);
                        }
                    }

                    Console.WriteLine($"Наивысшее место: {topplace}");
                    return topplace;
                }
            }

            public Team(string Name)
            {
                _name = Name;
                _sportsmens = [];

            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                Team champion = null;
                foreach (Team t in teams)
                {
                    if (champion == null || t.GetTeamStrength() > champion.GetTeamStrength())
                    {
                        champion = t;
                    } 
                }
                return champion;
            }

            public void Add(Sportsman Name)
            {
                if (_sportsmens.Length < 6)
                {
                    Array.Resize(ref _sportsmens, _sportsmens.Length + 1);
                    _sportsmens[_sportsmens.Length - 1] = Name;
                }
            }
            public void Add(Sportsman[] Names)
            {
                foreach (Sportsman sportsmen in Names)
                {
                    Add(sportsmen);
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null) return;
                Array.Sort(teams, (a, b) =>
                {
                    if (a.TotalScore != b.TotalScore)
                        return b.TotalScore.CompareTo(a.TotalScore);
                    else
                        return a.TopPlace.CompareTo(b.TopPlace);
                });
            }
            public void Print()
            {
                Console.WriteLine($"Команда: {Name}, Итоговые очки: {TotalScore}");
                foreach (Sportsman sportsmen in _sportsmens)
                {
                    sportsmen.Print();
                }
            }
        }

        public class ManTeam: Team
        {
            public ManTeam(string name): base(name) {}

            protected override double GetTeamStrength()
            {
                if (Sportsmen.Length == 0)
                {
                    return 0;
                }
                double sum = 0;
                foreach(var sportsmen in Sportsmen)
                {
                    sum += sportsmen.Place;
                }
                return (100 * Sportsmen.Length / sum);
            }
        }

        public class WomanTeam: Team
        {
            public WomanTeam(string name): base(name) {}

            protected override double GetTeamStrength()
            {
                if (Sportsmen.Length == 0)
                {
                    return 18;
                }
                double sum = 0;
                double com = 1;

                foreach (var sportsmen in Sportsmen)
                {
                    sum += sportsmen.Place;
                    com *= sportsmen.Place;
                }
                return (100 * sum * Sportsmen.Length) / com;
            }
        }
    }
}
