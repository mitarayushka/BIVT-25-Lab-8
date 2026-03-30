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

                if (place > 0) _place = place;
            }

            public void Print()
            {

            }
        }



        public abstract class Team
        {

            private string _name;
            protected Sportsman[] _sportsmen;
            protected int _count;

            public string Name => _name;

            public Sportsman[] Sportsmen
            {
                get
                {
                    
                    if (_sportsmen == null) return null;

                    Sportsman[] copy = new Sportsman[_count];
                    for (int i = 0; i < _count; i++)
                        copy[i] = _sportsmen[i];

                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    int score = 0;

                    for (int i = 0; i < _count; i++)
                    {
                        if (_sportsmen[i] == null) continue;

                        int p = _sportsmen[i].Place;


                        if (p == 1) score += 5;
                        else if (p == 2) score += 4;
                        else if (p == 3) score += 3;
                        else if (p == 4) score += 2;
                        else if (p == 5) score += 1;
                    }

                    return score;
                }
            }

            public int TopPlace
            {
                get
                {



                    if (_count == 0) return 18;

                    int min = int.MaxValue;

                    for (int i = 0; i < _count; i++)
                    {
                        if (_sportsmen[i] != null && _sportsmen[i].Place > 0)
                            if (_sportsmen[i].Place < min)
                                min = _sportsmen[i].Place;
                    }

                    return min == int.MaxValue ? 18 : min;
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
                
                if (sportsman == null || _count >= 6) return;

                _sportsmen[_count++] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
              
                if (sportsmen == null) return;

                foreach (var s in sportsmen)
                    Add(s);
            }

            public static void Sort(Team[] teams)
            {


                if (teams == null || teams.Length < 2) return;

                Array.Sort(teams, (x, y) =>
                {

                    int res = y.TotalScore.CompareTo(x.TotalScore);
                    if (res == 0)
                        return x.TopPlace.CompareTo(y.TopPlace);

                    return res;
                });
            }

            protected abstract double GetTeamStrength();


            public static Team GetChampion(Team[] teams)
            {

                if (teams == null || teams.Length == 0) return null;

                Team winner = teams[0];
                double max = winner.GetTeamStrength();

                for (int i = 1; i < teams.Length; i++)
                {
                    double cur = teams[i].GetTeamStrength();

                    if (cur > max)
                    {
                        max = cur;
                        winner = teams[i];
                    }
                }

                return winner;
            }

            public void Print()
            {

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
                    sum += _sportsmen[i].Place;

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
                double prod = 1;

                for (int i = 0; i < _count; i++)
                {
                    sum += _sportsmen[i].Place;
                    prod *= _sportsmen[i].Place;
                }

                return (100.0 * sum * _count) / prod;
            }
        }
    }
}
