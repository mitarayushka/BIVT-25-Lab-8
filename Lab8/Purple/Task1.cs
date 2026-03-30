namespace Lab8.Purple
{
    public class Task1
    {
        public class Participant
        {
            
            private string _name;
            private int _currentjump;
            private string _surname;
            private int[,] _marks;
            private double[] _coefs;

            public string Surname => _surname;
            public string Name => _name;
            public double[] Coefs => _coefs.ToArray();
            public int[,] Marks
            {
                get
                {
                    return (int[,])_marks.Clone();
                }
            }
            public double TotalScore
            {
                get
                {
                    double score = 0;
                    for (int jump = 0; jump < 4; jump++)
                    {
                        int JumpScore = 0;
                        int HighestMark = -100, LowestMark = 100;
                        for (int mark = 0; mark < 7; mark++)
                        {
                            JumpScore += _marks[jump, mark];
                            if (_marks[jump, mark] > HighestMark)
                            {
                                HighestMark = _marks[jump, mark];
                            }
                            if (_marks[jump, mark] < LowestMark)
                            {
                                LowestMark = _marks[jump, mark];
                            }
                        }
                        score += (JumpScore - HighestMark - LowestMark) * _coefs[jump];
                    }
                    return score;
                }
            }
            
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7]
                {
                    {0,0,0,0,0,0,0 },
                    {0,0,0,0,0,0,0 },
                    {0,0,0,0,0,0,0 },
                    {0,0,0,0,0,0,0 }
                };
                _currentjump = 0;
            }
            
            
            public void Jump(int[] marks)
            {
                for (int mark = 0; mark < 7; mark++)
                {
                    _marks[_currentjump, mark] = marks[mark];
                }
                _currentjump++;
            }
            public void SetCriterias(double[] coefs)
            {
                _coefs = coefs;
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = i; j > 0 && array[j].TotalScore > array[j - 1].TotalScore; j--)
                    {
                        (array[j], array[j - 1]) = (array[j - 1], array[j]);
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Имя: {_name}\n Фамилия:{_surname}\nРезультат:{TotalScore}");
            }
        }
        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _currentmark;
            public string Name => _name;
            public Judge(string name, int[] marks)
            {
                _marks = marks;
                _name = name;                
                _currentmark = 0;
            }
            public int CreateMark()
            {
                if (_marks.Length == 0)
                    return 0;
                else
                    return _marks[(_currentmark++) % _marks.Length];
            }
            public void Print()
            {

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
                int[] marks = new int[0];
                foreach (Judge judge in _judges)
                {
                    Array.Resize(ref marks, marks.Length + 1);
                    marks[^1] = judge.CreateMark();
                }
                jumper.Jump(marks);
            }
            public void Add(Participant jumper)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = jumper;
                this.Evaluate(_participants[^1]);
            }
            public void Add(Participant[] jumpers)
            {
                foreach (Participant jumper in jumpers)
                {
                    this.Add(jumper);
                }
            }
            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }
    }
}
