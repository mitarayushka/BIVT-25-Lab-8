namespace Lab8.Green
{
    {
    public class Task3
    {
        public class Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            private bool _isExpelled;
            private int _examCount;
            private int _id;
            private static int _nextID;

            static Student()
            {
                _nextID = 1;
            }
            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[3];
                _isExpelled = false;
                _examCount = 0;
                _id = _nextID++;
            }

            public string Name => _name;
            public string Surname => _surname;
            public int ID => _id;


            public int[] Marks
            {
                get
                {
                    if (_marks == null) return new int[0];
                    int[] copy = new int[_marks.Length];
                    for (int i = 0; i < _marks.Length; i++)
                        copy[i] = _marks[i];
                    return copy;
                }
            }

            public bool IsExpelled => _isExpelled;

            public double AverageMark
            {
                get
                {
                    if (_marks == null) return 0;
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
                    if (count == 0) return 0;
                    return sum / count;
                }
            }

            public void Exam(int mark)
            {
                if (_examCount >= 3) return;

                _marks[_examCount] = mark;
                _examCount++;

                if (mark == 2)
                {
                    _isExpelled = true;
                }
            }

            public void Restore()
            {
                _isExpelled = false;
            }

            public static void SortByAverageMark(Student[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
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

            public void Print()
            {
                string marksStr = string.Join(", ", Marks);
                string expelledText;
                if (_isExpelled)
                    expelledText = "да";
                else
                    expelledText = "нет";
                Console.WriteLine($"Студент: {Name} {Surname}, оценки: {marksStr}, средний балл: {AverageMark:F2}, отчислен: {expelledText}");
            }
        }

        public class Commission
        {
            public static void Sort(Student[] students)
            {
                if (students == null) return;
                for (int i = 0; i < students.Length; i++)
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
                if (students == null) return new Student[0];

                int exCoun = 0;
                int actCount = 0;

                foreach (var s in students)
                {
                    if (s == null) continue;
                    if (s.IsExpelled) exCoun++;
                    else actCount++;
                }

                Student[] expelled = new Student[exCoun];
                Student[] active = new Student[actCount];

                int indE = 0, indA = 0;
                foreach (var s in students)
                {
                    if (s == null) continue;
                    if (s.IsExpelled) expelled[indE++] = s;
                    else active[indA++] = s;

                }
                students = active;
                return expelled;
            }

            public static void Restore(ref Student[] students, Student restored)
            {
                if (students == null || restored == null) return;

                foreach (var s in students)
                {
                    if (s != null && s.ID == restored.ID) return;
                }

                int insertPos = 0;
                while (insertPos < students.Length && students[insertPos].ID < restored.ID)
                    insertPos++;

                Student[] newArray = new Student[students.Length + 1];
                for (int i = 0; i < insertPos; i++) { newArray[i] = students[i]; }
                newArray[insertPos] = restored;
                for (int i = insertPos; i < students.Length; i++)
                {
                    newArray[i + 1] = students[i];
                }
                students = newArray;
            }
        }
    }
}
