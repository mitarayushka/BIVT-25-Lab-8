namespace Lab8.Green
{
    public class Task3
    {
        public class Student
        {
            // Поля
            protected string _name;
            protected string _surname;
            protected int[] _marks;
            protected bool _isExpelled;
            protected int _id;
            protected static int _nextId;

            // Свойства
            public string Name => _name;
            public string Surname => _surname;
            public int ID => _id;
            public int[] Marks
            {
                get
                {
                    int[] copy = new int[_marks.Length];
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        copy[i] = _marks[i];
                    }
                    return copy;
                }
            }
            public bool IsExpelled => _isExpelled;
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

            // Cтатический конструктор
            static Student()
            {
                _nextId = 1;
            }

            // Конструктор
            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[3];
                _isExpelled = false;
                _id = _nextId;      
                _nextId++;          
            }

            // Метод
            public void Exam(int mark)
            {
                if (_isExpelled)
                    return;
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        if (mark == 2)
                            _isExpelled = true;
                        break;
                    }
                }
            }
            public void Restore()
            {
                _isExpelled = false;
            }

            public static void SortByAverageMark(Student[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].AverageMark < array[j + 1].AverageMark)
                        {
                            Student temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
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
            public static void Sort(Student[] students)
            {
                if (students == null || students.Length < 1)
                    return;
                for (int i = 0; i < students.Length; i++)
                {
                    for (int j = 0; j < students.Length - 1 - i; j++)
                    {
                        if (students[j].ID > students[j + 1].ID)
                        {
                            Student temp = students[j];
                            students[j] = students[j + 1];
                            students[j + 1] = temp;
                        }
                    }
                }
            }
            public static Student[] Expel(ref Student[] students)
            {
                if (students == null || students.Length == 0)
                    return new Student[0];
                int activeCount = 0;
                int expelledCount = 0;
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled)
                        expelledCount++;
                    else
                        activeCount++;
                }
                Student[] activeStudents = new Student[activeCount];
                Student[] expelledStudents = new Student[expelledCount];
                int count = 0, index = 0;
                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i].IsExpelled)
                    {
                        expelledStudents[count] = students[i];
                        count++;
                    }
                    else
                    {
                        activeStudents[index] = students[i];
                        index++;
                    }
                }
                students = activeStudents;
                return expelledStudents;
            }
            public static void Restore(ref Student[] students, Student restored)
            {
                if (students == null || restored == null) return;

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i] != null && students[i].ID == restored.ID)
                    {
                        if (!students[i].IsExpelled)
                        {
                            Console.WriteLine($"Ошибка: Студент с ID {restored.ID} уже учится!");
                            return;
                        }
                        else
                        {
                            students[i].Restore();
                            return;
                        }
                    }
                }
                Student[] newArray = new Student[students.Length + 1];

                // Находим позицию для вставки (по возрастанию ID)
                int insertIndex = 0;
                while (insertIndex < students.Length && students[insertIndex].ID < restored.ID)
                {
                    insertIndex++;
                }

                for (int i = 0; i < insertIndex; i++)
                {
                    newArray[i] = students[i];
                }

                restored.Restore();  
                newArray[insertIndex] = restored;

                for (int i = insertIndex; i < students.Length; i++)
                {
                    newArray[i + 1] = students[i];
                }

                students = newArray;

            }
        }
    }
}
