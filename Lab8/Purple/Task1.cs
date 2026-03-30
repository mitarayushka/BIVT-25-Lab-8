using System;
using System.Linq;

namespace Lab8.Purple
{
    public class Task1
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _jumpCount;

            public string Name => _name;
            public string Surname => _surname;

            public double[] Coefs
            {
                get
                {
                    var copy = new double[_coefs.Length];
                    Array.Copy(_coefs, copy, _coefs.Length);
                    return copy;
                }
            }

            public int[,] Marks
            {
                get
                {
                    var copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public double TotalScore
            {
                get
                {
                    double total = 0;
                    for (int jump = 0; jump < 4; jump++)
                    {
                        var jumpMarks = new int[7];
                        for (int j = 0; j < 7; j++)
                            jumpMarks[j] = _marks[jump, j];
                        Array.Sort(jumpMarks);
                        int sum = 0;
                        for (int i = 1; i < 6; i++)
                            sum += jumpMarks[i];
                        total += sum * _coefs[jump];
                    }
                    return total;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4];
                _marks = new int[4, 7];
                for (int i = 0; i < 4; i++)
                    _coefs[i] = 2.5;
                _jumpCount = 0;
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs == null) return;
                for (int i = 0; i < _coefs.Length && i < coefs.Length; i++)
                    _coefs[i] = coefs[i];
            }

            public void Jump(int[] marks)
            {
                if (_jumpCount >= 4 || marks == null) return;
                int line = _jumpCount++;
                for (int i = 0; i < marks.Length && i < 7; i++)
                    _marks[line, i] = marks[i];
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                var sorted = array.OrderByDescending(p => p.TotalScore).ToArray();
                for (int i = 0; i < array.Length; i++)
                    array[i] = sorted[i];
            }

            public void Print()
            {
                Console.WriteLine($"{Surname} {Name}: {TotalScore}");
            }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _index;

            public string Name => _name;

            public Judge(string name, int[] marks)
            {
                _name = name;
                _marks = marks ?? new int[0];
                _index = 0;
            }

            public int CreateMark()
            {
                if (_marks == null || _marks.Length == 0) return 0;
                int mark = _marks[_index];
                _index = (_index + 1) % _marks.Length;
                return mark;
            }

            public void Print()
            {
                Console.WriteLine(_name);
            }
        }

        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;

            public Judge[] Judges => _judges;
            public Participant[] Participants => _participants;

            public Competition(Judge[] judges)
            {
                _judges = judges ?? new Judge[0];
                _participants = new Participant[0];
            }

            public void Evaluate(Participant jumper)
            {
                if (jumper == null || _judges == null) return;
                var marks = new int[_judges.Length];
                for (int i = 0; i < _judges.Length; i++)
                    marks[i] = _judges[i].CreateMark();
                jumper.Jump(marks);
            }

            public void Add(Participant jumper)
            {
                if (jumper == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = jumper;
                Evaluate(jumper);
            }

            public void Add(Participant[] jumpers)
            {
                if (jumpers == null) return;
                foreach (var j in jumpers)
                    Add(j);
            }

            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }
    }
}
