using System.Runtime.InteropServices;

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
            public int[,] Marks => (int[,])_marks.Clone();

            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    foreach (int mark in _marks)
                    {
                        sum += mark;
                    }
                    return sum;
                }
            }

            public Participant(string Name, string Surname)
            {
                _name = Name;
                _surname = Surname;
                _marks = new int[2, 5];


            }
            public void Jump(int[] result)
            {
                Print();
                int sum = 0;
                Console.WriteLine(_marks.GetLength(1));
                for (int i = 0; i < _marks.GetLength(1); i++)
                {
                    sum += _marks[0, i];
                }
                if (sum == 0)
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        _marks[0, i] = result[i];
                    }
                }
                else
                {
                    sum = 0;
                    for (int i = 0; i < _marks.GetLength(1); i++)
                    {
                        sum += _marks[1, i];
                    }
                    if (sum == 0)
                    {
                        for (int i = 0; i < result.Length; i++)
                        {
                            _marks[1, i] = result[i];
                        }
                    }
                }

            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            Participant buf = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = buf;
                        }
                    }

                }
            }
            public void Print()
            {
                string marks = "";
                foreach (int i in _marks)
                {
                    marks += $"{i.ToString()}, ";
                }
                Console.WriteLine($"Имя: {Name} Фамилия: {Surname} \n Оценки {marks}");
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
            
            public abstract double[] Prize { get;  }
            
            protected WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length+1);
                _participants[_participants.Length-1] = participant;
            }

            public void Add(Participant[] participants)
            {
                foreach (Participant participant in participants)
                {
                    Add(participant);
                }
            }
        }

        public class WaterJump3m: WaterJump
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

            public WaterJump3m(string name, int bank): base(name, bank) {}
        }

        public class WaterJump5m: WaterJump
        {
            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3)
                    {
                        return null;
                    }
                    
                    int half = Participants.Length / 2;

                    double[] prizes = new double[half];

                    if (half >= 1) prizes[0] = Bank * 0.4;
                    if (half >= 2) prizes[1] = Bank * 0.25;
                    if (half >= 3) prizes[2] = Bank * 0.15;
                    if (half > 0)
                    {
                        double ost = Bank*0.2/half;
                        for (int i = 0; i < half; i++)
                        {
                            prizes[i] += ost;
                        }
                    }
                    return prizes;
                }
            }

            public WaterJump5m(string name,  int bank): base(name, bank) {}
        }
    }
}
