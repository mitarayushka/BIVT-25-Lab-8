namespace Lab8.Blue
{
    public class Task2
    {
        public abstract class WaterJump
        { 
            private string _name;
            private int _bank;
            private Participant[] _participants;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants.ToArray();
            public abstract double[] Prize { get; }

            protected WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant wj)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = wj;
            }
            
            public void Add(Participant[] waterjump)
            {
                foreach (Participant wj in waterjump)
                {
                    Array.Resize(ref _participants, _participants.Length + 1);
                    _participants[_participants.Length - 1] = wj;
                }
            }
        }
        
        public class WaterJump3m : WaterJump
        {
            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3) return null;

                    double[] percents = { 0.5, 0.3, 0.2 };
                    double[] result = new double[percents.Length];

                    for (int i = 0; i < result.Length; i++)
                    {
                        result[i] = Bank * percents[i];
                    }

                    return result;
                }
            }
            
            public WaterJump3m(string name, int bank) : base(name, bank)
            {
                
            }
        }

        public class WaterJump5m : WaterJump
        {
            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3) return null;
                    
                    int count = Participants.Length;

                    int midTop = count / 2;
                    
                    if (midTop < 3)
                        midTop = 3;

                    if (midTop > 10)
                        midTop = 10;

                    double N = 20.0 / midTop;
                    double[] prizes = new double[midTop];
                    for (int i = 0; i < midTop; i++)
                    {
                        double percent = N / 100;
                        if (i == 0) percent += 0.40;
                        
                        if (i == 1) percent += 0.25;
                        
                        if (i == 2) percent += 0.15;

                        prizes[i] = Bank * percent;
                    }
                    
                    return prizes;
                }
            }
            public WaterJump5m(string name, int bank) : base(name, bank)
            {
                
            }
        }

        public struct Participant
        {
            // Поля
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _jump_I;
            
            // Свойства
            public string Name => _name;
            public string Surname => _surname;

            public int[,] Marks
            {
                get
                {
                    int rows = _marks.GetLength(0);
                    int cols = _marks.GetLength(1);
                    int[,] copy = new int[rows, cols];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            copy[i, j] += _marks[i, j];
                        }
                    }

                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    int total = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            total += _marks[i, j];
                        }
                    }

                    return total;
                }
            }
            
            // Конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _jump_I = 0;
            }

            public void Jump(int[] result)
            {
                if (_jump_I >= 2) return;
                
                for (int j = 0; j < result.Length; j++)
                {
                    _marks[_jump_I, j] = result[j];
                }

                _jump_I++;
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].TotalScore < array[j].TotalScore)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}: {TotalScore}");
            }
        }
    }
}
