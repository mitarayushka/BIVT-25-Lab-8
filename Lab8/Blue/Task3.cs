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
            public int[] Penalties => (int[])_penalties.Clone();
            public int Total
            {
                get
                {
                    int sum = 0;
                    foreach (int mark in _penalties)
                    {
                        sum += mark;
                    }
                    return sum;
                }
            }
            public virtual bool IsExpelled
            {
                get
                {
                    foreach (int mark in _penalties)
                    {

                        if (mark == 10)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public Participant(string Name, string Surname)
            {
                _name = Name;
                _surname = Surname;
                _penalties = new int[0];
            }
            public virtual void PlayMatch(int time)
            {
                Array.Resize(ref _penalties, _penalties.Length + 1);
                _penalties[_penalties.Length - 1] = time;
            }
            public static void Sort(Participant[] array)
            {
                var sorted = array.OrderBy(p => p.Total).ToArray();

                Array.Copy(sorted, array, array.Length);
            }
            public void Print()
            {
                Console.WriteLine($"Имя: {_name} Фамилия: {_surname}\n");
                foreach (int i in _penalties)
                {
                    Console.WriteLine(i);
                }
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }


            public override bool IsExpelled
            {
                get
                {
                    int fivePenalties = 0;
                    int count = 0;

                    foreach (int i in _penalties)
                    {
                        if (i == 5)
                        {
                            fivePenalties++;
                        }
                        count += i;
                    }
                    if (fivePenalties > _penalties.Length * 0.1 || count > 2 * _penalties.Length)
                    {
                        return true;
                    }
                    return false;
                }
            }

            public override void PlayMatch(int fall)
            {
                if (fall < 0 || fall > 5)
                {
                    return;
                }
                base.PlayMatch(fall);
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int _players = 0;
            private static int _totalTime = 0;
            public HockeyPlayer(string name, string surname) : base(name, surname) { _players++; }
            public override bool IsExpelled
            {
                get
                {
                    int sum = 0;
                    bool b = false;

                    foreach (int i in _penalties)
                    {
                        if (i == 10)
                        {
                            b = true;
                        }
                        sum += i;
                    }

                    if (sum > 0.1 * _totalTime / _players)
                    {
                        b = true;
                    }
                    return b;
                }
            }
            public override void PlayMatch(int time)
            {
                base.PlayMatch(time);
            }
        }

    }
}
