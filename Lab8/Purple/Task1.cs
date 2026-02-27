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
            private int _ijump;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs => (double[])_coefs.Clone();
            public int[,] Marks => (int[,])_marks.Clone();

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4] {2.5, 2.5, 2.5, 2.5};
                _marks = new int[4, 7];
                _ijump = 0;
            }

            public double TotalScore
            {
                get
                {
                    double total = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        int[] array = new int[7];

                        for (int j = 0; j < 7; j++)
                        {
                            array[j] = _marks[i, j];
                        }

                        Array.Sort(array);
                        int sum = 0;

                        for (int j = 1; j < 6; j++)
                        {
                            sum += array[j];
                        }

                        total += sum * Coefs[i];
                    }

                    return total;
                }
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs == null) return;

                for (int i = 0; i < 4; i++)
                {
                    _coefs[i] = coefs[i];
                }
            }

            public void Jump(int[] marks)
            {
                if (marks == null) return;

                for (int i = 0; i < marks.Length; i++)
                {
                    _marks[_ijump, i] = marks[i];
                }

                _ijump++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} | {TotalScore}");
            }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _index;

            public string Name => _name;

            public Judge (string name, int[] marks)
            {
                _name = name;
                _marks = marks == null ? Array.Empty<int>() : (int[])marks.Clone();
                _index = 0;
            }

            public int CreateMark()
            {
                if (_marks == null || _marks.Length == 0) return 0;
                if (_index < 0 || _index >= _marks.Length) _index = 0;

                return _marks[_index++];
            }

            public void Print()
            {
                if (_marks == null || _marks.Length == 0)
                {
                    Console.WriteLine("No marks.");
                } else
                {
                    Console.Write($"{Name}: ");
                    for (int i = 0; i < _marks.Length; i++) Console.Write($"{_marks[i]} ");
                } 
            }
        }

        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;

            public Judge[] Judges => _judges == null ? Array.Empty<Judge>() : (Judge[])_judges.Clone();
            public Participant[] Participants => _participants == null ? Array.Empty<Participant>() : (Participant[])_participants.Clone();

            public Competition (Judge[] judges)
            {
                _judges = judges == null ? Array.Empty<Judge>() : (Judge[])judges.Clone();
                _participants = Array.Empty<Participant>();
            }

            public void Evaluate(Participant jumper)
            {
                if (jumper == null || _judges == null) return;
                
                int[] marks = new int[_judges.Length];

                for (int i = 0; i < _judges.Length; i++)
                {
                    marks[i] = _judges[i] == null ? 0 : _judges[i].CreateMark();
                } 

                jumper.Jump(marks);
            }

            public void Add (Participant jumper)
            {
                if (jumper == null) return;
                if (_participants == null) _participants = Array.Empty<Participant>();

                Participant[] array = new Participant[_participants.Length + 1];

                for (int i = 0; i < _participants.Length; i++)
                {
                    array[i] = _participants[i];
                }

                array[array.Length - 1] = jumper;
                _participants = array;
                Evaluate(jumper);
            }

            public void Add (Participant[] jumpers)
            {
                if (jumpers == null) return;
                if (_participants == null) _participants = Array.Empty<Participant>();

                for (int i = 0; i < jumpers.Length; i++)
                {
                    Add(jumpers[i]);
                }
            }

            public void Sort()
            {
                if (_participants == null) return;
                Participant.Sort(_participants);
            }
        }
    }
}
