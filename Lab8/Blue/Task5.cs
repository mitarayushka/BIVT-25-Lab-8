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
                Console.WriteLine("Name: " + _name + ", Surname: " + _surname + ", Place: " + _place);
            }
        }
        
        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _count;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;
            
            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _count; i++)
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
                    if (_count == 0)
                        return 18;
                    int top = _sportsmen[0].Place;
                    for (int i = 1; i < _count; i++)
                    {
                        if (top > _sportsmen[i].Place)
                        {
                            top = _sportsmen[i].Place;
                        }
                    }
                    return top;
                }
            }
            
            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6]; // максимум 6 человек
                _count = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (_count >= 6) return;
                _sportsmen[_count] = sportsman;
                _count++;
            }

            public void Add(Sportsman[] sportsmen)
            {
                for (int i = 0; i < sportsmen.Length; i++)
                {
                    if (_count >= 6) break;
                    _sportsmen[_count] = sportsmen[i];
                    _count++;
                }
            }
            
            public static void Sort(Team[] teams)
            {
                Array.Sort(teams, (a, b) =>
                {
                    // Сначала сравниваем по TotalScore (по убыванию)
                    int scoreComparison = b.TotalScore.CompareTo(a.TotalScore);
                    if (scoreComparison != 0) return scoreComparison;
        
                    // Если TotalScore равны, сравниваем по TopPlace (по возрастанию)
                    return a.TopPlace.CompareTo(b.TopPlace);
                });
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0)
                    return null;
                Team champion = teams[0];
                double maxStrength = teams[0].GetTeamStrength();
                for (int i = 1; i < teams.Length; i++)
                {
                    double strength = teams[i].GetTeamStrength();
                    if (strength > maxStrength)
                    {
                        maxStrength = strength;
                        champion = teams[i];
                    }
                }
                
                return champion;
            }
            
            public void Print()
            {
                Console.WriteLine("Team: " + _name + ", TotalScore: " + TotalScore + ", TopPlace: " + TopPlace);
                for (int i = 0; i < _count; i++)
                {
                    Console.Write("  ");
                    _sportsmen[i].Print();
                }
            }
        }
        
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name)
            {
            }
            
            protected override double GetTeamStrength()
            {
                Sportsman[] sportsmen = this.Sportsmen;
                if (sportsmen.Length == 0)
                    return 18;
                
                double sumPlaces = 0;
                foreach (var s in sportsmen)
                {
                    sumPlaces += s.Place;
                }
                
                double averagePlace = sumPlaces / sportsmen.Length;
                
                return 100.0 / averagePlace;
            }
            
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name)
            {
            }
            
            protected override double GetTeamStrength()
            {
                Sportsman[] sportsmen = this.Sportsmen;
                if (sportsmen.Length == 0)
                    return 18;
                
                double sumPlaces = 0;
                double productPlaces = 1;
                
                foreach (var s in sportsmen)
                {
                    sumPlaces += s.Place;
                    productPlaces *= s.Place;
                }
                
                int participantCount = sportsmen.Length;
                
                return 100.0 * sumPlaces * participantCount / productPlaces;
            }
        }
    }
}
