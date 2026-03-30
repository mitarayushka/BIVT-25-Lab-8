
using System.Linq.Expressions;

namespace Lab8.Blue
{
    public class Task2
    {
        public struct Participant
        {
            //поля
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _jumpcount;

            //свойства
            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                        for (int j = 0; j < _marks.GetLength(1); j++)
                            copy[i, j] = _marks[i, j];
                    return copy;
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

            //конструктор

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _jumpcount = 0;
            }
            //метод
            public void Jump(int[] result)
            {
                if (_jumpcount >= 2) return;

                for (int j = 0; j < 5; j++)
                {
                    _marks[_jumpcount, j] = result[j];
                }
                _jumpcount++;
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j].TotalScore > array[j - 1].TotalScore)
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"имя:{Name}, фамилия:{Surname},счет:{TotalScore}");
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
                Participant[] newArray = new Participant[_participants.Length + 1];

                for (int i = 0; i < _participants.Length; i++)
                {
                    newArray[i] = _participants[i];
                }

                newArray[newArray.Length - 1] = participant;

                _participants = newArray;
            }
            public void Add(Participant[] participants)
            {
                int count = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    count++;
                }

                if (count == 0)
                {
                    return;
                }

                Participant[] newArray = new Participant[_participants.Length + count];

                for (int i = 0; i < _participants.Length; i++)
                {
                    newArray[i] = _participants[i];
                }

                int currenIndex = _participants.Length;
                for (int i = 0; i < participants.Length; i++)
                {
                    newArray[currenIndex] = participants[i];
                    currenIndex++;
                }
                _participants = newArray;
            }
        }
        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants == null || Participants.Length < 3)
                    {
                        return null;
                    }
                    return new double[] { Bank * 0.50, Bank * 0.30, Bank * 0.20 };

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
                    if (Participants == null || Participants.Length < 3) return null;

                    int winnersCount = Participants.Length / 2;

                    if (winnersCount < 3) winnersCount = 3;
                    if (winnersCount > 10) winnersCount = 10;

                    double[] prizes = new double[winnersCount];

                    double nPrize = (Bank * 0.20) / winnersCount;

                    for (int i = 0; i < winnersCount; i++)
                    {
                        prizes[i] = nPrize;
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

            
