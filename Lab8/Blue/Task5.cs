namespace Lab8.Blue
{
    public class Task5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;
            private bool _isPlaceSet;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
                _isPlaceSet = false;
            }

            public void SetPlace(int place)
            {
                if (!_isPlaceSet && place >= 1 && place <= 18)
                {
                    _place = place;
                    _isPlaceSet = true;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} - место: {Place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _sportsmanCount;

            public string Name => _name;

            public Sportsman[] Sportsmen
            {
                get
                {
                    Sportsman[] result = new Sportsman[_sportsmanCount];
                    if (_sportsmen != null)
                        for (int i = 0; i < _sportsmanCount; i++)
                            result[i] = _sportsmen[i];
                    return result;
                }
            }

            public int SummaryScore
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _sportsmanCount; i++)
                    {
                        int place = _sportsmen[i].Place;
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
                    int best = 18;
                    for (int i = 0; i < _sportsmanCount; i++)
                    {
                        int place = _sportsmen[i].Place;
                        if (place != 0 && place < best)
                            best = place;
                    }
                    return best;
                }
            }

            protected int SportsmanCount => _sportsmanCount;

            protected Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[18];
                _sportsmanCount = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmanCount < 18)
                {
                    _sportsmen[_sportsmanCount] = sportsman;
                    _sportsmanCount++;
                }
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;
                for (int i = 0; i < sportsmen.Length && _sportsmanCount < 18; i++)
                {
                    _sportsmen[_sportsmanCount] = sportsmen[i];
                    _sportsmanCount++;
                }
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                Team best = null;
                foreach (Team t in teams)
                {
                    if (t == null) continue;
                    if (best == null || t.GetTeamStrength() > best.GetTeamStrength())
                        best = t;
                }
                return best;
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length <= 1) return;
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - 1 - i; j++)
                    {
                        bool shouldSwap = false;
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                            shouldSwap = true;
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore)
                            if (teams[j].TopPlace > teams[j + 1].TopPlace)
                                shouldSwap = true;

                        if (shouldSwap)
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Команда: {Name}");
                Console.WriteLine($"Общий счет: {SummaryScore} баллов");
                Console.WriteLine($"Лучшее место: {TopPlace}");
                Console.WriteLine("Состав команды:");
                Sportsman[] sp = Sportsmen;
                for (int i = 0; i < _sportsmanCount; i++)
                {
                    Console.Write("  ");
                    sp[i].Print();
                }
                Console.WriteLine();
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                if (SportsmanCount == 0) return 0;

                double sum = 0;
                Sportsman[] sp = Sportsmen;
                for (int i = 0; i < SportsmanCount; i++)
                    sum += sp[i].Place;

                double average = sum / SportsmanCount;
                return average == 0 ? 0 : 100.0 / average;
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                if (SportsmanCount == 0) return 0;

                double sum = 0;
                double product = 1;
                Sportsman[] sp = Sportsmen;

                for (int i = 0; i < SportsmanCount; i++)
                {
                    int place = sp[i].Place;
                    if (place == 0) return 0;
                    sum += place;
                    product *= place;
                }

                return product == 0 ? 0 : 100.0 * sum * SportsmanCount / product;
            }
        }
    }
}
