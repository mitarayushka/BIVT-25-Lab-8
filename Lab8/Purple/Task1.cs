using System;
using System.ComponentModel;


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

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4];
                _marks = new int[4, 7];
                _jumpCount = 0;

                for (int i = 0; i < 4; i++)
                {
                    _coefs[i] = 2.5;
                }
            }

            public string Name => _name;
            public string Surname => _surname;

            public double[] Coefs => (double[])_coefs.Clone();
            public int[,] Marks => (int[,])_marks.Clone();

            public double TotalScore
            {
                get
                {
                    double total = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        int sum = 0;
                        int min = 6;
                        int max = 0;

                        for (int j = 0; j < 7; j++)
                        {
                            int mark = _marks[i, j];
                            sum += mark;
                            if (mark < min) min = mark;
                            if (mark > max) max = mark;
                        }

                        total += (sum - min - max) * _coefs[i];
                    }
                    return total;
                }
            }


            public void SetCriterias(double[] coefs)
            {
                if (coefs.Length == 4)
                {
                    _coefs = coefs;
                }
            }

            public void Jump(int[] marks)
            {
                if (_jumpCount < 4 && marks.Length == 7)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        _marks[_jumpCount, j] = marks[j];
                    }
                    _jumpCount++;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_surname} {_name}\t| Total: {TotalScore:F2}");
            }

            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[i].TotalScore < array[j].TotalScore)
                        {
                            Participant temp = array[i];
                            array[i] = array[j];
                            array[j] = temp;
                        }
                    }
                }
            }
        }
        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _count;
            public string Name => _name;
            public int[] Marks => _marks;

            public Judge(string Name, int[] Marks)
            {
                _name = Name;
                _marks = Marks;
            }
            public int CreateMark()
            {
                int res = _marks[_count];
                _count++;
                if (_count == _marks.Length)
                {
                    _count = 0;
                }
                return res;
            }
            public void Print()
            {
                Console.WriteLine($"йоу, надо имя и массив оценок");
            }
        }
        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;
            public Judge[] Judges => _judges;
            public Participant[] Participants => _participants;
            public Competition(Judge[] Judges)
            {
                _participants = [];
                _judges = Judges;
            }
            public void Evaluate(Participant jumper)
            {
                int[] marks = new int[_judges.Length];

                for (int i = 0; i < _judges.Length; i++)
                {
                    marks[i] = _judges[i].CreateMark();
                }

                jumper.Jump(marks);
            }
            public void Add(Participant jumper)
            {
                Participant[] res = new Participant[_participants.Length+1];
                for (int i = 0; i < _participants.Length; i++)
                {
                    res[i] = _participants[i];
                }
                res[res.Length-1] = jumper;
                _participants = res;
                Evaluate(jumper);
            }
            public void Add(Participant[] jumpers)
            {
                for (int i = 0; i < jumpers.Length; i++)
                {
                    Add(jumpers[i]);
                }
            }
            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }
    }   
}
