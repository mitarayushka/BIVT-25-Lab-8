using System.Numerics;
using static Lab8.Blue.Task2;

namespace Lab8.Blue
{
    public class Task2
    {
        public struct Participant
        {
            // поля
            private string _name;
            private string _surname;
            private double[,] _marks;

            // свойства
            public string Name => _name;
            public string Surname => _surname;
            public double[,] Marks
            {
                get
                {
                    double[,] copy = new double[2, 5];
                    for (int i = 0; i < copy.GetLength(0); i++)
                    {
                        for (int j = 0; j < copy.GetLength(1); j++)
                        {
                            copy[i, j] = _marks[i, j];
                        }
                    }
                    return copy;
                }
            }
            public double TotalScore
            {
                get
                {
                    double sum = 0;
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
            // конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[2, 5];
            }
            public void Jump(int[] result)
            {
                int indexRow = -1;
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    bool flag = true;
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        if (_marks[i, j] != 0) //если в строке имеется оценка
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        indexRow = i;
                        break;
                    }
                }
                if (indexRow > -1)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        _marks[indexRow, j] = result[j];
                    }
                }
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length - i; j++)
                    {
                        if (array[j - 1].TotalScore < array[j].TotalScore) //по убыванию очков
                        {
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        }
                    }
                }
            }
            public void Print()
            {
                return;
            }
        }
        public abstract class WaterJump
        {
            //pole
            private string _name;
            private int _bank;
            private Participant[] _participants;
            //svoistvo
            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants
            {
                get
                {
                    Participant[] copy = new Participant[_participants.Length];
                    for (int i = 0; i < copy.Length; i++)
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
            //konstrultor
            protected WaterJump(string name,int bank)
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

                newArray[_participants.Length] = participant;

                _participants = newArray;
            }
            public void Add(Participant[] participants)
            {
                if (participants == null) return;

                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }
        }
        public class WaterJump3m : WaterJump
        {
            //svoistvo
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
            //konstruktor
            public WaterJump3m(string name, int bank) : base(name, bank) { }
        }
        public class WaterJump5m : WaterJump
        {
            //svoistvo
            public override double[] Prize
            { 
                get
                {
                    int half = (Participants.Length - 1) / 2; // середина
                    if (half > 10)
                    {
                        half = 10;
                    }
                    if (half < 3)
                    {
                        half = 3;
                    }
                    double[] prizes = new double[half + 1];

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
            //konstruktor
            public WaterJump5m(string name, int bank) : base(name, bank) { }
        }
    }
}
