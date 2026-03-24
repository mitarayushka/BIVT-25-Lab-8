namespace Lab8.Blue
{
    public class Task3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penalties;
            
            public string Name => _name;
            public string Surname => _surname;
            
            public int[] Penalties
            {
                get
                {
                    int[] copy = new int[_penalties.Length];
                    Array.Copy(_penalties, copy, _penalties.Length);
                    
                    return copy;
                }
            }
            
            public int Total
            {
                get
                {
                    int sum = 0;

                    foreach (var time in _penalties)
                        sum += time;

                    return sum;
                }
            }
            
            public virtual bool IsExpelled
            {
                get
                {
                    foreach (var t in _penalties)
                        if (t == 10)
                            return true;

                    return false;
                }
            }
            
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalties = new int[0];
            }

            public virtual void PlayMatch(int time)
            {
                int[] newArray = new int[_penalties.Length + 1];

                Array.Copy(_penalties, newArray, _penalties.Length);

                newArray[^1] = time;
                _penalties = newArray;
            }

            public static void Sort(Participant[] array)
            {
                Array.Sort(array, (a, b) => a.Total.CompareTo(b.Total));
            }
            
            
            public virtual void Print()
            { 
                Console.WriteLine("Name: " + _name + ", Surname: " + _surname + ", TotalTime: " + Total + ", IsExpelled: " + IsExpelled);
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }
            
            public override bool IsExpelled
            {
                get
                {
                    int gamesWith5Fouls = 0;

                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        if (_penalties[i] == 5)
                        {
                            gamesWith5Fouls++;
                        }
                    }
                    
                    double tenpercent = _penalties.Length * 0.1;
                    
                    if (gamesWith5Fouls > tenpercent)
                        return true;
                    
                    if (Total > _penalties.Length * 2)
                        return true;
                    
                    return false;
                }
            }
            
            public override void PlayMatch(int time)
            {
                if (time < 0 || time > 5)
                    return;
                
                int[] newArray = new int[_penalties.Length + 1];

                Array.Copy(_penalties, newArray, _penalties.Length);

                newArray[^1] = time;
                _penalties = newArray;
            }
        }
        
        public class HockeyPlayer : Participant
        {
            private static int _players = 0;
            private static int _totalTime = 0;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _players++;
            }
            
            public override bool IsExpelled
            {
                get
                {
                    foreach (var t in _penalties)
                        if (t == 10)
                            return true;
                    
                    double tenPercentAllTime = ((double)_totalTime / _players) / 10.0;
                    
                    if (Total > tenPercentAllTime)
                        return true;

                    return false;
                }
            }
            
            public override void PlayMatch(int time)
            {
                _totalTime += time;

                int[] newArray = new int[_penalties.Length + 1];
                Array.Copy(_penalties, newArray, _penalties.Length);
                newArray[^1] = time;
                _penalties = newArray;
            }
        }
    }
}
