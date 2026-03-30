namespace Lab8.Blue
{
    public class Task5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;
            private bool _placeSet;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
                _placeSet = false;
            }
            public void SetPlace(int place)
            {
                if (!_placeSet)
                {
                    _place = place;
                    _placeSet = true;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}: {_place} место");
            }
        }
        public abstract class Team
        {
            protected string _name;
            protected Sportsman[] _sportsmen;
            protected int _sportsmanCount;

            public string Name => _name;

            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) return new Sportsman[0];
                    Sportsman[] copy = new Sportsman[_sportsmanCount];
                    Array.Copy(_sportsmen, copy, _sportsmanCount);
                    return copy;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int total = 0;
                    for (int i = 0; i < _sportsmanCount; i++)
                    {
                        int place = _sportsmen[i].Place;
                        if (place == 1) total += 5;
                        else if (place == 2) total += 4;
                        else if (place == 3) total += 3;
                        else if (place == 4) total += 2;
                        else if (place == 5) total += 1;
                    }
                    return total;
                }
            }
            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null) return 19;
                    int best = 19;
                    for (int i = 0; i < _sportsmanCount; i++)
                    {
                        int place = _sportsmen[i].Place;
                        if (place > 0 && place < best)
                        {
                            best = place;
                        }
                    }
                    return best;
                }
            }
            protected Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _sportsmanCount = 0;
            }
            public void Add(Sportsman sportsman)
            {
                if (_sportsmanCount < 6 && _sportsmen != null)
                {
                    _sportsmen[_sportsmanCount] = sportsman;
                    _sportsmanCount++;
                }
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null) return;

                foreach (Sportsman s in sportsmen)
                {
                    if (_sportsmanCount < 6)
                    {
                        _sportsmen[_sportsmanCount] = s;
                        _sportsmanCount++;
                    }
                    else break;
                }
            }
            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0)
                    return null;
                Team champion = teams[0];
                double championStrength = champion.GetTeamStrength();
                for (int i = 1; i < teams.Length; i++)
                {
                    double currentStrength = teams[i].GetTeamStrength();
                    if (currentStrength > championStrength)
                    {
                        champion = teams[i];
                        championStrength = currentStrength;
                    }
                }
                return champion;
            }
            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length <= 1) return;
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = i + 1; j < teams.Length; j++)
                    {
                        bool swap = false;
                        if (teams[i].TotalScore < teams[j].TotalScore)
                        {
                            swap = true;
                        }
                        else if (teams[i].TotalScore == teams[j].TotalScore)
                        {
                            if (teams[i].TopPlace > teams[j].TopPlace)
                            {
                                swap = true;
                            }
                        }
                        if (swap)
                        {
                            Team temp = teams[i];
                            teams[i] = teams[j];
                            teams[j] = temp;
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Команда {_name}: {TotalScore} баллов, лучшее место: {TopPlace}");
            }
        }
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                if (_sportsmanCount == 0) return 0;
                double sumPlaces = 0;
                for (int i = 0; i < _sportsmanCount; i++)
                {
                    sumPlaces += _sportsmen[i].Place;
                }
                double averagePlace = sumPlaces / _sportsmanCount;
                return 100 / averagePlace;
            }
        }
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                if (_sportsmanCount == 0) return 0;
                double sumPlaces = 0;
                double productPlaces = 1;
                for (int i = 0; i < _sportsmanCount; i++)
                {
                    int place = _sportsmen[i].Place;
                    sumPlaces += place;
                    productPlaces *= place;
                }
                return 100 * sumPlaces * _sportsmanCount / productPlaces;
            }
        }
    }
}
