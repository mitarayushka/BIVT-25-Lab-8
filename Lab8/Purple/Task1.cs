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
            private int _jumpIndex;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs => _coefs.ToArray();
            public int[,] Marks => (int[,])_marks.Clone();
            public double TotalScore
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return 0;
                    if (_coefs == null || _coefs.Length == 0) return 0;

                    double totalScore = 0;

                    for (int row = 0; row < _marks.GetLength(0); row++)
                    {
                        int[] marksRow = new int[7];
                        for (int col = 0; col < _marks.GetLength(1); col++)
                        {
                            marksRow[col] = _marks[row, col];
                        }

                        for (int i = 0; i < marksRow.Length; i++)
                        {
                            for (int j = 1; j < marksRow.Length; j++)
                            {
                                if (marksRow[j - 1] < marksRow[j])
                                    (marksRow[j - 1], marksRow[j]) = (marksRow[j], marksRow[j - 1]);
                            }
                        }

                        int sum = 0;
                        for (int i = 1; i < marksRow.Length - 1; i++)
                        {
                            sum += marksRow[i];
                        }

                        totalScore += sum * _coefs[row];
                    }

                    return totalScore;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4];
                for (int i = 0; i < _coefs.Length; i++)
                {
                    _coefs[i] = 2.5;
                }
                _marks = new int[4, 7];
                _jumpIndex = 0;
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || coefs.Length == 0) return;

                for (int i = 0; i < coefs.Length; i++)
                {
                    _coefs[i] = coefs[i];
                }
            }
            public void Jump(int[] marks)
            {
                if (marks == null || marks.Length != 7) return;

                for (int i = 0; i < marks.Length; i++)
                {
                    _marks[_jumpIndex, i] = marks[i];
                }
                _jumpIndex++;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 1; j < array.Length; j++)
                    {
                        if (array[j - 1].TotalScore < array[j].TotalScore)
                            (array[j - 1], array[j]) = (array[j], array[j - 1]);
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Participant: {Name} {Surname}");
                Console.WriteLine($"Total Score: {TotalScore}");
            }
        }
        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _currentMark;
            public string Name => _name;

            public Judge(string name, int[] marks)
            {
                _name = name;
                _marks = marks;
                _currentMark = 0;
            }

            public int CreateMark()
            {
                if (_marks == null || _marks.Length == 0) return 0;
                if (_currentMark == _marks.Length) _currentMark = 0;

                int mark = _marks[_currentMark];
                _currentMark++;
                return mark;
            }
            public void Print()
            {
                Console.WriteLine($"Name: {_name}");
                foreach (var mark in _marks)
                {
                    Console.Write($"{mark} ");
                }
            }
        }
        public class Competition
        {
            private Participant[] _participants;
            private Judge[] _judges;
            public Participant[] Participants => _participants;
            public Judge[] Judges => _judges;

            public Competition(Judge[] judges)
            {
                _judges = judges;
                _participants = new Participant[0];
            }
            public void Evaluate(Participant jumper)
            {
                if (_judges == null || _judges.Length == 0) return;

                int[] marks = new int[7];
                for (int i = 0; i < marks.Length; i++)
                {
                    marks[i] = _judges[i].CreateMark();
                }
                jumper.Jump(marks);
            }
            public void Add(Participant participant)
            { 
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;

                Evaluate(participant);
            }
            public void Add(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;
                foreach (var participant in participants)
                {
                    Add(participant);
                }
            }
            public void Sort()
            {
                if (_participants == null || _participants.Length == 0) return;

                for (int i = 0; i < _participants.Length; i++)
                {
                    for (int j = 1; j < _participants.Length; j++)
                    {
                        if (_participants[j - 1].TotalScore < _participants[j].TotalScore)
                        {
                            (_participants[j - 1], _participants[j]) = (_participants[j], _participants[j - 1]);
                        }
                    }
                }
            }
        }
    }
}
