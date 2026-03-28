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

            private int _currentJump;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs => _coefs.ToArray();
            public int[,] Marks => (int[,])_marks.Clone();

            public double TotalScore
            {
                get
                {
                    int[] temp = new int[7];
                    double score = 0;
                    for (int y = 0; y < _marks.GetLength(0); y++)
                    {
                        for (int x = 0; x < _marks.GetLength(1); x++)
                        {
                            temp[x] = _marks[y, x];
                        }
                        score += (temp.Sum() - temp.Max() - temp.Min()) * _coefs[y];
                    }
                    return score;
                }
            }

            public Participant(string Name, string Surname)
            {
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname)) throw new Exception("Null constructor");
                _name = Name;
                _surname = Surname;
                _coefs = [2.5, 2.5, 2.5, 2.5];
                _marks = new int[4, 7];
                for (int y = 0; y < _marks.GetLength(0); y++)
                    for (int x = 0; x < _marks.GetLength(1); x++)
                        _marks[y, x] = 0;
            }
            public void SetCriterias(double[] coefs)
            {
                bool isA = true;
                if (coefs.Length == 4)
                {
                    for (int x = 0; x < _coefs.Length; x++)
                    {
                        if (coefs[x] < 2.5 || coefs[x] > 3.5)
                        {
                            isA = false;
                            break;
                        }
                    }
                    if (isA)
                    {
                        for (int x = 0; x < _coefs.Length; x++)
                            _coefs[x] = coefs[x];
                    }
                }
            }
            public void Jump(int[] marks)
            {
                bool isA = true;
                if (marks.Length == 7 && _currentJump < 4)
                {
                    for (int x = 0; x < _marks.GetLength(1); x++)
                        if (marks[x] < 0 || marks[x] > 6)
                        {
                            isA = false;
                            break;
                        }
                }
                if (isA)
                {
                    for (int x = 0; x < _marks.GetLength(1); x++)
                        _marks[_currentJump, x] = marks[x];
                    _currentJump++;
                }
            }
            public static void Sort(Participant[] array)
            {
                Participant temp;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j + 1].TotalScore > array[j].TotalScore)
                        {
                            temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Имя:{_name}\n" +
                    $"Фамилия:{_surname}\n" +
                    $"Сложность прыжков:{string.Join(' ', _coefs)}");
                Console.WriteLine("Оценки Судей:");
                PrintMatrix(_marks);
            }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _currentMark;

            public string Name => _name;

            public Judge(string Name, int[] Marks)
            {
                if (_marks.Length == 4)
                    _name = Name;
                _marks = Marks;
                _currentMark = 0;
            }
            public int CreateMark()
            {
                int mark = _marks[_currentMark];
                _currentMark = (_currentMark + 1) % _marks.Length;
                return mark;
            }
            public void Print()
            {
                Console.WriteLine($"Имя:{_name}" +
                    $"Нужны оценочки 4 штуки");
            }
        }

        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;

            public Judge[] Judges => (Judge[])_judges.Clone();
            public Participant[] Participants => (Participant[])_participants.Clone();
            public Competition(Judge[] Judges, Participant[] Participants)
            {
                if (Judges.Length == 7)
                    _judges = Judges;
                _participants = Participants;
            }

            public void Evaluate(Participant jumper)
            {
                int[] temp = new int[7];
                int z = 0;
                for (int i = 0; i < 4; i++)
                {

                }
            }
            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    Console.Write($"{matrix[y, x]} ");
                }
                Console.WriteLine();
            }
        }
    }
}