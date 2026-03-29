using System;

namespace Lab8.Blue
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[][] _marks;
            private int _totalScore;

            public string Name => _name;
            public string Surname => _surname;
            public int[][] Marks => _marks;
            public int TotalScore => _totalScore;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[0][];
                _totalScore = 0;
            }

            public void Jump(int[] marks)
            {
                if (_marks == null)
                    _marks = new int[0][];

                Array.Resize(ref _marks, _marks.Length + 1);
                _marks[_marks.Length - 1] = marks;

                if (marks != null)
                {
                    foreach (var m in marks)
                    {
                        _totalScore += m;
                    }
                }
            }

            public static void Sort(Participant[] participants)
            {
                if (participants == null) return;
                Array.Sort(participants, (p1, p2) => p2.TotalScore.CompareTo(p1.TotalScore));
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
                if (_participants == null)
                    _participants = new Participant[0];

                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = p;
            }

            public void Add(Participant[] p)
            {
                if (p == null || p.Length == 0) return;
                if (_participants == null)
                    _participants = new Participant[0];

                int oldLength = _participants.Length;
                Array.Resize(ref _participants, oldLength + p.Length);
                Array.Copy(p, 0, _participants, oldLength, p.Length);
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
                        return null;

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
                    if (Participants == null || Participants.Length < 3)
                        return null;

                    int winnersCount = Participants.Length / 2;
                    if (winnersCount < 3) winnersCount = 3;
                    if (winnersCount > 10) winnersCount = 10;

                    double nPercent = 20.0 / winnersCount;
                    double bonus = Bank * (nPercent / 100.0);

                    double[] prizes = new double[winnersCount];

                    for (int i = 0; i < winnersCount; i++)
                    {
                        prizes[i] = bonus;
                    }

                    if (winnersCount >= 1) prizes[0] += Bank * 0.40;
                    if (winnersCount >= 2) prizes[1] += Bank * 0.25;
                    if (winnersCount >= 3) prizes[2] += Bank * 0.15;

                    return prizes;
                }
            }
        }
    }
}