using System.ComponentModel;

namespace Lab8.White
{
    public class Task5
    {
        public struct Match
        {
            private int _goals;
            private int _misses;

            public int Goals => _goals;
            public int Misses => _misses;
            public int Difference
            {
                get
                {
                    return _goals - _misses;
                }
            }
            public int Score
            {
                get
                {
                    if(Difference > 0)
                    {
                        return 3;
                    }else if( Difference == 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            public Match(int goals, int misses)
            {
                _goals = goals;
                _misses = misses;
            }
            public void Print()
            {
                Console.WriteLine($"Goals: {_goals}");
                Console.WriteLine($"Misses: {_misses}");
                Console.WriteLine($"Difference: {Difference}");
                Console.WriteLine($"Score: {Score}");
            }
        }
        public abstract class Team
        {
            private string _name;
            private Match[] _matches;
            
            public string Name => _name;
            public Match[] Matches => _matches;
            public int TotalDifference
            {
                get
                {
                    int count = 0;

                    for(int i = 0; i < _matches.Length; i++)
                    {
                        count += _matches[i].Difference;
                    }
                    return count;
                }
            }
            public int TotalScore
            {
                get
                {
                    int count = 0;
                    for (int i = 0; i < _matches.Length; i++)
                    {
                        count+= _matches[i].Score;
                    }
                    return count;
                }
            }
            public Team(string name)
            {
                _name = name;
                _matches = new Match[0]; 
            }
            public virtual void PlayMatch( int goals, int misses)
            {
                int currentLength = _matches.Length;
                Array.Resize(ref _matches, currentLength + 1);
                _matches[currentLength] = new Match(goals,misses);
            }
            public static void SortTeams(Team[] teams)
            {
                for( int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - 1 -i; j++)
                    {
                        if(teams[j].TotalScore < teams [j + 1].TotalScore)
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }else if (teams[j].TotalScore == teams[j + 1].TotalScore)
                        {
                            if(teams[j].TotalDifference < teams[j + 1].TotalDifference)
                            {
                                Team temp = teams[j];
                                teams[j] = teams[j + 1];
                                teams[j + 1] = temp;
                                
                            }
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Name: {_name}");
                Console.WriteLine($"Total Score: {TotalScore}");
                Console.WriteLine($"Total Diference: {TotalDifference}");
                Console.WriteLine($"Matches: ");
                if (_matches == null || _matches.Length == 0)
                {
                    Console.WriteLine("This Team doesn't have Matches");
                }
                else
                {
                    for(int i = 0; i < _matches.Length; i++)
                    {
                        Console.WriteLine($"{i+1}° Match Goals: {_matches[i].Goals} ");
                        Console.WriteLine($"{i+1}° Match Misses: {_matches[i].Misses} ");
                        Console.WriteLine($"{i+1}° Match Score: {_matches[i].Score} ");
                    }    
                }


            }
        }
        public class ManTeam : Team
        {
            private ManTeam _derby;
            public ManTeam Derby => _derby;
            public ManTeam( string name, ManTeam derby = null) : base(name)
            {
                _derby = derby;
            }
            public void PlayMatch(int goals,int misses, ManTeam team = null )
            {
                if(team != null && team  == _derby)
                {
                    goals++;
                }
                base.PlayMatch(goals,misses);
            }
        }
        public class WomenTeam : Team
        {
            private int[] _penalties;
            public int[] Penalties => _penalties.ToArray();
            public int TotalPenalties
            {
                get
                {
                    int total = 0;
                    foreach(var penalty in _penalties)
                    {
                        total+= penalty;
                    }
                    return total;
                }
            }
            public WomenTeam(string name): base(name)
            {
                _penalties = new int[0];
            }
            public override void PlayMatch(int goals, int misses)
            {
                if(misses > goals)
                {
                    int diff = misses - goals;
                    Array.Resize(ref _penalties, _penalties.Length +1);
                    _penalties[_penalties.Length - 1] = diff;
                }
                base.PlayMatch(goals, misses);
            }
        }
    }
}