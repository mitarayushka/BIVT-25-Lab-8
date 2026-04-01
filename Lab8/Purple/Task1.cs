namespace Lab8.Purple
{
    public class Task1
    {
        public class Participant
        {

            //поля

            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _jumpNumber;

            //свойства

            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return new double[0];
                    double[] copy = new double[_coefs.Length];
                    Array.Copy(_coefs, copy, _coefs.Length);
                    return copy;
                }
            }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return new int[0, 0];
                    int rows = _marks.GetLength(0);
                    int cols = _marks.GetLength(1);
                    int[,] copy = new int[rows, cols];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }
            public double TotalScore
            {
                get
                {
                    double res = 0;
                    int mx, mn, podsum;
                    for (int i = 0; i < 4; i++)
                    {
                        podsum = 0;
                        mx = int.MinValue;
                        mn = int.MaxValue;
                        for (int j = 0; j < 7; j++)
                        {
                            podsum += _marks[i, j];
                            mx = Math.Max(_marks[i, j], mx);
                            mn = Math.Min(_marks[i, j], mn);
                        }
                        res += _coefs[i] * (podsum - mn - mx);
                    }
                    return res;
                }   
            }

            //конструктор

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4];
                _marks = new int[4, 7];
                _jumpNumber = 0;
                for (int i = 0; i < 4; i++)
                {
                    _coefs[i] = 2.5;
                    for (int j = 0; j < 7; j++)
                    {
                        _marks[i, j] = 0;
                    }
                }
            }

            //методы

            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || coefs.Length < 4)
                    return;
                
                for (int i = 0; i < 4; i++)
                {
                    _coefs[i] = coefs[i];
                }
            }

            public void Jump(int[] marks)
            {
                if (marks == null || marks.Length < 7 || _jumpNumber > 3)
                {
                    return;
                }
                for (int j = 0; j < 7; j++)
                {
                    _marks[_jumpNumber, j] = marks[j];
                }
                _jumpNumber++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1)
                    return;
                
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname}: {TotalScore}");
            }
        }

        public class Judge
        {
            //поля

            private string _name;
            private int[] _marks;
            private int _index;

            //свойства

            public string Name => _name;

            //конструктор

            public Judge(string name, int[] marks)
            {
                _name = name;
                if (marks == null || marks.Length == 0)
                    _marks = new int[0];
                else
                {
                    _marks = new int[marks.Length];
                    Array.Copy(marks, _marks, marks.Length);
                }
                _index = 0;
            }

            //методы

            public int CreateMark()
            {
                if (_marks.Length == 0)
                    return 0;
                int mark = _marks[_index];
                _index = (_index + 1) % _marks.Length;
                return mark;
            }

            public void Print()
            {
                System.Console.WriteLine($"Name: {_name}, Marks: {string.Join(" ", _marks)}");
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
                    if (_judges == null)
                    {
                        return new Judge[0];
                    }
                    else
                    {
                        Judge[] copy = new Judge[_judges.Length];
                        Array.Copy(_judges, copy, _judges.Length);
                        return copy;
                    }
                }
            }
            public Participant[] Participants
            {
                get
                {
                    if (_participants == null)
                    {
                        return new Participant[0];
                    }
                    else
                    {
                        Participant[] copy = new Participant[_participants.Length];
                        Array.Copy(_participants, copy, _participants.Length);
                        return copy;
                    }
                }
            }

            public Competition(Judge[] judges)
            {
                if(judges == null || judges.Length == 0)
                {
                    _judges = new Judge[0];
                }
                else
                {
                    _judges = new Judge[judges.Length];
                    Array.Copy(judges, _judges, judges.Length);
                }
                _participants = new Participant[0];
            }

            public void Evaluate(Participant jumper)
            {
                if (jumper == null || _judges.Length == 0)
                    return;
                int n = _judges.Length;
                int[] MarksFromJudge = new int[n];
                for (int i = 0; i < n; i++)
                {
                    MarksFromJudge[i] = _judges[i].CreateMark();
                }
                jumper.Jump(MarksFromJudge);
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
                Evaluate(participant);
            }

            public void Add(Participant[] participants)
            {
                if (participants == null || participants.Length == 0)
                {
                    return;
                }
                int n = _participants.Length;
                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }

            public void Sort()
            {
                if (_participants == null || _participants.Length == 0 || _participants.Length == 1)
                    return;
                Participant.Sort(_participants);
            }


        }
    }


}