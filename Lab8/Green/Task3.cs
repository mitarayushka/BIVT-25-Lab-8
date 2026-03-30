namespace Lab8.Green
{
    public class Task3
    {
        public class Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            private bool _isExpelled;
            private int _id;

            private static int _nextId;

            static Student()
            {
                _nextId = 1;
            }

            public Student(string name, string surname)
            {
                _name = name == null ? "" : name;
                _surname = surname == null ? "" : surname;
                _marks = new int[3];
                _isExpelled = false;
                _id = _nextId;
                _nextId++;
            }

            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int[] Marks
            {
                get
                {
                    if (_marks == null)
                        return new int[0];

                    int[] copy = new int[_marks.Length];
                    for (int i = 0; i < _marks.Length; i++)
                        copy[i] = _marks[i];
                    return copy;
                }
            }
            public bool IsExpelled { get { return _isExpelled; } }
            public double AverageMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0)
                        return 0;

                    int sum = 0;
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

                    return (double)sum / count;
                }
            }
            public int ID { get { return _id; } }

            public void Exam(int mark)
            {
                if (_isExpelled || _marks == null)
                    return;

                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;

                        if (mark == 2)
                            _isExpelled = true;

                        return;
                    }
                }
            }

            public void Restore()
            {
                _isExpelled = false;
            }

            public static void SortByAverageMark(Student[] array)
            {
                if (array == null)
                    return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        double left = array[j] == null ? -1 : array[j].AverageMark;
                        double right = array[j + 1] == null ? -1 : array[j + 1].AverageMark;

                        if (left < right)
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
                System.Console.WriteLine($"{Name} {Surname} {ID} {AverageMark} {IsExpelled}");
            }
        }

        public class Commission
        {
            private static int[] _knownIds;

            static Commission()
            {
                _knownIds = new int[0];
            }

            private static bool Contains(int[] array, int value)
            {
                if (array == null)
                    return false;

                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == value)
                        return true;
                }

                return false;
            }

            private static void Remember(Student[] students)
            {
                if (students == null)
                    return;

                if (_knownIds == null)
                    _knownIds = new int[0];

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i] != null && !Contains(_knownIds, students[i].ID))
                    {
                        int[] newIds = new int[_knownIds.Length + 1];
                        for (int j = 0; j < _knownIds.Length; j++)
                            newIds[j] = _knownIds[j];
                        newIds[newIds.Length - 1] = students[i].ID;
                        _knownIds = newIds;
                    }
                }
            }

            public static void Sort(Student[] students)
            {
                if (students == null)
                    return;

                Remember(students);

                for (int i = 0; i < students.Length - 1; i++)
                {
                    for (int j = 0; j < students.Length - 1 - i; j++)
                    {
                        int left = students[j] == null ? int.MaxValue : students[j].ID;
                        int right = students[j + 1] == null ? int.MaxValue : students[j + 1].ID;

                        if (left > right)
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
                if (students == null)
                {
                    students = new Student[0];
                    return new Student[0];
                }

                Remember(students);

                int expelledCount = 0;
                int activeCount = 0;

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i] == null)
                        continue;

                    if (students[i].IsExpelled)
                        expelledCount++;
                    else
                        activeCount++;
                }

                Student[] expelled = new Student[expelledCount];
                Student[] active = new Student[activeCount];
                int e = 0;
                int a = 0;

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i] == null)
                        continue;

                    if (students[i].IsExpelled)
                    {
                        expelled[e] = students[i];
                        e++;
                    }
                    else
                    {
                        active[a] = students[i];
                        a++;
                    }
                }

                students = active;
                return expelled;
            }

            public static void Restore(ref Student[] students, Student restored)
            {
                if (restored == null)
                    return;

                if (students == null)
                    students = new Student[0];

                if (_knownIds == null)
                    _knownIds = new int[0];

                Remember(students);

                if (!Contains(_knownIds, restored.ID))
                    return;

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i] != null && students[i].ID == restored.ID)
                        return;
                }

                restored.Restore();
                Sort(students);

                Student[] newArr = new Student[students.Length + 1];
                int index = students.Length;

                for (int i = 0; i < students.Length; i++)
                {
                    if (students[i] != null && students[i].ID > restored.ID)
                    {
                        index = i;
                        break;
                    }
                }

                for (int i = 0; i < index; i++)
                    newArr[i] = students[i];

                newArr[index] = restored;

                for (int i = index; i < students.Length; i++)
                    newArr[i + 1] = students[i];

                students = newArr;
            }
        }
    }
}
