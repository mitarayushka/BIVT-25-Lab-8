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
            
            public string Name =>  _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    if (_coefs.Length == 0 || _coefs == null) return null;
                    double[] copy = new double[_coefs.Length];
                    Array.Copy(_coefs, copy, _coefs.Length);
                    return copy;
                }
            }

            public int[,] Marks
            {
                get
                {
                    if (_marks.Length == 0 || _marks == null) return null;
                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
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
                    double result = 0;
                    for (int jump = 0; jump < 4; jump++)
                    {
                        int[] jumpMarks = new int[7];
                        for (int judge = 0; judge < 7; judge++)
                        {
                            jumpMarks[judge] = _marks[jump, judge];
                        }

                        Array.Sort(jumpMarks);
                        int sum = 0;
                        for (int i = 1; i <= 5; i++)
                        {
                            sum += jumpMarks[i];
                        }

                        result += sum * _coefs[jump];
                    }
                    return result;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];

            }
            public void SetCriterias(double[] coefs)
            {
                Array.Copy(coefs, _coefs, _coefs.Length);
            }
            int jumpNumber = 0;
            public void Jump(int[] marks)
            {
                if (jumpNumber < marks.Length)
                {
                    for (int judge = 0; judge < 7; judge++)
                    {
                        _marks[jumpNumber, judge] = marks[judge];
                    }
                }
                jumpNumber++;
            } 
            public void Print()
            {
                Console.WriteLine(Name);
                Console.WriteLine(Surname);
                Console.WriteLine(TotalScore);
                
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                var sorted = array.OrderByDescending(participant => participant.TotalScore).ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] =  sorted[i];
                }
            }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _currentIndex = 0;
            public string Name => _name;
            
            public Judge(string name, int[] marks)
            {
                _name = name;
                _marks = marks ?? new int[0];
                _currentIndex = 0;
            }
            public int CreateMark()
            {
                if (_marks == null ||  _marks.Length == 0) return 0;    
                int mark = _marks[_currentIndex];
                _currentIndex++;
                if (_currentIndex >= _marks.Length) _currentIndex = 0;
                return mark;
            }

            public void Print()
            {
                Console.WriteLine(Name);
            }
        }

        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;

            public Judge[] Judges
            {
                get
                {
                    if (_judges == null || _judges.Length == 0) return null;
                    Judge[] copy = new Judge[_judges.Length];
                    Array.Copy(_judges, copy, _judges.Length);
                    return copy;
                }
            }

            public Participant[] Participants
            {
                get
                {
                    if (_participants == null || _participants.Length == 0) return null;
                    Participant[] copy = new Participant[_participants.Length];
                    Array.Copy(_participants, copy, _participants.Length);
                    return copy;
                }
            }

            public Competition(Judge[] judges)
            {
                _judges = judges;
                _participants = new Participant[0];
            }

            public void Evaluate(Participant jumper)
            {
                if (jumper == null || _judges == null) return;
                int[] marks = new int[_judges.Length];
                for (int i = 0; i < _judges.Length; i++)
                {
                    marks[i] = _judges[i].CreateMark();
                }
                jumper.Jump(marks);
            }

            public void Add(Participant jumper)
            {
                if (jumper == null) return;
                Array.Resize(ref _participants,  _participants.Length + 1);
                _participants[_participants.Length - 1] = jumper;
                Evaluate(jumper);
            }

            public void Add(Participant[] jumpers)
            {
                if (jumpers == null || jumpers.Length == 0) return;
                foreach (var j in jumpers)
                {
                    Add(j);
                }
            }
            public void Sort() => Participant.Sort(_participants);
        }
    }
}   
