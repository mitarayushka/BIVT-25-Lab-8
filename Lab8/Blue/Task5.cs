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
            private int _count;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
                _count = 0;
            }

            public void SetPlace(int place)
            {
                if (_count > 0)
                {
                    return;
                }
                _place = place;
                _count++;
            }

            public void Print()
            {
                Console.WriteLine($"имя:{_name},Фамилия:{_surname},место:{_place}");
            }
        }

        public abstract class Team
        {
            protected string _name;
            protected Sportsman[] _sportsmen;
            protected int _count;

            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    Sportsman[] array = new Sportsman[_sportsmen.Length];
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        array[i] = _sportsmen[i];
                    }
                    return array;
                }
            }

            public int TotalScore
            {
                get
                {
                    int totalteam = 0;
                    for (int i = 0; i < _count; i++) 
                    {
                        int place = _sportsmen[i].Place;
                        if (place == 1) totalteam += 5;
                        else if (place == 2) totalteam += 4;
                        else if (place == 3) totalteam += 3;
                        else if (place == 4) totalteam += 2;
                        else if (place == 5) totalteam += 1;
                    }
                    return totalteam;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_count == 0) return 18; 
                    
                    int top = int.MaxValue;
                    for (int i = 0; i < _count; i++)
                    {
                        if (_sportsmen[i] != null && _sportsmen[i].Place > 0)
                        {
                            if (top > _sportsmen[i].Place)
                            {
                                top = _sportsmen[i].Place;
                            }
                        }
                    }
                    return top == int.MaxValue ? 0 : top; 
                }
            }

            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _count = 0;
            }

            public void Add(Sportsman sportsman)
            {
                _sportsmen[_count++] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                
                foreach (Sportsman t in sportsmen)
                {
                    if (_count >= 6)
                    {
                        break;
                    }
                    Add(t);
                }
            }

            public static void Sort(Team[] teams)
            {
                
                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 1; j < teams.Length; j++)
                    {
                    if (teams[j].TotalScore > teams[j - 1].TotalScore || (teams[j].TotalScore == teams[j - 1].TotalScore && teams[j].TopPlace < teams[j - 1].TopPlace))
                            {
                                (teams[j - 1], teams[j]) = (teams[j], teams[j - 1]);
                            }
                    
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name}, наивысший счет {TotalScore}, наивысшее место {TopPlace}");
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {

                Team champion = null;
                double maxStrength = -1;

                for (int i = 0; i < teams.Length; i++)
                {
                    if (teams[i] != null)
                    {
                        double currentStrength = teams[i].GetTeamStrength();
                        if (currentStrength > maxStrength)
                        {
                            maxStrength = currentStrength;
                            champion = teams[i];
                        }
                    }
                }

                return champion;
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (_count == 0) return 0;
                
                double sum = 0;
                for (int i = 0; i < _count; i++)
                {
                    sum += _sportsmen[i].Place;
                }
                
                if (sum == 0) return 0; 
                
                return 100.0 / (sum / _count);
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (_count == 0) return 0;

                double sum = 0;
                double product = 1;

                for (int i = 0; i < _count; i++)
                {
                    int place = _sportsmen[i].Place;
                    sum += place;
                    product *= place;
                }

                if (product == 0) return 0; 
                
                return 100.0 * sum * _count / product;
            }
        }
    }
}
