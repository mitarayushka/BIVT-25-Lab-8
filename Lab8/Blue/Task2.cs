namespace Lab8.Blue
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _jumpCounter;

            public string Name => _name;
            public string Surname => _surname;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _jumpCounter = 0;
            }

            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return new int[0, 0];

                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;

                    int sum = 0;
                    foreach (int mark in _marks)
                    {
                        sum += mark;
                    }

                    return sum;
                }
            }

            public void Jump(int[] result)
            {
                if (result == null || result.Length != 5 || _jumpCounter >= 2) return;

                for (int i = 0; i < 5; i++)
                {
                    _marks[_jumpCounter, i] = result[i];
                }

                _jumpCounter++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.Write($"{Name} {Surname} {TotalScore} ");

                if (_marks != null)
                {
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            Console.Write($"{_marks[i, j]} ");
                        }
                    }
                }

                Console.WriteLine();
            }
        }

        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;

            public abstract double[] Prize { get; }

            protected WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                Participant[] newArr = new Participant[_participants.Length + 1];
                Array.Copy(_participants, newArr, _participants.Length);
                newArr[_participants.Length] = participant;
                _participants = newArr;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;

                Participant[] newArr = new Participant[_participants.Length + participants.Length];
                Array.Copy(_participants, newArr, _participants.Length);
                Array.Copy(participants, 0, newArr, _participants.Length, participants.Length);
                _participants = newArr;
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3) return new double[0];

                    Participant[] sorted = (Participant[])Participants.Clone();
                    Participant.Sort(sorted);

                    return new double[]
                    {
                        Bank * 0.50,
                        Bank * 0.30,
                        Bank * 0.20
                    };
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3) return new double[0];

                    Participant[] sorted = (Participant[])Participants.Clone();
                    Participant.Sort(sorted);

                    int middle = Participants.Length / 2;
                    int topCount = Math.Clamp(middle, 3, 10);
                    double sharePercent = 20.0 / topCount;

                    double[] prizes = new double[topCount];

                    for (int i = 0; i < topCount; i++)
                    {
                        prizes[i] = Bank * sharePercent / 100.0;
                    }

                    prizes[0] += Bank * 0.40;
                    prizes[1] += Bank * 0.25;
                    prizes[2] += Bank * 0.15;

                    return prizes;
                }
            }
        }
    }
}
