using System.Runtime.Serialization.Json;

namespace Lab8.Green
{
    public class Task3
    {
        public class Student
        {
            // Поля 
            private string _name;
            private string _surname;
            private int[] _marks;
            private bool _isexpelled;
            private int _id;
            private static int _nextid;

            // Свойства
            public string Name => _name;
            public string Surname => _surname;
            public int[] Marks => _marks.ToArray();
            public bool IsExpelled => _isexpelled;
            public int ID => _id;

            public double AverageMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return 0;
                    double sum = 0;
                    int count = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] > 0)
                        {
                            sum += _marks[i];
                            count++;
                        }
                    }
                    if (count == 0)
                        return 0;
                    return sum / count;
                }
            }

            // Конструктор
            static Student()
            {
                _nextid = 1;
            }
            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[3];
                _isexpelled = false;
                _id = _nextid++;
            }

            // Метод
            public void Exam(int mark)
            {
                if (_isexpelled)
                    return;
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        if (mark == 2)
                            _isexpelled = true;
                        break;
                    }
                }
            }
            public static void SortByAverageMark(Student[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].AverageMark < array[j + 1].AverageMark)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }
            public void Restore()
            {
                _isexpelled = false;
            }
            public void Print()
            {
                Console.WriteLine($"Имя: {Name} {Surname}");
                Console.Write("Оценки: ");
                foreach (int m in _marks)
                    Console.Write(m + " ");
                Console.WriteLine($"Средний балл: {AverageMark}");
                Console.WriteLine($"Статус: {(IsExpelled ? "Отчислен" : "Учится")}");
                Console.WriteLine();
            }
        }
        public class Commission
        {
            private static Student[] _expelledStudents = new Student[0];
            public static void Sort(Student[] students)
            {
                if (students == null || students.Length <= 1)
                    return;

                for (int i = 0; i < students.Length - 1; i++)
                {
                    for (int j = 0; j < students.Length - 1 - i; j++)
                    {
                        if (students[j].ID > students[j + 1].ID)
                        {
                            (students[j], students[j + 1]) = (students[j + 1], students[j]);
                        }
                    }
                }
            }
            public static Student[] Expel(ref Student[] students)
            {
                if (students == null || students.Length == 0)
                    return new Student[0];

                int expelledCount = 0;
                foreach (var student in students)
                {
                    if (student.IsExpelled)
                        expelledCount++;
                }

                Student[] expelledStudents = new Student[expelledCount];
                int expelledIndex = 0;

                Student[] activeStudents = new Student[students.Length - expelledCount];
                int activeIndex = 0;

                foreach (var student in students)
                {
                    if (student.IsExpelled)
                    {
                        expelledStudents[expelledIndex++] = student;
                    }
                    else
                    {
                        activeStudents[activeIndex++] = student;
                    }
                }

                Sort(activeStudents);
                students = activeStudents;

                _expelledStudents = expelledStudents;

                return expelledStudents;
            }
            public static void Restore(ref Student[] students, Student restored)
            {
                if (students == null || restored == null)
                    return;

                bool studentExists = false;
                foreach (var student in _expelledStudents)
                {
                    if (student.ID == restored.ID)
                    {
                        studentExists = true;
                        break;
                    }
                }

                if (!studentExists)
                {
                    return;
                }

                foreach (var student in students)
                {
                    if (student.ID == restored.ID)
                    {
                        return;
                    }
                }

                Student[] newArray = new Student[students.Length + 1];

                for (int i = 0; i < students.Length; i++)
                {
                    newArray[i] = students[i];
                }

                newArray[students.Length] = restored;

                Sort(newArray);

                students = newArray;

            }
        }
    }
}
