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

            public string Name
            {
                get { return _name; }
            }

            public string Surname
            {
                get { return _surname; }
            }

            public int[,] Marks
            {
                get
                {
                    if (_marks == null)
                    {
                        return new int[0, 0];
                    }

                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_marks == null)
                    {
                        return 0;
                    }

                    int sum = 0;

                    foreach (int mark in _marks)
                    {
                        sum += mark;
                    }

                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _jumpCounter = 0;
            }

            public void Jump(int[] result)
            {
                if (result == null || result.Length != 5 || _jumpCounter >= 2)
                {
                    return;
                }

                for (int i = 0; i < 5; i++)
                {
                    _marks[_jumpCounter, i] = result[i];
                }

                _jumpCounter++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1)
                {
                    return;
                }

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.Write($"{_name} {_surname} {TotalScore} ");

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

            public string Name
            {
                get { return _name; }
            }

            public int Bank
            {
                get { return _bank; }
            }

            public Participant[] Participants
            {
                get
                {
                    if (_participants == null)
                    {
                        return new Participant[0];
                    }

                    Participant[] copy = new Participant[_participants.Length];
                    Array.Copy(_participants, copy, _participants.Length);
                    return copy;
                }
            }

            public abstract double[] Prize { get; }

            protected WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                Participant[] newParticipants = new Participant[_participants.Length + 1];

                for (int i = 0; i < _participants.Length; i++)
                {
                    newParticipants[i] = _participants[i];
                }

                newParticipants[newParticipants.Length - 1] = participant;
                _participants = newParticipants;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null)
                {
                    return;
                }

                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }

            protected Participant[] GetSortedParticipants()
            {
                if (_participants == null)
                {
                    return new Participant[0];
                }

                Participant[] sorted = new Participant[_participants.Length];
                Array.Copy(_participants, sorted, _participants.Length);
                Participant.Sort(sorted);
                return sorted;
            }
        }

        public class WaterJump3m : WaterJump
        {
            public override double[] Prize
            {
                get
                {
                    Participant[] participants = GetSortedParticipants();

                    if (participants == null || participants.Length < 3)
                    {
                        return new double[0];
                    }

                    return new double[]
                    {
                        Bank * 0.5,
                        Bank * 0.3,
                        Bank * 0.2
                    };
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
                    Participant[] participants = GetSortedParticipants();

                    if (participants == null || participants.Length < 3)
                    {
                        return new double[0];
                    }

                    int count = participants.Length / 2;

                    if (count < 3)
                    {
                        count = 3;
                    }

                    if (count > 10)
                    {
                        count = 10;
                    }

                    double[] prizes = new double[count];
                    double percent = 20.0 / count;

                    for (int i = 0; i < count; i++)
                    {
                        prizes[i] = Bank * percent / 100.0;
                    }

                    prizes[0] += Bank * 0.4;
                    prizes[1] += Bank * 0.25;
                    prizes[2] += Bank * 0.15;

                    return prizes;
                }
            }

            public WaterJump5m(string name, int bank) : base(name, bank)
            {
            }
        }
    }
}
