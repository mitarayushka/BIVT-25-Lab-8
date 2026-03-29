using System;

namespace Lab7.White
{
    public class Task5
    {
        public struct Match
        {
            private int _goals;
            private int _misses;

            public int Goals { get { return _goals; } }
            public int Misses { get { return _misses; } }

            public int Difference => _goals - _misses;
            public int Score
            {
                get
                {
                    if (_goals > _misses)
                        return 3;
                    else if (_goals == _misses)
                        return 1;
                    else
                        return 0;
                }
            }
            public Match(int goals, int misses)
            {
                _goals = goals;
                _misses = misses;
            }
            public void Print()
            {
                Console.WriteLine($"Забито: {_goals}, Пропущено: {_misses}");
                Console.WriteLine($"Разница: {Difference}, Очки: {Score}");
            }
        }

        public abstract class Team
        {
            protected string _name;
            protected Match[] _matches;

            public string Name => _name;
            public Match[] Matches => _matches;

            public int TotalDifference
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _matches.Length; i++)
                        sum += _matches[i].Difference;
                    return sum;
                }
            }

            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _matches.Length; i++)
                        sum += _matches[i].Score;
                    return sum;
                }
            }

            public Team(string name)
            {
                _name = name;
                _matches = new Match[0];
            }

            public virtual void PlayMatch(int goals, int misses)
            {
                int currentLength = _matches.Length;
                Array.Resize(ref _matches, currentLength + 1);
                _matches[currentLength] = new Match(goals, misses);
            }

            public static void SortTeams(Team[] teams)
            {
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - 1 - i; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                        else if (teams[j].TotalScore == teams[j + 1].TotalScore)
                        {
                            if (teams[j].TotalDifference < teams[j + 1].TotalDifference)
                            {
                                Team temp = teams[j];
                                teams[j] = teams[j + 1];
                                teams[j + 1] = temp;
                            }
                        }
                    }
                }
            }

            public virtual void Print()
            {
                Console.WriteLine($"Название: {_name}");
                Console.WriteLine($"Total Score: {TotalScore}");
                Console.WriteLine($"Total Difference: {TotalDifference}");
            }
        }

        public class ManTeam : Team
        {
            private ManTeam _derby;

            public ManTeam Derby => _derby;

            public ManTeam(string name, ManTeam derby = null) : base(name)
            {
                _derby = derby;
            }

            public void PlayMatch(int goals, int misses, ManTeam team = null)
            {
                if (team != null && team == _derby)
                {
                    goals++;
                }
                base.PlayMatch(goals, misses);
            }

            public override void Print()
            {
                base.Print();
                if (_derby != null)
                {
                    Console.WriteLine($"Команда-дерби: {_derby.Name}");
                }
            }
        }

        public class WomanTeam : Team
        {
            private int[] _penalties;

            public int[] Penalties => _penalties;

            public int TotalPenalties
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        sum += _penalties[i];
                    }
                    return sum;
                }
            }

            public WomanTeam(string name) : base(name)
            {
                _penalties = new int[0];
            }

            public override void PlayMatch(int goals, int misses)
            {
                base.PlayMatch(goals, misses);

                if (misses > goals)
                {
                    int penalty = misses - goals;
                    int currentLength = _penalties.Length;
                    Array.Resize(ref _penalties, currentLength + 1);
                    _penalties[currentLength] = penalty;
                }
            }

            public override void Print()
            {
                base.Print();
                Console.WriteLine($"Total Penalties: {TotalPenalties}");
                Console.Write("Штрафы: ");
                for (int i = 0; i < _penalties.Length; i++)
                {
                    Console.Write(_penalties[i] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
