namespace Lab8.Green
{
    public class Task2
    {
        public class Human
        {
            private string _name;
            private string _surname;

            public string Name => _name;
            public string Surname => _surname;

            public Human(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }

            public void Print()
            {
                return;
            }
        }
        public class Student : Human
        {
            private int[] _marks;
            private static int _excelentStudents;

            public int[] Marks => _marks.ToArray();
            public double AverageMark => _marks.Average();
            public bool IsExcellent
            {
                get
                {
                    foreach (int mark in _marks)
                    {
                        if (mark < 4) return false;
                    }
                    return true;
                }
            }
            public static int ExcellentAmount
            {
                get
                {
                    return _excelentStudents;
                }

            }

            public Student(string name, string surname) : base(name, surname)
            {
                _marks = new int[4];
            }

            public void Exam(int mark)
            {
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        break;
                    }
                    ;
                }

                if (IsExcellent)
                    _excelentStudents++;
            }

            public static void SortByAverageMark(Student[] array)
            {
                Array.Sort(array, (a, b) => b.AverageMark.CompareTo(a.AverageMark));
            }

            public void Print()
            {
                return;
            }
        }
    }
}
