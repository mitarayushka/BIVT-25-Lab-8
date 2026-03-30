namespace Lab8.Blue
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _currentJump;

            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks => (int[,])_marks.Clone();

            public int TotalScore
            {
                get
                {
                    int summ = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                        for (int j = 0; j < _marks.GetLength(1); j++)
                            summ += _marks[i, j];
                    return summ;
                }
            }

            public void Jump(int[] result)
            {
                if (result == null) { System.Console.WriteLine("Input Jump() array null"); return; }
                if (result.Length != 5) { System.Console.WriteLine("оценок судей не 5"); return; }

                if (_currentJump <= 1)
                {
                    for (int i = 0; i < result.Length; i++)
                        _marks[_currentJump, i] = result[i];

                    _currentJump++;
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                System.Array.Sort(array, (a, b) => b.TotalScore.CompareTo(a.TotalScore));
            }

            public void Print()
            {
                System.Console.WriteLine($"Name: {Name}, Surname: {Surname}, Total score: {TotalScore}");
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _currentJump = 0;
            }
        }

        public abstract class WaterJump
        {
            private readonly string _name;
            private readonly int _bank;
            private Participant[] _participants;
            private int _count;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants
            {
                get
                {
                    var copy = new Participant[_count];
                    System.Array.Copy(_participants, copy, _count);
                    return copy;
                }
            }

            public abstract double[] Prize { get; }

            protected WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
                _count = 0;
            }

            public void Add(Participant participant)
            {
                System.Array.Resize(ref _participants, _count + 1);
                _participants[_count] = participant;
                _count++;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null) return;
                foreach (var p in participants) Add(p);
            }

            protected Participant[] GetSortedByScoreDesc()
            {
                var arr = Participants;
                Participant.Sort(arr);
                return arr;
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    var sorted = GetSortedByScoreDesc();
                    if (sorted.Length < 3) return new double[0];

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
                    var sorted = GetSortedByScoreDesc();
                    int n = sorted.Length;
                    if (n < 3) return new double[0];

                    int aboveHalf = n / 2;
                    aboveHalf = System.Math.Clamp(aboveHalf, 3, 10);

                    double topShare = Bank * 0.40;
                    double secondShare = Bank * 0.25;
                    double thirdShare = Bank * 0.15;

                    double restPool = Bank * 0.20;
                    double each = restPool / aboveHalf;

                    var prizes = new double[aboveHalf];
                    prizes[0] = topShare + each;
                    prizes[1] = secondShare + each;
                    prizes[2] = thirdShare + each;

                    for (int i = 3; i < aboveHalf; i++)
                        prizes[i] = each;

                    return prizes;
                }
            }
        }
    }
}