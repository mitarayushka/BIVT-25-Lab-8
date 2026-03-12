namespace Lab8.Blue
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;

            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    int[,] copy = new int[2, 5];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            copy[i, j] = _marks[i, j];
                        }
                    }
                    return copy;
                }
            }
            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
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
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length - i; j++)
                    {
                        if (array[j - 1].TotalScore < array[j].TotalScore)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }

            public void Jump(int[] result)
            {
                if (result.Length != 5) { return; }
                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] < 0)
                    {
                        return;
                    }
                }
                int currentJamp = -1;
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        if (_marks[i, j] != 0)      // если хотя бы один элемент не равен 0 то оценки были выставлены
                        {
                            currentJamp = -1; break;
                        }
                        else
                        {
                            currentJamp = i;
                        }
                    }
                    if (currentJamp == i)
                    {
                        break;
                    }
                }

                if (currentJamp == -1)
                {
                    return;
                }

                for (int j = 0; j < _marks.GetLength(1); j++)
                {
                    _marks[currentJamp, j] = result[j];
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}");
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        Console.Write(_marks[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }

        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants
            {
                get
                {
                    Participant[] copy = new Participant[_participants.Length];
                    for (int i = 0; i < _participants.Length; i++)
                    {
                        copy[i] = _participants[i];
                    }
                    return copy;
                }
            }
            public abstract double[] Prize
            {
                get;
            }

            protected WaterJump(string name,  int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length-1] = participant;
            }
            public void Add(Participant[] participants)
            {
                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }

            

        }

        public class WaterJump3m : WaterJump
        {
            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3)
                    {
                        return null;
                    }

                    return [Bank * 0.5, Bank * 0.3, Bank * 0.2];
                }
            }

            public WaterJump3m(string name, int bank) : base(name, bank) { }

        }

        public class WaterJump5m : WaterJump
        {
            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3)
                    {
                        return null;
                    }

                    int half = (Participants.Length - 1) / 2;   // индекс последнего получившего приз

                    if (half > 10)
                    {
                        half = 10;
                    }
                    if (half < 3)
                    {
                        half = 3;
                    }

                    double[] prizes = new double[half+1];

                    for (int i = 0; i <= half; i++)
                    {
                        prizes[i] = Bank * (20 / (Participants.Length / 2)) / 100;
                    }

                    prizes[0] += Bank * 0.4;
                    prizes[1] += Bank * 0.25;
                    prizes[2] += Bank * 0.15;

                    return prizes;
                }
            }

            public WaterJump5m(string name, int bank) : base(name, bank) { }
        }

    }
}
