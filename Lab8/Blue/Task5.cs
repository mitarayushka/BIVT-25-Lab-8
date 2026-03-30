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
                _isPlaceSet = false;
            }

            public void SetPlace(int place)
            {
                if (!_isPlaceSet)
                {
                    _place = place;
                    _isPlaceSet = true;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}: {_place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _sportsmanCount;

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

                    Sportsman[] result = new Sportsman[_sportsmanCount];
                    Array.Copy(_sportsmen, result, _sportsmanCount);
                    return result;
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

                    int score = 0;

                    for (int i = 0; i < _sportsmanCount; i++)
                    {
                        if (_sportsmen[i] == null)
                        {
                            continue;
                        }

                        int place = _sportsmen[i].Place;

                        if (place == 1)
                        {
                            score += 5;
                        }
                        else if (place == 2)
                        {
                            score += 4;
                        }
                        else if (place == 3)
                        {
                            score += 3;
                        }
                        else if (place == 4)
                        {
                            score += 2;
                        }
                        else if (place == 5)
                        {
                            score += 1;
                        }
                    }

                    return score;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null)
                    {
                        return 18;
                    }

                    int bestPlace = 18;

                    for (int i = 0; i < _sportsmanCount; i++)
                    {
                        if (_sportsmen[i] == null)
                        {
                            continue;
                        }

                        int place = _sportsmen[i].Place;

                        if (place > 0 && place < bestPlace)
                        {
                            bestPlace = place;
                        }
                    }

                    return bestPlace;
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
                if (_sportsmen == null || sportsman == null)
                {
                    return;
                }

                if (_sportsmanCount < _sportsmen.Length)
                {
                    _sportsmen[_sportsmanCount] = sportsman;
                    _sportsmanCount++;
                }
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

            public static void Sort(Team[] teams)
            {
                if (teams == null)
                {
                    return;
                }

                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (CompareTeams(teams[j], teams[j + 1]) > 0)
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                    }
                }
            }

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0)
                {
                    return null;
                }

                Team champion = null;
                double bestStrength = double.MinValue;

                for (int i = 0; i < teams.Length; i++)
                {
                    if (teams[i] == null)
                    {
                        continue;
                    }

                    double strength = teams[i].GetTeamStrength();

                    if (champion == null || strength > bestStrength)
                    {
                        champion = teams[i];
                        bestStrength = strength;
                    }
                }

                return champion;
            }

            public void Print()
            {
                Console.WriteLine($"{_name}:");

                if (_sportsmen != null)
                {
                    for (int i = 0; i < _sportsmanCount; i++)
                    {
                        if (_sportsmen[i] != null)
                        {
                            Console.Write($"  {i + 1}. ");
                            _sportsmen[i].Print();
                        }
                    }
                }

                Console.WriteLine($"{TotalScore} баллов, наивысшее место: {TopPlace}");
            }

            protected Sportsman[] GetFilledSportsmen()
            {
                if (_sportsmen == null)
                {
                    return new Sportsman[0];
                }

                Sportsman[] result = new Sportsman[_sportsmanCount];
                Array.Copy(_sportsmen, result, _sportsmanCount);
                return result;
            }

            protected abstract double GetTeamStrength();

            private static int CompareTeams(Team first, Team second)
            {
                if (first == null && second == null)
                {
                    return 0;
                }

                if (first == null)
                {
                    return 1;
                }

                if (second == null)
                {
                    return -1;
                }

                if (first.TotalScore > second.TotalScore)
                {
                    return -1;
                }

                if (first.TotalScore < second.TotalScore)
                {
                    return 1;
                }

                if (first.TopPlace < second.TopPlace)
                {
                    return -1;
                }

                if (first.TopPlace > second.TopPlace)
                {
                    return 1;
                }

                string firstName = first.Name == null ? string.Empty : first.Name;
                string secondName = second.Name == null ? string.Empty : second.Name;

                return string.Compare(firstName, secondName, StringComparison.Ordinal);
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name)
            {
            }

            protected override double GetTeamStrength()
            {
                Sportsman[] sportsmen = GetFilledSportsmen();

                if (sportsmen == null || sportsmen.Length == 0)
                {
                    return 0;
                }

                double sum = 0;
                int count = 0;

                for (int i = 0; i < sportsmen.Length; i++)
                {
                    if (sportsmen[i] != null && sportsmen[i].Place > 0)
                    {
                        sum += sportsmen[i].Place;
                        count++;
                    }
                }

                if (count == 0)
                {
                    return 0;
                }

                double average = sum / count;
                return 100.0 / average;
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name)
            {
            }

            protected override double GetTeamStrength()
            {
                Sportsman[] sportsmen = GetFilledSportsmen();

                if (sportsmen == null || sportsmen.Length == 0)
                {
                    return 0;
                }

                double sumPlaces = 0;
                double productPlaces = 1;
                int count = 0;

                for (int i = 0; i < sportsmen.Length; i++)
                {
                    if (sportsmen[i] != null && sportsmen[i].Place > 0)
                    {
                        sumPlaces += sportsmen[i].Place;
                        productPlaces *= sportsmen[i].Place;
                        count++;
                    }
                }

                if (count == 0 || productPlaces == 0)
                {
                    return 0;
                }

                return 100.0 * sumPlaces * count / productPlaces;
            }
        }
    }
}
