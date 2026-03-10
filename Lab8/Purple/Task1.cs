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
            public double[] Coefs => (double[])_coefs.Clone();
            public int[,] Marks => (int[,])_marks.Clone();
            public double TotalScore
            {
                get
                {
                    double total = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        double sum = 0;
                        int min = int.MaxValue;
                        int max = int.MinValue;
                        for (int j = 0; j < 7; j++)
                        {
                            if (_marks[i,j]<min) min = _marks[i,j];
                            if (_marks[i,j]>max) max = _marks[i,j];
                            sum += _marks[i,j];
                        }
                        sum -= min+max;
                        total += sum * _coefs[i];
                    }
                    return total;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];
                _jumpIndex = 0;
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || coefs.Length != 4) return;
                for (int i = 0; i < 4; i++)
                    if (coefs[i] < 2.5 || coefs[i] > 3.5) return;

                for (int i = 0; i < 4; i++)
                    _coefs[i] = coefs[i];
            }
            public void Jump(int[] marks)
            {
                if (_jumpIndex >= 4) return;
                if (marks == null || marks.Length != 7) return;
                for (int i = 0; i < 7; i++)
                    if (marks[i] < 0 || marks[i] > 6) return;

                for (int i = 0; i < 7; i++)
                    _marks[_jumpIndex, i] = marks[i];

                _jumpIndex++;
            }
            public  static  void  Sort(Participant[] array)
            {
                if (array == null || array.Length < 2) return;
                int l = 1;
                while (l < array.Length)
                {
                    if (l == 0 || array[l - 1].TotalScore >= array[l].TotalScore) l++;
                    else
                    {
                        (array[l - 1], array[l]) = (array[l], array[l - 1]);
                        l--;
                    }
                }
            }

            public void Print ()
            {
                System.Console.WriteLine("__________Participant___________");
                System.Console.WriteLine($"Name: {_name}; Surname: {_surname}");
                System.Console.WriteLine($"Coef: {string.Join(" ",_coefs)}");
                System.Console.WriteLine($"TotalScore: {TotalScore}");
                System.Console.WriteLine("Marks: ");
                System.Console.WriteLine();
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        System.Console.Write($"{_marks[i,j],5}");
                    }
                    System.Console.WriteLine();
                }
            }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _indexMark;
            public string Name => _name;
            public Judge(string name, int[] marks)
            {
                if (marks == null) return;
                _name = name;
                _marks = (int[])marks.Clone();
                _indexMark = 0;
            }
            public int CreateMark()
            {
                if (_indexMark == _marks.Length) _indexMark = 0;
                return _marks[_indexMark++];
            }
            public void Print()
            {
                System.Console.WriteLine("_____________Judge______________");
                System.Console.WriteLine($"Name: {_name}; Marks: {string.Join(" ",_marks)};");
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
                _judges = (Judge[])judges.Clone();
                _participants = new Participant[0];
            }

            // собирает у судей оценку за первый прыжок
            private int[] CreateJumpMarks()
            {
                var marks = new int[_judges.Length];
                for (int i = 0; i < _judges.Length; i++)
                    marks[i] = _judges[i].CreateMark();

                return marks;
            }

            public void Evaluate(Participant jumper)
            {
                if (jumper == null) return;
                jumper.Jump(CreateJumpMarks());
            }
            public void Add(Participant participant)
            {
                if (participant == null) return;
                Evaluate(participant);
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[^1] = participant;
            }
            public void Add(Participant[] participants)
            {
                if (participants == null) return;
                for (int i = 0; i < participants.Length; i++)
                    Add(participants[i]);
            }
            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }
    }
}