namespace Lab8.Blue
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _jumpCount;
            
            public string Name => _name;
            public string Surname => _surname;
            
            public int[,] Marks
            {
                get
                {
                    return (int[,])_marks.Clone();
                }
            }
            
            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            sum += _marks[i, j];
                        }
                    }
                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _jumpCount = 0;
            }

            public void Jump(int[] result)
            {
                if (_jumpCount >= 2) return;
                if (result == null) return;
                if (result.Length != 5) return;

                for (int i = 0; i < 5; i++)
                {
                    _marks[_jumpCount, i] = result[i];
                }

                _jumpCount++;
            }
            
            public static void Sort(Participant[] array)
            {
                Array.Sort(array, (a, b) => b.TotalScore.CompareTo(a.TotalScore));
            }
            
            public void Print()
            {
                Console.WriteLine($"Name: {_name}, Surname: {_surname}, TotalScore: {TotalScore}");
            }
        }
        
        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;
            private int _count;
            
            public string Name => _name;
            public int Bank => _bank;

            public Participant[] Participants
            {
                get
                {
                    Participant[] copy = new Participant[_count];
                    Array.Copy(_participants, copy, _count);
                    return copy;
                }
            }
            
            public abstract double[] Prize { get; }
            
            protected  WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[1000];
                _count = 0;
            }

            public void Add(Participant participant)
            {
                if (_count < _participants.Length)
                {
                    _participants[_count] = participant;
                    _count++;
                }
            }
            
            public void Add(Participant[] participants)
            {
                for (int i = 0; i < participants.Length; i++)
                {
                    if (_count >= _participants.Length) break;
                    _participants[_count] = participants[i];
                    _count++;
                }
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank)
            {
            }

            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3) 
                    {
                        return new double[0];
                    }
                    double[] prizes = new double[3];
                    prizes[0] = Bank * 0.5;
                    prizes[1] = Bank * 0.3;
                    prizes[2] = Bank * 0.2;
                    return prizes;
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank)
            {
            }

            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3)
                    {
                        return new double[0];
                    }
                    
                    int middleCount = Participants.Length / 2;
                    double[] prizes = new double[middleCount];
                    
                    if (middleCount >= 1) {prizes[0] = Bank * 0.4;}
                    if (middleCount >= 2) {prizes[1] = Bank * 0.25;}
                    if (middleCount >= 3) {prizes[2] = Bank * 0.15;}
                    
                    if (middleCount > 0)
                    {
                        double remainingPerPerson = Bank * (20.0 / middleCount) / 100;

                        for (int i = 0; i < middleCount; i++)
                        {
                            prizes[i] += remainingPerPerson;
                        }
                    }

                    return prizes;
                    
                }
            }
        }
    }
}
