using System;

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
                    if (_marks == null) return null;
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
                    foreach (int m in _marks) sum += m;
                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
            }

            public void Jump(int[] result)
            {
                if (result == null || _marks == null) return;

                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    bool empty = true;

                    for (int j = 0; j < _marks.GetLength(1); j++)
                        if (_marks[i, j] != 0)
                        {
                            empty = false;
                            break;
                        }

                    if (empty)
                    {
                        for (int j = 0; j < Math.Min(result.Length, _marks.GetLength(1)); j++)
                            _marks[i, j] = result[j];
                        break;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                Array.Sort(array, (x, y) => y.TotalScore.CompareTo(x.TotalScore));
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {TotalScore}");
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

            public void Add(Participant p)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = p;
            }

            public void Add(Participant[] arr)
            {
                if (arr == null) return;
                foreach (var p in arr) Add(p);
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants == null || Participants.Length < 3) return null;
                    return new double[] { Bank * 0.5, Bank * 0.3, Bank * 0.2 };
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

                    int count = Participants.Length / 2;
                    double nPercent = 20.0 / count;
                    double[] prizes = new double[count];

                    if (count >= 1) prizes[0] = Bank * 0.40;
                    if (count >= 2) prizes[1] = Bank * 0.25;
                    if (count >= 3) prizes[2] = Bank * 0.15;

                    for (int i = 0; i < prizes.Length; i++)
                        prizes[i] += Bank * (nPercent / 100.0);

                    return prizes;
                }
            }
        }
    }
}
